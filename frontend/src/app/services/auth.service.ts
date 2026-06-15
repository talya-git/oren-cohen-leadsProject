import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';

export interface User {
  id: number;
  name: string;
  role: string;
}

interface LoginResponse {
  token: string;
  user: User;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = environment.apiUrl;
  private userSubject = new BehaviorSubject<User | null>(null);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    const saved = localStorage.getItem('leads_token');
    const user = localStorage.getItem('leads_user');
    if (saved && user) {
      this.userSubject.next(JSON.parse(user));
    }
  }

  get token(): string | null {
    return localStorage.getItem('leads_token');
  }

  get currentUser(): User | null {
    return this.userSubject.value;
  }

  get isManager(): boolean {
    return this.currentUser?.role === 'manager';
  }

  login(name: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/auth/login`, { name, password }).pipe(
      tap(res => {
        localStorage.setItem('leads_token', res.token);
        localStorage.setItem('leads_user', JSON.stringify(res.user));
        this.userSubject.next(res.user);
      })
    );
  }

  setPassword(name: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/set-password`, { name, password });
  }

  checkUser(name: string): Observable<{ hasPassword: boolean; role: string }> {
    return this.http.post<{ hasPassword: boolean; role: string }>(`${this.apiUrl}/auth/check-user`, { name });
  }

  logout(): void {
    localStorage.removeItem('leads_token');
    localStorage.removeItem('leads_user');
    this.userSubject.next(null);
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!this.token && !!this.currentUser;
  }
}
