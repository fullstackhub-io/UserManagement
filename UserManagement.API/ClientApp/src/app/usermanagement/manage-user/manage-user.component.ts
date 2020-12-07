import { Component, OnInit } from '@angular/core';
import { AddUserCommand, UpdateUserCommand, UserDTO, UserService } from 'src/app/user-management-api';
import { DBOperation } from 'src/app/shared/enum';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UtilService } from 'src/app/shared/util.service';

interface SelectValue {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.css']
})
export class ManageUserComponent implements OnInit {

  modalTitle: string;
  modalBtnTitle: string;
  dbops: DBOperation;
  user: UserDTO;
  minDate: Date;
  maxDate: Date;

  userFrm: FormGroup = this.fb.group({
    userID: [''],
    firstName: ['', [Validators.required, Validators.max(50)]],
    lastName: ['', [Validators.required, Validators.max(50)]],
    dob: ['', [Validators.required]],
    gender: ['', [Validators.required]],
    emailAddress: ['', [Validators.required, Validators.email]],
    phoneNumber: ['', [Validators.required, Validators.required]],
    city: ['', [Validators.required, Validators.max(100)]],
    state: ['', [Validators.required, Validators.required]],
    zip: ['', [Validators.required, Validators.required]],
    country: ['', [Validators.required]]
  });

  states: SelectValue[] = [
    { value: 'AL', viewValue: 'Alabama' },
    { value: 'AK', viewValue: 'Alaska' },
    { value: 'AS', viewValue: 'American Samoa' },
    { value: 'AZ', viewValue: 'Arizona' },
    { value: 'AR', viewValue: 'Arkansas' },
    { value: 'CA', viewValue: 'California' }
  ];

  countries: SelectValue[] = [
    { value: 'US', viewValue: 'United States' },
    { value: 'CA', viewValue: 'Canada' },
  ];

  gender: SelectValue[] = [
    { value: 'M', viewValue: 'Male' },
    { value: 'F', viewValue: 'Female' },
  ];

  constructor(private utilService: UtilService, private userService: UserService, private fb: FormBuilder, public dialogRef: MatDialogRef<ManageUserComponent>) {
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 60, 0, 1);
    this.maxDate = new Date(currentYear - 10, 11, 31);
  }

  ngOnInit(): void {
    debugger
    if (this.dbops != DBOperation.create)
      this.userFrm.patchValue(this.user);

    if (this.dbops == DBOperation.delete)
      this.userFrm.disable();

    if (this.dbops == DBOperation.update) {
      this.userFrm.controls["firstName"].disable();
      this.userFrm.controls["lastName"].disable();
      this.userFrm.controls["dob"].disable();
      this.userFrm.controls["gender"].disable();
      this.userFrm.controls["emailAddress"].disable();
    }
  }

  onSubmit() {
    switch (this.dbops) {
      case DBOperation.create:
        if (this.userFrm.valid) {
          this.userService.post(<AddUserCommand>{
            firstName: this.userFrm.value.firstName,
            lastName: this.userFrm.value.lastName,
            dob: this.userFrm.value.dob,
            gender: this.userFrm.value.gender,
            emailAddress: this.userFrm.value.emailAddress,
            phoneNumber: this.userFrm.value.phoneNumber,
            city: this.userFrm.value.city,
            state: this.userFrm.value.state,
            zip: this.userFrm.value.zip,
            country: this.userFrm.value.country
          }).subscribe(
            data => {
              if (data > 0) {
                this.utilService.openSnackBar("Successfully added the user!");
                this.dialogRef.close()
              }
              else {
                this.utilService.openSnackBar("Error adding user, contact your system administrator!");
              }
            }
          );
        }
        break;
      case DBOperation.update:
        if (this.userFrm.valid) {
          this.userService.put(<UpdateUserCommand>{
            userID: this.userFrm.value.userID,
            phoneNumber: this.userFrm.value.phoneNumber,
            city: this.userFrm.value.city,
            state: this.userFrm.value.state,
            zip: this.userFrm.value.zip,
            country: this.userFrm.value.country
          }).subscribe(
            data => {
              if (data == true) {
                this.utilService.openSnackBar("Successfully updated the user!");
                this.dialogRef.close()
              }
              else {
                this.utilService.openSnackBar("Error updating user, contact your system administrator!");
              }
            }
          );
        }
        break;
      case DBOperation.delete:
        this.userService.delete(this.userFrm.value.userID).subscribe(
          data => {
            debugger
            if (data == true) {
              this.utilService.openSnackBar("Successfully deleted the user!");
              this.dialogRef.close()
            }
            else {
              this.utilService.openSnackBar("Error deleting user, contact your system administrator!");
            }
          }
        );
        break;
    }
  }
}