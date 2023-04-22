import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Category } from 'src/app/shared/Interface/category';
import { Product } from 'src/app/shared/Interface/product';
import { CategoryService } from 'src/app/shared/service/category.service';
import { ProductService } from 'src/app/shared/service/product.service';
import { UtilityService } from 'src/app/shared/service/utility.service';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.css'],
})
export class ProductDialogComponent implements OnInit {
  formGroup: FormGroup;
  checkPassword: boolean = true;
  titleAction: string = 'New';
  buttonAction: string = 'Save';
  listCategory: Category[] = [];

  constructor(
    private dialog: MatDialogRef<ProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public productData: Product,
    private fb: FormBuilder,
    private cateService: CategoryService,
    private productService: ProductService,
    private utService: UtilityService
  ) {
    this.formGroup = this.fb.group({
      name: ['', Validators.required],
      idCategory: ['', Validators.required],
      stock: ['', Validators.required],
      price: ['', Validators.required],
      isActive: ['1', Validators.required],
    });

    if (this.productData != null) {
      this.titleAction = 'Edit';
      this.buttonAction = 'Update';
    }
    //Get Value Dropdownlist
    this.cateService.GetList().subscribe({
      next: (data) => {
        if (data.status) this.listCategory = data.value;
      },
      error: (e) => {},
    });
  }

  ngOnInit(): void {
    this.checkAvailableData();
  }

  actionProduct() {
    const product: Product = {
      productId: this.productData == null ? 0 : this.productData.productId,
      name: this.formGroup.value.name,
      categoryId: this.formGroup.value.idCategory,
      stock: this.formGroup.value.stock,
      price: this.formGroup.value.price,
      isActive: parseInt(this.formGroup.value.isActive),
    };
    if (this.productData == null) {
      this.productService.Create(product).subscribe({
        next: (data) => {
          console.log(data);
          if (data.status) {
            this.utService.showAlert(data.message, 'success');
            this.dialog.close('true');
          } else {
            this.utService.showAlert(data.message, 'error');
          }
        },
        error: (e) => {},
      });
    } else {
      this.productService.Update(product).subscribe({
        next: (data) => {
          if (data.status) {
            this.utService.showAlert(data.message, 'success');
            this.dialog.close('true');
          } else {
            this.utService.showAlert(data.message, 'error');
          }
        },
        error: (e) => {},
      });
    }
  }

  checkAvailableData() {
    if (this.productData != null) {
      this.formGroup.patchValue({
        name: this.productData.name,
        idCategory: this.productData.categoryId,
        stock: this.productData.stock,
        price: this.productData.price,
        isActive: this.productData.isActive.toString(),
      });
    }
  }
}
