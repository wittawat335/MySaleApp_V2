import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Guid } from 'guid-typescript';
import { Role } from 'src/app/shared/Interface/role';
import { User } from 'src/app/shared/Interface/user';
import { RoleService } from 'src/app/shared/service/role.service';
import { UserService } from 'src/app/shared/service/user.service';
import { UtilityService } from 'src/app/shared/service/utility.service';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css'],
})
export class UserDialogComponent implements OnInit {
  formUser: FormGroup;
  checkPassword: boolean = true;
  titleAction: string = 'New';
  buttonAction: string = 'Save';
  listRole: Role[] = [];
  test: Guid = Guid.parse('0000-00000-00000-00000');

  constructor(
    private dialog: MatDialogRef<UserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dataUser: User,
    private fb: FormBuilder,
    private roleService: RoleService,
    private userService: UserService,
    private utService: UtilityService
  ) {
    this.formUser = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', Validators.email],
      idRole: ['', Validators.required],
      password: ['', Validators.required],
      isActive: ['1', Validators.required],
    });

    if (this.dataUser != null) {
      this.titleAction = 'Edit';
      this.buttonAction = 'Update';
    }
    this.roleService.GetList().subscribe({
      next: (data) => {
        if (data.status) this.listRole = data.value;
      },
      error: (e) => {},
    });
  }
  ngOnInit(): void {
    this.pushData();
  }

  pushData() {
    if (this.dataUser != null) {
      this.formUser.patchValue({
        fullName: this.dataUser.fullName,
        email: this.dataUser.email,
        idRole: this.dataUser.roleId,
        password: this.dataUser.password,
        isActive: this.dataUser.isActive.toString(),
      });
    }
  }

  updateUser() {
    const userData: User = {
      userId:
        this.dataUser == null
          ? '00000000-0000-0000-0000-000000000000'
          : this.dataUser.userId,
      fullName: this.formUser.value.fullName,
      email: this.formUser.value.email,
      roleId: this.formUser.value.idRole,
      password: this.formUser.value.password,
      isActive: parseInt(this.formUser.value.isActive),
    };

    if (this.dataUser == null) {
      console.log(userData);
      this.userService.Register(userData).subscribe({
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
    } else {
      console.log(userData);
      this.userService.Update(userData).subscribe({
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
}
