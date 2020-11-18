import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { SaveVehicle } from '../app/models/Vehicle';

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

  GetVehicle(id) {
    return this.http.get('api/vehicle/' + id)
      .pipe(map(res => res));
  }

  Update(vehicle: SaveVehicle) {
    return this.http.put('api/vehicle/' + vehicle.id, vehicle)
      .pipe(map(res => res));

  }

  Delete(id) {
    return this.http.delete('api/vehicle/' + id)
      .pipe(map(res => res));
  }
}
