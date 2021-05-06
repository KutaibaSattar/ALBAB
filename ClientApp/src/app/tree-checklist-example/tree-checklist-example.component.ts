
import {SelectionModel} from '@angular/cdk/collections';
import {FlatTreeControl} from '@angular/cdk/tree';
import {Component, Injectable,OnInit} from '@angular/core';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { DbaccountService } from 'app/services/dbaccount.service';
import {BehaviorSubject} from 'rxjs';

/**
 * Node for to-do item
 */
export class dbAccounts {
  children: dbAccounts[];
  name:string
  level: number;
 }


/** Flat to-do item node with expandable and level information */
export class dbAccountFlatNode {
  name: string;
  level: number;
  expandable: boolean;
}

/**
 * The Json object for to-do list data.
 */



/**
 * Checklist database, it can build a tree structured Json object.
 * Each node in Json object represents a to-do item or a category.
 * If a node is a category, it has children items and new items can be added under the category.
 */
@Injectable()
export class ChecklistDatabase {
  dataChange = new BehaviorSubject<dbAccounts[]>([]);

  get data(): dbAccounts[] { return this.dataChange.value; }


  constructor(private dbAccountService: DbaccountService) {
    this.initialize();
  }



  initialize() {
    // Build the tree nodes from Json object. The result is a list of `TodoItemNode` with nested
    //     file node as children.

    this.dbAccountService.getAccounts().subscribe(

      (res: any) => {




        console.log(res[0]);
        console.log(res);
               // Notify the change.
        this.dataChange.next(res);


        //this.dataSource.data = res;}
      })

  }

  /**
   * Build the file structure tree. The `value` is the Json object, or a sub-tree of a Json object.
   * The return value is the list of `TodoItemNode`.
   */


  /** Add an item to to-do list */
  insertItem(parent: dbAccounts, name: string) {
    if (parent.children) {
      parent.children.push({name: name} as dbAccounts);
      this.dataChange.next(this.data);
    }
  }

  updateItem(node: dbAccounts, name: string) {
    node.name = name;
    this.dataChange.next(this.data);
  }
}

/**
 * @title Tree with checkboxes
 */

@Component({
  selector: 'app-tree-checklist-example',
  templateUrl: './tree-checklist-example.component.html',
  styleUrls: ['./tree-checklist-example.component.scss'],
  providers: [ChecklistDatabase]

})
export class TreeChecklistExampleComponent {

 /** Map from flat node to nested node. This helps us finding the nested node to be modified */
 flatNodeMap = new Map<dbAccountFlatNode, dbAccounts>();

 /** Map from nested node to flattened node. This helps us to keep the same object for selection */
 nestedNodeMap = new Map<dbAccounts, dbAccountFlatNode>();

 /** A selected parent node to be inserted */
 selectedParent: dbAccountFlatNode | null = null;

 /** The new item's name */
 newItemName = '';

 treeControl: FlatTreeControl<dbAccountFlatNode>;

 treeFlattener: MatTreeFlattener<dbAccounts, dbAccountFlatNode>;

 dataSource: MatTreeFlatDataSource<dbAccounts, dbAccountFlatNode>;

 /** The selection for checklist */
 checklistSelection = new SelectionModel<dbAccountFlatNode>(true /* multiple */);

 constructor(private _database: ChecklistDatabase) {
   this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,this.isExpandable, this.getChildren);

   this.treeControl = new FlatTreeControl<dbAccountFlatNode>(this.getLevel, this.isExpandable);

   this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

