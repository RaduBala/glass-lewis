import { Component, inject } from '@angular/core';
import { CompaniesService, Company } from '../companies.service';
import { MatDialog } from '@angular/material/dialog';
import { CompanyFormComponent } from '../company-form/company-form.component';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-companies',
  imports: [
    AsyncPipe
  ],
  templateUrl: './companies.component.html',
  styleUrl: './companies.component.scss'
})
export class CompaniesComponent {
  private companiesService = inject(CompaniesService);
  private dialog = inject(MatDialog);

  displayedColumns: string[] = ['name', 'ticker', 'exchange', 'isin', 'website'];

  get companies$() {
    return this.companiesService.all$;
  }

  ngOnInit(): void {
    this.companiesService.getAll().subscribe();
  }

  addCompany() {
    const dialogRef = this.dialog.open(CompanyFormComponent, {
      width: '500px',
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe();
  }

  edit(company: Company): void {
    const dialogRef = this.dialog.open(CompanyFormComponent, {
      width: '500px',
      disableClose: true,
      data: company
    });

    dialogRef.afterClosed().subscribe();
  }
}
