export interface dbAccounts {
  id: number;
  keyId: string;
  name: string;
  lvl: number;
  created: string;
  parent: dbAccounts;
  parentId: number | null;
  isExpandable: boolean;

}
export interface dbAccountsNode {
  id: number;
  keyId: string;
  name: string;
  lvl: number;
  created: string;
  parent: dbAccounts;
  parentId: number | null;
  isExpandable: boolean;
  selected?: boolean;
  children: dbAccountsNode[];

}
export class dbAccountsNewChild {
  id:number
  keyId: string;
  name: string;
  lvl: number;
  parentId: number;
  isExpandable: boolean;
}

