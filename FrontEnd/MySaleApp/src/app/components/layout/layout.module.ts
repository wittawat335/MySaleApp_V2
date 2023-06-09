import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { UserComponent } from './pages/user/user.component';
import { SaleComponent } from './pages/sale/sale.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ProductComponent } from './pages/product/product.component';
import { ReportComponent } from './pages/report/report.component';
import { SaleHistoryComponent } from './pages/sale-history/sale-history.component';
import { SharedModule } from 'src/app/shared/shared/shared.module';
import { ProductDialogComponent } from './dialogs/product-dialog/product-dialog.component';
import { UserDialogComponent } from './dialogs/user-dialog/user-dialog.component';
import { SaleDetailDialogComponent } from './dialogs/sale-detail-dialog/sale-detail-dialog.component';

@NgModule({
  declarations: [
    UserComponent,
    SaleComponent,
    DashboardComponent,
    ProductComponent,
    ReportComponent,
    SaleHistoryComponent,
    ProductDialogComponent,
    UserDialogComponent,
    SaleDetailDialogComponent,
  ],
  imports: [CommonModule, LayoutRoutingModule, SharedModule],
})
export class LayoutModule {}
