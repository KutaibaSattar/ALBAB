import { Component, OnInit } from '@angular/core';
import { DbaccountService } from 'app/services/dbaccount.service';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { SelectionModel } from '@angular/cdk/collections';

export class dbAccounts {
  children: dbAccounts[];
  name:string;
  keyId : string;
  id : number;
  lvl:number;
  selected?: boolean;

 }


@Component({
  selector: 'app-dbaccount',
  templateUrl: './dbaccount.component.html',
  styleUrls: ['./dbaccount.component.scss']
})
export class DbaccountComponent implements OnInit {

  constructor(private dbAccountService: DbaccountService) {}



  treeControl: FlatTreeControl<dbAccounts>;

  treeFlattener: MatTreeFlattener<dbAccounts,dbAccounts>;

  dataSource: MatTreeFlatDataSource<dbAccounts, dbAccounts>;
  dataSourceAccount: MatTreeFlatDataSource<dbAccounts, dbAccounts>;

  ngOnInit(): void {

      this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,this.isExpandable, this.getChildren);

      this.treeControl = new FlatTreeControl(this.getLevel, this.isExpandable);



      this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

      this.getData();



  }
  getData(){

    this.dbAccountService.getAccounts().subscribe(

      (res: dbAccounts[]) => {






        //this.dataSource.data = TREE_DATA;
        // Notify the change.


        this.dataSource.data = res;

        Object.keys(this.dataSource.data).forEach(x => {
          this.setParent(this.dataSource.data[x], null);
        });




        //this.dataSource.data = res;}
      })

  }







 /* treeControl = new NestedTreeControl<FoodNode>(node => node.children); */





  getLevel = (node: dbAccounts): number => {
    //return node.lvl
    return node.lvl;
  };

  isExpandable = (node: dbAccounts): boolean => {
    return node.children.length > 0; // second calling for seting isexpandable property to node
  };

  getChildren = (node: dbAccounts) => {
    return node.children;
  };

  transformer = (node: dbAccounts, level: number) => { // first calling to map (node level with key) and return node
    //this.levels.set(node, level);
    node["selected"] = false;
    return node;
  }

  hasChildren = (index: number, node: dbAccounts): boolean => {
    return node.children.length > 0;
  };

  isOdd(node: dbAccounts) {
    return node.children.length > 0
    //return this.getLevel(node) % 2 === 1;
  }

  todoItemSelectionToggle(checked, node) {
    node.selected = checked;
    if (node.children) {
      node.children.forEach(x => {
        this.todoItemSelectionToggle(checked, x);
      });
    }
    this.checkAllParents(node);
  }
  checkAllParents(node) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every(child => child.selected);
      node.parent.indeterminate = descendants.some(child => child.selected);
      this.checkAllParents(node.parent);
    }

  }

  setParent(data, parent) {
    data.parent = parent;

    if (data.lvl < 1){
      data.expand();

    }

    if (data.children) {
      data.children.forEach(x => {
        this.setParent(x, data);
      });
    }
  }

}
