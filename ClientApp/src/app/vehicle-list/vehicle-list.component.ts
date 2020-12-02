import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { Vehicle, KeyValuePair } from '../models/Vehicle';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {

  vehicles: any;
  makes: any;
  filter: any = {};

  constructor(private vehicleService : VehicleService) { }

  ngOnInit(): void {

    this.vehicleService.getMakes().subscribe(makes => this.makes = makes);

    this.PopulateVehicles();

  }

  PopulateVehicles() {
    this.vehicleService.GetVehicles(this.filter).subscribe(vehicles => this.vehicles = vehicles);
  }

  ResetFilter() {
    this.filter = {};
    this.PopulateVehicles();
  }

}
