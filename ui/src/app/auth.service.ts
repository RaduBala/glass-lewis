import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, switchMap, tap } from 'rxjs';
import { Company } from './companies.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private baseUrl = "https://localhost:44338/authentication";

  login(request: { email: string; password: string }): Observable<{ token: string }> {
    return this.http
      .post<{ token: string }>(`${this.baseUrl}/login`, request)
      .pipe(tap(response => localStorage.setItem("authToken", response.token)));
  }
}
