import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/shared/Interface/user';
import { UserService } from 'src/app/shared/service/user.service';
import { UtilityService } from 'src/app/shared/service/utility.service';
import Swal from 'sweetalert2';
import { UserDialogComponent } from '../../dialogs/user-dialog/user-dialog.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'FullName',
    'Email',
    'RoleName',
    'isActive',
    'Actions',
  ];
  dataSource = new MatTableDataSource<User>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private utService: UtilityService
  ) {}

  GetTableList() {
    this.userService.GetList().subscribe({
      next: (data) => {
        if (data.status) {
          this.dataSource.data = data.value;
        } else {
          this.utService.showAlert(data.message, 'Oops!');
        }
      },
      error: (e) => {},
    });
  }
  ngOnInit(): void {
    this.GetTableList();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  newUser() {
    this.dialog
      .open(UserDialogComponent, {
        disableClose: true,
      })
      .afterClosed()
      .subscribe((result) => {
        if (result === 'true') {
          this.GetTableList();
        }
      });
  }

  updateUser(user: User) {
    this.dialog
      .open(UserDialogComponent, {
        disableClose: true,
        data: user,
      })
      .afterClosed()
      .subscribe((result) => {
        if (result === 'true') {
          this.GetTableList();
        }
      });
  }

  deleteUser(user: User) {
    Swal.fire({
      title: 'Do you want delete user?',
      text: user.fullName,
      icon: 'warning',
      confirmButtonColor: '#3085d6',
      confirmButtonText: 'Yes',
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No',
    }).then((result) => {
      if (result.isConfirmed) {
        this.userService.Delete(user.userId).subscribe({
          next: (data) => {
            if (data.status) {
              this.utService.showAlert(data.message, 'success');
              this.GetTableList();
            } else {
              this.utService.showAlert(data.message, 'Error');
            }
          },
          error: (e) => {},
        });
      }
    });
  }
}
