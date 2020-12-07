import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import {UserDTO, UserService} from 'src/app/user-management-api';
import { DBOperation } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material/dialog';
import { ManageUserComponent } from '../manage-user/manage-user.component';
import { UtilService } from 'src/app/shared/util.service';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.css']
})

export class ListUsersComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  displayedColumns: string[] = ['firstName', 'lastName', 'age', 'gender', 'emailAddress', 'phoneNumber', 'city', 'state','zip', 'country','edit','delete'];
  data: MatTableDataSource<UserDTO>; 

  constructor(private userService:UserService, public dialog: MatDialog, private util: UtilService) { }

  ngOnInit(): void {
    this.loadUser();
  }

  private loadUser(){
   this.userService.getAll().subscribe(user => {
     this.data = new MatTableDataSource(user.userList);
     this.data.paginator = this.paginator;
     this.data.sort = this.sort;
   })
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.data.filter = filterValue.trim().toLowerCase();
    if (this.data.paginator) {
      this.data.paginator.firstPage();
    }
  }

  private openManageUser(user: UserDTO = null, dbops:DBOperation, modalTitle:string, modalBtnTitle:string)
  {
    let dialogRef = this.util.openCrudDialog(this.dialog, ManageUserComponent, '70%');
    dialogRef.componentInstance.dbops = dbops;
    dialogRef.componentInstance.modalTitle = modalTitle;
    dialogRef.componentInstance.modalBtnTitle = modalBtnTitle;
    dialogRef.componentInstance.user = user;
 
     dialogRef.afterClosed().subscribe(result => {
         this.loadUser();
     });
  }

  public addUser()
  {
    this.openManageUser(null, DBOperation.create, 'Add New User', 'Add');
  }

  public editUser(user: UserDTO)
  {
    this.openManageUser(user, DBOperation.update, 'Update User', 'Update');
  }

  public deleteUser(user: UserDTO)
  {
    this.openManageUser(user, DBOperation.delete, 'Delete User', 'Delete');
  }

}