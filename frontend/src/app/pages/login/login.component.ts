import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  selectedName = '';
  password = '';
  newPassword = '';
  error = '';
  needsPassword = false;
  loading = false;

  agents = ['מנהל', 'אריה', 'דב', 'רבקה', 'מוישי', 'מיכאל', 'אהרון', 'ליסה'];

  constructor(private auth: AuthService, private router: Router) {
    if (auth.isLoggedIn()) {
      this.router.navigate([auth.isManager ? '/manager' : '/agent']);
    }
  }

  onNameSelect(): void {
    this.error = '';
    this.needsPassword = false;
    if (!this.selectedName) return;
    this.auth.checkUser(this.selectedName).subscribe({
      next: (res) => { this.needsPassword = !res.hasPassword; },
      error: () => { this.error = 'משתמש לא נמצא'; }
    });
  }

  login(): void {
    if (!this.selectedName || !this.password) {
      this.error = 'מלא את כל השדות';
      return;
    }
    this.loading = true;
    this.auth.login(this.selectedName, this.password).subscribe({
      next: () => {
        this.router.navigate([this.auth.isManager ? '/manager' : '/agent']);
      },
      error: () => {
        this.error = 'סיסמה שגויה';
        this.loading = false;
      }
    });
  }

  setPassword(): void {
    if (!this.newPassword || this.newPassword.length < 3) {
      this.error = 'סיסמה חייבת להיות לפחות 3 תווים';
      return;
    }
    this.loading = true;
    this.auth.setPassword(this.selectedName, this.newPassword).subscribe({
      next: () => {
        this.password = this.newPassword;
        this.needsPassword = false;
        this.login();
      },
      error: () => {
        this.error = 'שגיאה בהגדרת סיסמה';
        this.loading = false;
      }
    });
  }
}
