import { Component, OnInit } from '@angular/core';
import { DbAccountService } from 'app/services/dbaccount.service';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { SelectionModel } from '@angular/cdk/collections';
import { FormControl, FormGroup } from '@angular/forms';
import { dbAccounts, dbAccountsNewChild, dbAccountsNode } from 'app/models/dbaccounts';

/* export class dbAccounts {
  children: dbAccounts[];
  name:string;
  keyId : string;
  id : number;
  lvl:number;
  selected?: boolean;

 } */


@Component({
  selector: 'app-dbaccount',
  templateUrl: './dbaccount.component.html',
  styleUrls: ['./dbaccount.component.scss']
})
export class DbaccountComponent implements OnInit {

  constructor(private dbAccountService: DbAccountService) {}

  mainDbAccountForm = new FormGroup({
    Id : new FormControl(),
    Key : new FormControl(),
    Name : new FormControl(),
    lvl :new FormControl(),
    isExpandable : new FormControl(false)

  })
  childDbAccountForm = new FormGroup({
    Key : new FormControl(),
    Name : new FormControl(),
    isExpandable : new FormControl(false)

  })

  selectedNode : dbAccounts ;
  treeControl: FlatTreeControl<dbAccountsNode>;

  treeFlattener: MatTreeFlattener<dbAccountsNode,dbAccountsNode>;

  dataSource: MatTreeFlatDataSource<dbAccountsNode, dbAccountsNode>;
  dataSourceAccount: MatTreeFlatDataSource<dbAccountsNode, dbAccountsNode>;
  flatNodeMap = new Map<dbAccountsNode, dbAccountsNode>();

  ngOnInit(): void {


      this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,this.isExpandable, this.getChildren);
      this.treeControl = new FlatTreeControl(this.getLevel, this.isExpandable);
      this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

      this.getData();



  }


  getData(){

    this.dbAccountService.getDbAccounts().subscribe(

      (res: dbAccountsNode[]) => {

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





  getLevel = (node: dbAccountsNode): number => {
    //return node.lvl
    return node.lvl;
  };

  isExpandable = (node: dbAccountsNode): boolean => {
    return node.children.length > 0; // second calling for seting isexpandable property to node
  };

  getChildren = (node: dbAccountsNode) => {
    return node.children;
  };

  transformer = (node: dbAccountsNode, level: number) => { // first calling to map (node level with key) and return node
    //this.levels.set(node, level);
    node["selected"] = false;
    return node;
  }

  hasChildren = (index: number, node: dbAccountsNode): boolean => {
    return node.children.length > 0;
  };

  isOdd(node: dbAccountsNode) {
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



    if (data.children) {
      data.children.forEach(x => {
        this.setParent(x, data);
      });
    }
  }

  onSelect(node : dbAccounts){
     this.selectedNode = node
     this.mainDbAccountForm.patchValue({
      Key: node.keyId,
      Name: node.name,
      isExpandable: node.isExpandable,
      Id: node.id,
      lvl:node.lvl
    })
  }

  onCreate(){

    const dbaccountNewChild : dbAccountsNewChild = {
      id:0,
      keyId : this.childDbAccountForm.controls.Key.value,
      name : this.childDbAccountForm.controls.Name.value,
      lvl : this.selectedNode.lvl +1,
      parentId : this.selectedNode.id,
      isExpandable : this.childDbAccountForm.controls.isExpandable.value

    };
    this.dbAccountService.createDbAccount(dbaccountNewChild).subscribe(
      res => console.log(res)

    )

  }

}
