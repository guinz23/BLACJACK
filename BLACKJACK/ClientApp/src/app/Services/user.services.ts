import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../interfaces/user.interface';

@Injectable()

export class UserService {

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') public baseUrl: string) {

  }
  saveUser(user: User) {
    return this.httpClient.post<User>(this.baseUrl + 'api/user',user, this.headers());
  }
  private headers() {
    return {
      headers: { "Accept": "application/json"}
    }
  }
}
