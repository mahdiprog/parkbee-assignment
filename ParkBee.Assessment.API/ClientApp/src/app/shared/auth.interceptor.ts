import {
  HttpErrorResponse,
  HttpEvent, HttpHandler, HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import {
  catchError,

  filter, finalize,
  mergeMap,
  switchMap,
  take
} from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { TokenStorageService } from '../services/token-storage.service';
import { LoginResponse } from './login-response.model';
import { UserData } from './user-data.model';


const TOKEN_HEADER_KEY = 'Authorization';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  isRefreshingToken = false;
  tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(
    private token: TokenStorageService<UserData>,
    private authService: AuthService,
    private router: Router,
  ) { }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.headers.keys().includes('no-auth')) {
      req = req.clone({
        headers: req.headers.delete('no-auth')
      });

      return next.handle(req);
    }

    const token = this.token.getToken();

    if (token != null && !this.token.isExpired()) {
      return this.getRequestWithAuthentication(req, next, token).pipe(
        catchError((error: Error, caught: Observable<HttpEvent<any>>) => {
          if (error instanceof HttpErrorResponse) {
            if (error.status === 401) {
              this.tryRefreshingToken(req, next);
            }
            if (error.status === 403) {
              this.router.navigate(['auth/access-denied']);
            }
          }

          return throwError(error);
        })
      );
    } else {
        return next.handle(req).pipe(
          catchError((error: Error, caught: Observable<HttpEvent<any>>) => {
            if (error instanceof HttpErrorResponse) {
              if (error.status === 401) {
                return this.tryRefreshingToken(req, next);
              }
              if (error.status === 403) {
                this.router.navigate(['auth/access-denied']);
              }
            }

            return throwError(error);
          })
        );

    }
  }

  private getRequestWithAuthentication(
    request: HttpRequest<any>,
    next: HttpHandler,
    accessToken: string
  ): Observable<HttpEvent<any>> {

    const req = request.clone({
      headers: request.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + accessToken)
    });
    return next.handle(req);
  }

  tryRefreshingToken(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<any> {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;
      // Reset here so that the following requests wait until the token
      // comes back from the refreshToken call.
      this.tokenSubject.next('');

      return this.authService
        .getToken('test','test')
        .pipe(
          mergeMap((newTokens: LoginResponse) => {
             debugger
            if (newTokens) {
              this.token.saveToken(newTokens.token);
              this.token.saveExpiry(newTokens.expiration);
              this.tokenSubject.next(newTokens.token);

              return this.getRequestWithAuthentication(
                req,
                next,
                newTokens.token
              );
            }

            // If we don't get a new token, we are in trouble so logout.
            this.token.signOut();
            this.router.navigate(['auth/login']);
            return of(null);
          }),
          finalize(() => {
            this.isRefreshingToken = false;
          })
        );
    } else {
      return this.tokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(token => {
          return this.getRequestWithAuthentication(req, next, token);
        })
      );
    }
  }
}
