import { Guid } from 'guid-typescript';

export interface User {
  userId: Guid;
  fullName: string;
  email: string;
  roleId: number;
  roleName?: string;
  password: string;
  isActive: number;
}
