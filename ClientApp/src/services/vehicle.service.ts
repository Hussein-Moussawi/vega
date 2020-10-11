import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get('api/makes')
      .pipe(map(res => res));
  }

  GetFeatures() {

    return this.http.get('api/feature')
      .pipe(map(res => res));
  }

  Create(vehicle) {
    return this.http.post('api/vehicle', vehicle)
      .pipe(map(res => res));;
  }
}
