import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { UsersService } from '../../../../../../core/services/users.service';
import { RolesService } from '../../../../../../core/services/roles.service';

import { CreateUserRequest } from '../../../../../../core/models/user.model';
import { Role } from '../../../../../../core/models/role.model';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [CommonModule, FormsModule, MatSnackBarModule],
  templateUrl: './create-user.component.html',
})
export class CreateUserComponent implements OnInit {
  user: CreateUserRequest = {
    name: '',
    email: '',
    password: '',
    roleId: '',
    isActive: true,
  };

  roles: Role[] = [];
  loading = false;

  constructor(
    private usersService: UsersService,
    private rolesService: RolesService,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  loadRoles() {
    this.rolesService.getAll().subscribe({
      next: (roles) => (this.roles = roles),
      error: () => alert('Erro ao carregar perfis'),
    });
  }

  submit() {
    if (
      !this.user.name ||
      !this.user.email ||
      !this.user.password ||
      !this.user.roleId
    ) {
      this.snackBar.open('Preencha todos os campos obrigatórios', 'Fechar', {
        duration: 3000,
      });
      return;
    }

    this.loading = true;

    this.usersService.create(this.user).subscribe({
      next: () => {
        this.snackBar.open('Usuário criado com sucesso 🎉', 'OK', {
          duration: 3000,
          panelClass: ['snackbar-success'],
        });
        this.reset();
      },
      error: () => {
        this.snackBar.open('Erro ao criar usuário ❌', 'Fechar', {
          duration: 4000,
          panelClass: ['snackbar-error'],
        });
      },
      complete: () => (this.loading = false),
    });
  }

  reset() {
    this.user = {
      name: '',
      email: '',
      password: '',
      roleId: '',
      isActive: true,
    };
  }
}
