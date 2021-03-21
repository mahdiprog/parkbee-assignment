import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenStorageService } from '../services/token-storage.service';
import { UserData } from '../shared/user-data.model';
import { GarageDto } from './models/garage-dto.model';

@Injectable({
  providedIn: 'root'
})
export class GarageService {

  constructor(private httpClient: HttpClient,
    private token: TokenStorageService<UserData>,
  ) { }
  getGarageDetails (): Observable<GarageDto> {
    return this.httpClient.get<GarageDto>('/api/garage', {
      headers: { "Authorization": 'Bearer ' + this.token.getToken() }
    });
  }
  refreshDoor (doorId): Observable<boolean> {
    return this.httpClient.post<boolean>(`/api/garage/refresh/${doorId}`,
      { },
      {
        headers: { "Authorization": 'Bearer ' + this.token.getToken() }
      });
  }
}
