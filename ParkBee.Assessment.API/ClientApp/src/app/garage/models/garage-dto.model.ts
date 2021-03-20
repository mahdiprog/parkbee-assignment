import { DoorDto } from './door-dto.model';

export class GarageDto {
  public garageId: number = 0;

  public name: string = '';

  public address: string = '';

  public doors: DoorDto[] = [];

  constructor(init?: Partial<GarageDto>) {
    Object.assign(this, init);
  }
}
