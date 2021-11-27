import { User } from "./User";

export class UserParams{
    pageNo=1;
    pageSize=5;
    gender:string;
    minAge=18;
    maxAge=99;
    orderBy="lastActive";

    constructor(user:User) {
        
       this.gender=user.gender==="male"?"female":"male";
        
    }

    getValuesAsString(){
        return `${this.pageNo}-${this.pageSize}=${this.gender}-${this.minAge}-${this.maxAge}-${this.orderBy}`;
    }
}