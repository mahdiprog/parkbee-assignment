
export class DoorDto {
  public doorId: number = 0;

  public name: string = '';

  public isOnline: boolean = false;

  constructor(init?: Partial<DoorDto>) {
    Object.assign(this, init);
  }
}
