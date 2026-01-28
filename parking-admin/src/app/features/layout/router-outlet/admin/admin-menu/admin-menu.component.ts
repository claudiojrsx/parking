import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin-menu',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-menu.component.html',
  styleUrls: ['./admin-menu.component.scss'],
})
export class AdminMenuComponent {}
