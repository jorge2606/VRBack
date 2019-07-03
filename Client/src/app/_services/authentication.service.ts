import { Login } from './../login/login';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {

    private loggedIn = new BehaviorSubject<boolean>(false); // {1}

    constructor(private http: HttpClient,
        private router: Router) {
        if (localStorage.getItem('currentUser')) {
            // logged in so return true
            this.loggedIn.next(true);
        }
    }

    login(user: Login) {
        return this.http.post<any>(environment.apiUrl+'User/Auth', user)
            .pipe(map(this.saveToken));
    }

    saveToken = user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.loggedIn.next(true);
        }
        return user;
    };


    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        
        this.loggedIn.next(false);

        this.router.navigate(['login']);
    }

    get isLoggedIn() {
        return this.loggedIn.asObservable(); // {2}
    }

    userId(key : string){
        let current = localStorage.getItem('currentUser');
        if (current){
            var currentUserJSON = JSON.parse(current);
            return currentUserJSON[key];
        }
    }

    ifLogged(){
        let result : boolean = false;
        if (localStorage.getItem('currentUser')) {
            // logged in so return true
            result = true;
        } 
        return result;       
    }

    urlFile(userId : number, width : number, height: number){
        return environment.apiUrl+"File/"+userId+"/"+width+"/"+height;
    }

    urlExpenditureRefundFile(expId : number, width : number, height: number){
        return environment.apiUrl+"File/ExpenditureRefund/"+expId+"/"+width+"/"+height;
    }

}