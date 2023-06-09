import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import * as moment from 'moment';
import { Report } from 'src/app/shared/Interface/report';
import { SaleService } from 'src/app/shared/service/sale.service';
import { UtilityService } from 'src/app/shared/service/utility.service';

import * as XLSX from 'xlsx'; //ใช้ทำ Report

export const MY_DATA_FORMAT = {
  parse: {
    dataInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css'],
  providers: [{ provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMAT }],
})
export class ReportComponent implements OnInit, AfterViewInit {
  formGroup: FormGroup;
  listReport: Report[] = [];
  displayedColumns: string[] = [
    'createDate',
    'documentNumber',
    'paymentMethod',
    'total',
    'product',
    'quantity',
    'price',
    'totalProduct',
  ];

  dataSource = new MatTableDataSource<Report>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private saleService: SaleService,
    private utService: UtilityService
  ) {
    this.formGroup = this.fb.group({
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
    });
  }

  ngOnInit(): void {}
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  saleSearch() {
    const startDate = moment(this.formGroup.value.startDate).format(
      'DD/MM/YYYY'
    );
    const endDate = moment(this.formGroup.value.endDate).format('DD/MM/YYYY');

    if (startDate === 'Invalid Date' || endDate === 'Invalid Date') {
      this.utService.showAlert('you must enter both dates?', 'Oops!');
      return;
    }

    this.saleService.Report(startDate, endDate).subscribe({
      next: (data) => {
        if (data.status) {
          this.listReport = data.value;
          this.dataSource.data = data.value;
        } else {
          this.listReport = [];
          this.dataSource.data = [];
          this.utService.showAlert('No Data', 'Oops!');
        }
      },
      error: (e) => {},
    });
  }

  exportExcel() {
    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.json_to_sheet(this.listReport);

    XLSX.utils.book_append_sheet(wb, ws, 'Report');
    XLSX.writeFile(wb, 'Report Sales.xlsx');
  }
}
