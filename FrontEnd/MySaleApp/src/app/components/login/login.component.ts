import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/shared/Interface/login';
import { UserService } from 'src/app/shared/service/user.service';
import { UtilityService } from 'src/app/shared/service/utility.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  formLogin: FormGroup;
  checkPassword: boolean = true;
  showLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private _userService: UserService,
    private utService: UtilityService
  ) {
    this.formLogin = this.fb.group({
      email: ['', Validators.email],
      password: ['', Validators.required],
    });
  }
  ngOnInit(): void {}

  LoginUser() {
    this.showLoading = true;

    const req: Login = {
      email: this.formLogin.value.email,
      password: this.formLogin.value.password,
    };

    this._userService.Login(req).subscribe({
      next: (data) => {
        if (data.status) {
          localStorage.setItem('token', data.value.token);
          this.utService.setSessionUser(data.value);
          this.router.navigate(['pages']);
        } else {
          this.utService.showAlert(data.message, 'Oops!');
        }
      },
      complete: () => {
        this.showLoading = false;
      },
      error: () => {
        this.utService.showAlert('Error', 'Oops!');
      },
    });
  }
}
