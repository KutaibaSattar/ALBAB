export interface Member {
  id: number;
  userId: string;
  password: string;
  displayName: string;
  phoneNumber?: string;
  created?: Date;
  lastActive?: Date;
  introduction?: string;
  lookingFor?: string;
  interests?: string;
}
