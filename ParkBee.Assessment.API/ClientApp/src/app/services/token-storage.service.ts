import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LocalStorage } from '../shared/local-storage';
import { SessionStorage } from '../shared/session-storage';
import { UserData } from '../shared/user-data.model';

const TOKEN_KEY = 'AuthToken';
const REFRESH_TOKEN_KEY = 'RefreshToken';
const EXPIRE_KEY = 'ExpIn';
const PROVIDER_KEY = 'Provider';
const USER_DATA = 'user-data';
const SESSION_ID_KEY = 'SessionId';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService<UserModel extends UserData> {
  private tokenChangedSubject: BehaviorSubject<string> = new BehaviorSubject('');
  tokenChanged: Observable<string> = this.tokenChangedSubject.asObservable();

  constructor(@Inject(PLATFORM_ID) private platformId: Object,
    @Inject(LocalStorage) private localStorage: LocalStorage,
    @Inject(SessionStorage) private sessionStorage: SessionStorage) {
    if (isPlatformBrowser(this.platformId)) {
      if (!localStorage) {
        this.localStorage = window.localStorage;
      }
      if (!sessionStorage) {
        this.sessionStorage = window.sessionStorage;
      }
    }
  }

  signOut() {
    this.sessionStorage.removeItem(TOKEN_KEY);
    this.sessionStorage.removeItem(EXPIRE_KEY);
    this.sessionStorage.removeItem(USER_DATA);
    this.localStorage.removeItem(REFRESH_TOKEN_KEY);
    this.localStorage.removeItem(PROVIDER_KEY);
  }

  public saveToken(token: string) {
    this.sessionStorage.removeItem(TOKEN_KEY);
    this.sessionStorage.setItem(TOKEN_KEY, token);
    this.tokenChangedSubject.next(token);
  }

  public getToken(): string|null {
    return this.sessionStorage.getItem(TOKEN_KEY);
  }
  public getRefreshToken(): string|null {
    return this.localStorage.getItem(REFRESH_TOKEN_KEY);
  }
  public getProvider(): string|null {
    return this.localStorage.getItem(PROVIDER_KEY);
  }
  public saveRefreshToken(token: string) {
    this.localStorage.removeItem(REFRESH_TOKEN_KEY);
    this.localStorage.setItem(REFRESH_TOKEN_KEY, token);
  }
  public saveProvider(provider: string) {
    this.localStorage.removeItem(PROVIDER_KEY);
    this.localStorage.setItem(PROVIDER_KEY, provider);
  }
  public saveExpiry (expiration: string) {
    this.sessionStorage.removeItem(EXPIRE_KEY);
    this.sessionStorage.setItem(EXPIRE_KEY, expiration);
  }
  public isExpired(): boolean {
    const expireItem = sessionStorage.getItem(EXPIRE_KEY);
    if (!expireItem)
    return true;
    const expirationDate = new Date(expireItem);
    return new Date() > expirationDate;
  }
  public saveUserData(userData: UserModel) {
    this.sessionStorage.setItem(USER_DATA, JSON.stringify(userData));
  }
  public getUserData(): UserModel|null {
    if (!this.sessionStorage.getItem(USER_DATA))
    return null;
    const userData: UserModel = JSON.parse(
      this.sessionStorage.getItem(USER_DATA)!
    ) as UserModel;
    return userData;
  }
  public saveSessionId(sessionId: string) {
    this.sessionStorage.removeItem(SESSION_ID_KEY);
    this.sessionStorage.setItem(SESSION_ID_KEY, sessionId);
  }

  public getSessionId(): string|null {
    return this.sessionStorage.getItem(SESSION_ID_KEY);
  }
  public isSignedIn(): boolean {
    return (
      this.sessionStorage.getItem(TOKEN_KEY) !== null &&
      this.sessionStorage.getItem(TOKEN_KEY) !== '' &&
      this.sessionStorage.getItem(TOKEN_KEY) !== 'undefined' && !this.isExpired()
    );
  }
}
