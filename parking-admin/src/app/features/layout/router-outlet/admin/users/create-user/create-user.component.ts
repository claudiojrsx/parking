import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CreateUserRequest } from '../../../../../../core/models/user.model';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-user.component.html'
})
export class CreateUserComponent {
  user: CreateUserRequest = {
    name: '',
    email: '',
    password: '',
    role: 'Operator',
    isActive: true,
  };

  submit() {
    if (!this.user.name || !this.user.email || !this.user.password) {
      alert('Preencha todos os campos obrigatórios');
      return;
    }

    console.log('Usuário criado:', this.user);

    // 🔜 aqui entra a chamada da API
    // this.userService.create(this.user).subscribe(...)
  }
}
