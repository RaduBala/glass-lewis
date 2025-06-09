import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable, switchMap, tap } from 'rxjs';

export interface Company {
  id: string;
  name: string;
  ticker: string;
  exchange: string;
  isin: string;
  website?: string;
}

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {
  private http = inject(HttpClient);
  private baseUrl = "https://localhost:44338/companies";
  private _companies$: BehaviorSubject<Company[]> = new BehaviorSubject<Company[]>([]);

  all$ = this._companies$.asObservable();

  constructor() { }

  getAll(): Observable<Company[]> {
    return this.http.get<Company[]>(`${this.baseUrl}/list`).pipe(tap(result => this._companies$.next(result)));
  }

  create(request: Company): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.baseUrl}`, request).pipe(switchMap(result => this.getAll().pipe(map(() => result))));
  }

  update(request: Company): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${request.id}`, request).pipe(switchMap(result => this.getAll().pipe(map(() => undefined))));
  }
}
