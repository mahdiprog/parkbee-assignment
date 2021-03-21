import { Component, OnInit } from '@angular/core';
import { GarageService } from './garage.service';
import { DoorDto } from './models/door-dto.model';
import { GarageDto } from './models/garage-dto.model';

@Component({
  selector: 'app-counter-component',
  templateUrl: './garage.component.html'
})
export class GarageComponent implements OnInit {
  garage: GarageDto
  constructor(private garageService: GarageService) { }
  ngOnInit (): void {
    this.garageService.getGarageDetails().subscribe(data => this.garage = data);
  }
  refreshDoorStatus (doorId: number) {
    this.garageService.refreshDoor(doorId).subscribe(data => {
      this.garage.doors.find(d => d.doorId === doorId).isOnline = data;
    });
  }
  trackByIdentity (index: number, item: DoorDto) {
    return item.doorId;
  };
}
