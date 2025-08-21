import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private httpClient: HttpClient) {}
  users: any;
  ngOnInit(): void {
    this.httpClient.get('https://localhost:5001/api/users').subscribe({
      next: (res) => {
        this.users = res;
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log('خلصت ياض');
      },
    });
  }

  title = 'linkup-client';
}
