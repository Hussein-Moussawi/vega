import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { SaveVehicle } from '../app/models/Vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private readonly vehiclesEndPoint = 'api/vehicle/';

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
    return this.http.post(this.vehiclesEndPoint, vehicle)
      .pipe(map(res => res));;
  }

  GetVehicle(id) {
    return this.http.get(this.vehiclesEndPoint + id)
      .pipe(map(res => res));
  }

  Update(vehicle: SaveVehicle) {
    return this.http.put(this.vehiclesEndPoint + vehicle.id, vehicle)
      .pipe(map(res => res));

  }

  Delete(id) {
    return this.http.delete(this.vehiclesEndPoint + id)
      .pipe(map(res => res));
  }

  GetVehicles(filter) {
    return this.http.get(this.vehiclesEndPoint + '?' + this.ToQueryString(filter))
      .pipe(map(res => res));
  }

  ToQueryString(obj) {
    var parts = [];
    for (var prop in obj) {
      var value = obj[prop];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }
}
