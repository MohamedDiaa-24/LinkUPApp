import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from './navbar/navbar.component';
import { AccountService } from './services/account.service';
import { HomeComponent } from "./home/home.component";
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavbarComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private httpClient: HttpClient) {}
  title = 'linkup-client';
  users: any;
  private accountService = inject(AccountService);

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
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
}