   _database.dataChange.subscribe(data => {
     this.dataSource.data = data;
   });
 }

 getLevel = (node: dbAccountFlatNode) => node.level;

 isExpandable = (node: dbAccountFlatNode) => node.expandable;

 getChildren = (node: dbAccounts): dbAccounts[] => node.children;

 hasChild = (_: number, _nodeData: dbAccountFlatNode) => _nodeData.expandable;

 hasNoContent = (_: number, _nodeData: dbAccountFlatNode) => _nodeData.name === '';

 /**  Transformer to convert nested node to flat node. Record the nodes in maps for later use. */

 transformer = (node: dbAccounts, level: number) => {
   const existingNode = this.nestedNodeMap.get(node);
   const flatNode = existingNode && existingNode.name === node.name
       ? existingNode
       : new dbAccountFlatNode();
   flatNode.name = node.name;
   flatNode.level = level;
   flatNode.expandable = !!node.children?.length;
   this.flatNodeMap.set(flatNode, node);
   this.nestedNodeMap.set(node, flatNode);
   return flatNode;
 }

 /** Whether all the descendants of the node are selected. */
 descendantsAllSelected(node: dbAccountFlatNode): boolean {
   const descendants = this.treeControl.getDescendants(node);
   const descAllSelected = descendants.length > 0 && descendants.every(child => {
     return this.checklistSelection.isSelected(child);
   });
   return descAllSelected;
 }

 /** Whether part of the descendants are selected */
 descendantsPartiallySelected(node: dbAccountFlatNode): boolean {
   const descendants = this.treeControl.getDescendants(node);
   const result = descendants.some(child => this.checklistSelection.isSelected(child));
   return result && !this.descendantsAllSelected(node);
 }

 /** Toggle the to-do item selection. Select/deselect all the descendants node */
 todoItemSelectionToggle(node: dbAccountFlatNode): void {
   this.checklistSelection.toggle(node);
   const descendants = this.treeControl.getDescendants(node);
   this.checklistSelection.isSelected(node)
     ? this.checklistSelection.select(...descendants)
     : this.checklistSelection.deselect(...descendants);

   // Force update for the parent
   descendants.forEach(child => this.checklistSelection.isSelected(child));
   this.checkAllParentsSelection(node);
 }

 /** Toggle a leaf to-do item selection. Check all the parents to see if they changed */
 todoLeafItemSelectionToggle(node: dbAccountFlatNode): void {
   this.checklistSelection.toggle(node);
   this.checkAllParentsSelection(node);
 }

 /* Checks all the parents when a leaf node is selected/unselected */
 checkAllParentsSelection(node: dbAccountFlatNode): void {
   let parent: dbAccountFlatNode | null = this.getParentNode(node);
   while (parent) {
     this.checkRootNodeSelection(parent);
     parent = this.getParentNode(parent);
   }
 }

 /** Check root node checked state and change it accordingly */
 checkRootNodeSelection(node: dbAccountFlatNode): void {
   const nodeSelected = this.checklistSelection.isSelected(node);
   const descendants = this.treeControl.getDescendants(node);
   const descAllSelected = descendants.length > 0 && descendants.every(child => {
     return this.checklistSelection.isSelected(child);
   });
   if (nodeSelected && !descAllSelected) {
     this.checklistSelection.deselect(node);
   } else if (!nodeSelected && descAllSelected) {
     this.checklistSelection.select(node);
   }
 }

 /* Get the parent node of a node */
 getParentNode(node: dbAccountFlatNode): dbAccountFlatNode | null {
   const currentLevel = this.getLevel(node);

   if (currentLevel < 1) {
     return null;
   }

   const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

   for (let i = startIndex; i >= 0; i--) {
     const currentNode = this.treeControl.dataNodes[i];

     if (this.getLevel(currentNode) < currentLevel) {
       return currentNode;
     }
   }
   return null;
 }

 /** Select the category so we can insert the new item. */
 addNewItem(node: dbAccountFlatNode) {
   const parentNode = this.flatNodeMap.get(node);
   this._database.insertItem(parentNode!, '');
   this.treeControl.expand(node);
 }

 /** Save the node to database */
 saveNode(node: dbAccountFlatNode, itemValue: string) {
   const nestedNode = this.flatNodeMap.get(node);
   this._database.updateItem(nestedNode!, itemValue);
 }

}
