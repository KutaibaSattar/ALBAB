import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { DbaccountService } from 'app/services/dbaccount.service';
import { BehaviorSubject } from 'rxjs';

export class GameNode {
  children: BehaviorSubject<GameNode[]>;
  constructor(public item: string, children?: GameNode[]) {
    this.children = new BehaviorSubject(children === undefined ? [] : children);
  }
}
const TREE_DATA = [
  new GameNode('Simulation',[
    new GameNode('Factorio'),
    new GameNode('Oxygen not included'),
  ]),
  new GameNode('Indie', [
    new GameNode(`Don't Starve`, [
      new GameNode(`Region of Giants`),
      new GameNode(`Together`),
      new GameNode(`Shipwrecked`)
    ]),
    new GameNode('Terraria'),
    new GameNode('Starbound'),
    new GameNode('Dungeon of the Endless')
  ]),
  new GameNode('Action', [
    new GameNode('Overcooked')
  ]),
  new GameNode('Strategy', [
    new GameNode('Rise to ruins')
  ]),
  new GameNode('RPG', [
    new GameNode('Magicka', [
      new GameNode('Magicka 1'),
      new GameNode('Magicka 2')
    ])
  ])
];
/**
 * Node for Accounts item
 */
 export class dbAccounts {
  children: dbAccounts[];
  name:string;
  keyId : string;
  id : number;

 }


/** Flat Account item item node with expandable and level information */
export class dbAccountFlatNode {
  name: string;
  level: number;
  keyId : string;
  id : number;
  expandable: boolean;
}

@Component({
  selector: 'app-material-tree-flat',
  templateUrl: './material-tree-flat.component.html',
  styleUrls: ['./material-tree-flat.component.scss']
})
export class MaterialTreeFlatComponent implements OnInit {

  constructor(private dbAccountService: DbaccountService) {}
  recursive: boolean = false;
  levels = new Map<GameNode, number>();

  treeControl: FlatTreeControl<any>;

  treeFlattener: MatTreeFlattener<any,any>;

  dataSource: MatTreeFlatDataSource<any, any>;
  dataSourceAccount: MatTreeFlatDataSource<any, any>;

  ngOnInit(): void {

    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,this.isExpandable, this.getChildren);

    this.treeControl = new FlatTreeControl(this.getLevel, this.isExpandable);

    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

    this.getData();



}

getData(){

  this.dbAccountService.getAccounts().subscribe(

    (res: any) => {




      console.log(res[0]);
      console.log(res);
      console.log(TREE_DATA);
      //this.dataSource.data = TREE_DATA;
      // Notify the change.
      this.dataSource.data = res;


      //this.dataSource.data = res;}
    })

}

getLevel = (node: any): number => {
  //return node.lvl
  return node.lvl
};

isExpandable = (node: any): boolean => {
  return node.children.length > 0; // second calling for seting isexpandable property to node
};

getChildren = (node: any) => {
  return node.children;
};

transformer = (node: any, level: number) => { // first calling to map (node level with key) and return node
  //this.levels.set(node, level);
  return node;
}

hasChildren = (index: number, node: any): boolean => {
  return node.children.length > 0;
};

isOdd(node: any) {
  return this.getLevel(node) % 2 === 1;
}


}
