
export class LoginResponse {
  public token: string;
  public expiration: string;
  constructor(
    token?: string,
    expiration?: string
    ) {
    this.token = token || '';
    this.expiration = expiration ||'';
  }
}
