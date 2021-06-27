export enum JournalType {
 Receipt = "Receipt" ,
 SalesReceipt = "SalesReceipt",
 Paymnet = "Paymnet",
 Purchase2Pay = "Purchase2Pay",
 Journal = "Journal",
 Purchase = "Purchase",
 Sales ="Sales",
}

export interface JournalEntry {
  id: number;
  jeNo: string;
  type: string;
  note: string;
  entryDate: string;
  created: string;
}
export interface JournalAccount {
  id: number;
  journalId: number;
  created: string;
  dueDate: string;
  appUserId: number;
  dbAccountId: number;
  credit: number;
  debit: number;
  refNo: string;
}

export class Journal{
  id: number;
  jeNo: string;
  JournalType: string;
  note: string;
  entryDate: string;
  created: string;
  journalAccounts: JournalAccount[];

}
