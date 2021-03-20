
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { LoginResponse } from '../shared/login-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string;
  constructor(
    private http: HttpClient
  ) { this.baseUrl = '/api/account'; }

  getToken(
    username: string,
    password: string
  ): Observable<LoginResponse> {
      return this.http.post<LoginResponse>(
      this.baseUrl + '/token',
        { username: username, password: password}
    );
  }
  removeToken(userId: string, clientId: string): Observable<any> {
    return this.http.delete(
      this.baseUrl + '/api/tokens/' + clientId + '/' + userId
    );
  }

  refreshToken(
    clientId: string,
    refreshToken: string
  ): Observable<LoginResponse> {
    const options = {
      headers: new HttpHeaders().set(
        'Content-Type',
        'application/x-www-form-urlencoded'
      )
    };
    const body = new URLSearchParams();
    body.set('client_id', clientId);
    body.set('grant_type', 'refresh_token');
    if (refreshToken) {
      body.set('refresh_token', refreshToken);

      return this.http.post<LoginResponse>(
        this.baseUrl + '/connect/tokens',
        body.toString(),
        options
      );
    }
    return EMPTY;
  }


}
