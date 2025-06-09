import { HttpInterceptorFn, } from '@angular/common/http';
import { catchError, throwError } from "rxjs";
import { inject, PLATFORM_ID } from "@angular/core";
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const platformId = inject(PLATFORM_ID);

  let token: string | null = null;

  if (isPlatformBrowser(platformId)) token = localStorage.getItem('authToken');

  if (token) {
    req = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }

  return next(req).pipe(
    catchError((err) => {
      if (err.status === 401) router.navigate(['/login']).then();

      return throwError(() => err);
    })
  );
};
