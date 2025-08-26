import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-test-error',
  standalone: true,
  imports: [],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.css',
})
export class TestErrorComponent {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/buggy';
  get400Error() {
    this.http.get(`${this.baseUrl}/bad-request`).subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }

  get400ValidationError() {
    this.http.post(`${this.baseUrl}/validation-error`, 'two').subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }

  get401Error() {
    this.http.get(`${this.baseUrl}/auth`).subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }

  get404Error() {
    this.http.get(`${this.baseUrl}/not-found`).subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }

  get500ServerError() {
    this.http.get(`${this.baseUrl}/server-error`).subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
    });
  }
}
