import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  accountService = inject(AccountService);
  model: any = {};

  Login() {
    this.accountService.login(this.model).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => console.log(err),
      complete: () => console.log('finished'),
    });
  }

  Logout() {
    this.accountService.Logout();
  }
}
