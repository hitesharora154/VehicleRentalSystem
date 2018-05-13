export class TokenReponse{
    token: string;
    name: string;
    validUpto: Date;
    role: string;
    userID: string;

    constructor(token: string, name: string, validUpto: Date, role: string, userID: string) {
        this.token = token;
        this.name = name;
        this.validUpto = validUpto;
        this.role = role;
        this.userID = userID;
    }
}