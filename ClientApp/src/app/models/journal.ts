export interface Journal{
  id: number;
  journalNo: number;
  JournalType: string;
  note: string;
  entryDate: string;
  created: string;
  journalAccount: JournalAccount[];


}




export interface JournalEntry {
  id: number;
  journalNo: number;
  JournalType: string;
  note: string;
  entryDate: string;
  created: string;

}
export interface JournalAccount {
  id: number;
  journalId: number;
  created: string;
  dueDate: string;
  accountId: number;
  credit: number;
  debit: number;
  refNo: string;
}
