export interface Member {
  id: number;
  keyId: string;
  name: string;
  phoneNumber?: string;
  created?: Date;
  lastActive?: Date;
  introduction?: string;
  lookingFor?: string;
  interests?: string;
}
