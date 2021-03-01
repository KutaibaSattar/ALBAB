export interface Member {
  id: number;
  userId: string;
  displayName: string;
  phoneNumber?: string;
  created?: Date;
  lastActive?: Date;
  introduction?: string;
  lookingFor?: string;
  interests?: string;
}
