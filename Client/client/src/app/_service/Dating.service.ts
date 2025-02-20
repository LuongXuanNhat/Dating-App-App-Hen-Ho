import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DatingService {
  updateDatingProfile(datingProfile: FormData) {
    return this.http.post(this.baseUrl + 'DatingProfile', datingProfile);
  }
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  GetUserInterests() {
    return this.http.get(this.baseUrl + 'DatingProfile/GetUserInterests');
  }
  GetWhereToDate() {
    return this.http.get(this.baseUrl + 'DatingProfile/GetWhereToDate');
  }
  GetHeight() {
    return this.http.get(this.baseUrl + 'DatingProfile/GetHeight');
  }
  GetDatingObject() {
    return this.http.get(this.baseUrl + 'DatingProfile/GetDatingObject');
  }
}
