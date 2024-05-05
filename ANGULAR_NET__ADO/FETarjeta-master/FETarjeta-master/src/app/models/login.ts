export class Login {
    UserName: string;
    Password: string;
    constructor(data: any) {
        this.UserName = data.UserName;
        this.Password = data.Password;
    }    
}
  