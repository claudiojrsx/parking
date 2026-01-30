import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { UsersService } from '../../../../../../core/services/users.service';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatSlideToggleModule
  ],
  templateUrl: './edit-user.component.html',
})
export class EditUserComponent implements OnInit {
  userId!: string;

  user = {
    name: '',
    email: '',
    roleId: '',
    isActive: true
  };

  loading = false;

  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id')!;
    this.loadUser();
  }

  loadUser() {
    this.usersService.getById(this.userId).subscribe(user => {
      this.user = user;
    });
  }

  save() {
    this.loading = true;

    this.usersService.update(this.userId, this.user).subscribe({
      next: () => {
        this.router.navigate(['/admin/users']);
      },
      complete: () => (this.loading = false)
    });
  }
}
