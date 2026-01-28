export interface CreateUserRequest {
  name: string;
  email: string;
  password: string;
  role: 'Admin' | 'Operator';
  isActive: boolean;
}
