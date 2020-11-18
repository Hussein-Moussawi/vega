import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from '@angular/compiler/src/util';
import { forkJoin } from 'rxjs';
import { Vehicle, SaveVehicle } from '../models/Vehicle';
import * as _ from 'underscore';
import {ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any;
  models: any[];
  features: any;
  vehicle: SaveVehicle = {
    id : 0,
    makeId : 0,
    modelId: 0,
    isRegistered : false,
    features: [],
    contact: {
      name: '',
      phone: '',
      email: ''
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastrService: ToastrService) {

    route.params.subscribe(p => {
      this.vehicle.id = +p['id'];
    })
  }

    
  

  ngOnInit() {

    var sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.GetFeatures()
    ];

    if (this.vehicle.id)
      sources.push(this.vehicleService.GetVehicle(this.vehicle.id));

    var values$ = forkJoin(sources)
      .subscribe(data => {
        this.makes = data[0];
        this.features = data[1];
        if (this.vehicle.id) {
          this.SetVehicle(<Vehicle>data[2]);
          this.PopulateModels();
        }
      },
        err => {
          if (err.status == 404)
            this.router.navigate(['']);
        }
      );
  }

  OnMakeChange() {
    this.PopulateModels();
    delete (this.vehicle.modelId);
  }

  ToggleFeaters(featureId, $event) {
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  Submit() {

    if (this.vehicle.id) {
      this.vehicleService.Update(this.vehicle).subscribe(x => {
        this.toastrService.success('Vehicle Saved Sucessfully', 'Save', {
          timeOut: 3000,
          positionClass: 'top-left',
        });
      });
    }
    else {
      this.vehicleService.Create(this.vehicle)
        .subscribe(
          x => console.log(x),
        );
    }
  }

  Delete() {
    if (confirm("are you sure?")) {
      this.vehicleService.Delete(this.vehicle.id)
        .subscribe(x => this.router.navigate(['']));
    }
  }

  private PopulateModels() {
    var selectedMakes = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMakes ? selectedMakes.models : [];
  }

  private SetVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');

  }

 }


