import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any;
  models: any[];
  features: any;
  vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(private vehicleService : VehicleService, private toastrService : ToastrService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(makes => {
      this.makes = makes;

      this.vehicleService.GetFeatures().subscribe(features => {
        this.features = features
      });
    });
  }

  OnMakeChange() {
    var selectedMakes = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMakes ? selectedMakes.models : [];
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
    this.vehicleService.Create(this.vehicle)
      .subscribe(
        x => console.log(x),
        err => {
          this.toastrService.error('everything is broken', 'Major Error', {
            timeOut: 3000,
            positionClass: 'md-toast-top-right',
          });
        }
      );
  }

 }


