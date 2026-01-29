import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { RolesService } from '../../../../../../../core/services/roles.service';

@Component({
  selector: 'app-create-role',
  standalone: true,
  imports: [CommonModule, FormsModule, MatSnackBarModule],
  templateUrl: './create-role.component.html',
})
export class CreateRoleComponent {
  roleName = '';
  loading = false;

  constructor(
    private rolesService: RolesService,
    private snackBar: MatSnackBar
  ) {}

  submit() {
    if (!this.roleName.trim()) {
      this.snackBar.open('Informe o nome do role', 'Fechar', {
        duration: 3000,
      });
      return;
    }

    this.loading = true;

    this.rolesService.create({ name: this.roleName }).subscribe({
      next: () => {
        this.snackBar.open('Role criado com sucesso', 'Fechar', {
          duration: 3000,
        });
        this.roleName = '';
      },
      error: () => {
        this.snackBar.open('Erro ao criar role', 'Fechar', {
          duration: 3000,
        });
      },
      complete: () => (this.loading = false),
    });
  }
}
