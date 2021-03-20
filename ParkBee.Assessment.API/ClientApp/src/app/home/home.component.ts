import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { TokenStorageService } from '../services/token-storage.service';
import { UserData } from '../shared/user-data.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  constructor(private authService: AuthService,
    private token: TokenStorageService<UserData>,
  ) { }
  ngOnInit (): void {
    this.authService.getToken('test', 'test').subscribe(resp => {
      this.token.saveToken(resp.token);
      this.token.saveExpiry(resp.expiration);
    })
  }
}
