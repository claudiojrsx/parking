export interface CreateUserRequest {
  name: string;
  email: string;
  password: string;
  roleId: string;
  isActive: boolean;
}

export interface User {
  name: string;
  email: string;
  password: string;
  role: string;
  isActive: boolean;
}
