import { Component, Inject, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormField, MatInputModule, MatLabel } from '@angular/material/input';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CompaniesService, Company } from '../companies.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-company-form',
  imports: [
    ReactiveFormsModule,
    MatFormField,
    MatFormFieldModule,
    MatLabel,
    MatInputModule,
    MatDialogContent,
    MatDialogTitle,
    MatDialogActions,
    MatButton,
    MatDialogClose
  ],
  templateUrl: './company-form.component.html',
  styleUrl: './company-form.component.scss'
})
export class CompanyFormComponent implements OnInit {
  private dialogRef = inject(MatDialogRef<CompanyFormComponent>);
  private companiesService = inject(CompaniesService);

  formGroup: FormGroup = new FormGroup({
    name: new FormControl("", Validators.required),
    ticker: new FormControl("", Validators.required),
    exchange: new FormControl("", Validators.required),
    isin: new FormControl("", Validators.required),
    website: new FormControl(""),
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data?: Company) {}

  ngOnInit(): void {
    if (this.data) this.formGroup.patchValue(this.data);
  }

  submit() {
    if (!this.formGroup.valid) return;

    const rawValue = this.formGroup.value;

    if (this.data) {
      this.companiesService
        .update({
          id: this.data.id,
          exchange: rawValue.exchange,
          isin: rawValue.isin,
          name: rawValue.name,
          ticker: rawValue.ticker,
          website: rawValue.website
        })
        .pipe(finalize(() => this.dialogRef.close()))
        .subscribe();
    } else {
      this.companiesService
        .create({
          id: '',
          exchange: rawValue.exchange,
          isin: rawValue.isin,
          name: rawValue.name,
          ticker: rawValue.ticker,
          website: rawValue.website
        })
        .pipe(finalize(() => this.dialogRef.close()))
        .subscribe();
    }
  }
}
