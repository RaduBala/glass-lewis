import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CompaniesComponent } from './companies/companies.component';

export const routes: Routes = [
  { path: "", component: CompaniesComponent },
  { path: "login", component: LoginComponent },
];
