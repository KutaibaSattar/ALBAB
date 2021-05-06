import { Component, OnInit } from '@angular/core';
import { DbaccountService } from 'app/services/dbaccount.service';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';
import { SelectionModel } from '@angular/cdk/collections';

/**
 * Food data with nested structure.
 * Each node has a name and an optional list of children.
 */
 interface FoodNode {
  name: string;
  children?: FoodNode[];
}



/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}
interface dbAccount {
  name: string;
  lvl  : number;
}

@Component({
  selector: 'app-dbaccount',
  templateUrl: './dbaccount.component.html',
  styleUrls: ['./dbaccount.component.scss']
})
export class DbaccountComponent implements OnInit {
  private _transformer = (node: FoodNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };}

    dbaccounts: dbAccount[];

    treeControl = new FlatTreeControl<ExampleFlatNode>(
      node => node.level, node => node.expandable);


 /* treeControl = new NestedTreeControl<FoodNode>(node => node.children); */

  treeFlattener = new MatTreeFlattener(
      this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);


  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  constructor(private dbAccountService: DbaccountService) { }

  ngOnInit(): void {


    this.dbAccountService.getAccounts().subscribe(

      (res: any) => {

        this.dbaccounts = res[0];
        console.log(this.dbaccounts);

        console.log(res);

        this.dataSource.data = res;}

    )

  }
  checklistSelection = new SelectionModel<ExampleFlatNode>(true /* multiple */);

  descendantsAllSelected(node: ExampleFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    return descendants.every(child => this.checklistSelection.isSelected(child));
  }

  descendantsPartiallySelected(node: ExampleFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const result = descendants.some(child => this.checklistSelection.isSelected(child));
    return result && !this.descendantsAllSelected(node);
  }

  todoItemSelectionToggle(node: ExampleFlatNode): void {
    this.checklistSelection.toggle(node);
    const descendants = this.treeControl.getDescendants(node);
    this.checklistSelection.isSelected(node)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);
  }

}
