import { Register } from './../_models/register';
import { RoleUserDto } from './../_models/roles';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { User, modifyUser, createUser } from '../users/users';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthenticationService } from './authentication.service';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient, private authenticationService : AuthenticationService) { }

    getAll() {
        return this.http.get<any>(environment.apiUrl+'User/getall');
    }

    getPaginator(filters: any) {
        var param = new HttpParams({
            fromObject :{
                'page' : filters.page,
                'distributionId' : filters.distributionId,
                'dni' : filters.dni == null ? "" : filters.dni.toString(),
                'firstName' : filters.firstName
            }
        });
        return this.http.get<any>(environment.apiUrl+'User/page/',{params: param});
    }

    getById() {
        return this.http.get<modifyUser>(environment.apiUrl+'User/getbyid/');
    }

    getByIdAdministrator(id : number) {
        return this.http.get<modifyUser>(environment.apiUrl+'User/getbyidAdministrator/'+id);
    }

    updateUsers(user: User) : Observable<any> {
        return this.http.put(environment.apiUrl+'User/UpdateProfileAsAdmin/', user);
    }

    updateProfileUsers(user: modifyUser) {
        return this.http.put<any>(environment.apiUrl+'User/UpdateMyProfile', user);
    }

    createWithObjectUser(user: createUser): Observable<any> {
        return this.http.post(environment.apiUrl+'User/Save', user)
        .pipe(
            map(this.authenticationService.saveToken),
            catchError(error => this.handleError(error))
          );
    }

    deleteUser(id: number) {
        return this.http.delete(environment.apiUrl+'User/' + id);
    }

    register(user: Register): Observable<any> {
        return this.http.post<any>(environment.apiUrl+'User/register',user)
        .pipe(
            map(this.authenticationService.saveToken),
            catchError(error => this.handleError(error))
          );
    }
    
    handleError(err : any){
        return throwError(err);
    }

    SaveUserRoles(RoleUserDto: RoleUserDto) {
        this.http.post(environment.apiUrl+'User/SaveRolUser/', RoleUserDto).subscribe(
            data => {
                console.log("POST Request is successful ", data);
            },
            error => {
                console.log("Rrror", error);
            }
        );
    }

    deleteProfilePhoto(id: number, width : number, height : number) {
        return this.http.delete<any>(environment.apiUrl+'File/removePhoto/' + id+"/"+width+"/"+height);
    }

    getHolographSign(userId : number, width : number, height: number) {
        return this.http.get<any>(environment.apiUrl+'File/HolographSignUrl/'+userId+"/"+width+"/"+height);
    }

    getUserImage(userId: number, width : number, height : number) {
        return this.http.get<any>(environment.apiUrl+"File/"+userId+"/"+width+"/"+height);
    }

}