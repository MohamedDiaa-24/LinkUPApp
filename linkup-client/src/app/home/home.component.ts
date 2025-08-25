import { Component, EventEmitter, inject, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  httpClient = inject(HttpClient);
  registerMode = false;

  users: any;
  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }
  getUsers() {
    this.httpClient.get('https://localhost:5001/api/users').subscribe({
      next: (res) => {
        this.users = res;
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log('complete load users');
      },
    });
  }
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
