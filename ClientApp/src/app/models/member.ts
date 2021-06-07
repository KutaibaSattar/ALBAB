
export enum MemberType {
  Client = '30' ,
  Supplier = '31',
  Staff = '32',
 }

export interface Member {
  id: number;
  keyId: string;
  name: string;
  type: MemberType;
  phoneNumber?: string;
  created?: Date;
  lastActive?: Date;
  introduction?: string;
  lookingFor?: string;
  interests?: string;
}
