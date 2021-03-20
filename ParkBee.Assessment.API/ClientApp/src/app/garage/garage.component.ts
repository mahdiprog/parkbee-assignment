import { Component, OnInit } from '@angular/core';
import { GarageService } from './garage.service';
import { GarageDto } from './models/garage-dto.model';

@Component({
  selector: 'app-counter-component',
  templateUrl: './garage.component.html'
})
export class GarageComponent implements OnInit{
  garage :GarageDto
  constructor(public garageService: GarageService) { }
  ngOnInit (): void {
    this.garageService.getGarageDetails().subscribe(data => this.garage = data);
    debugger
  }

}
