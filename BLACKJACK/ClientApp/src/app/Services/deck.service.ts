import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Deck } from '../interfaces/deck.interface';


@Injectable()

export class DeckService {

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') public baseUrl: string) {

  }
   createDeck() {
    let count = 1;
    return this.httpClient.get<Deck>(this.baseUrl + `api/deck/?deck_count=${count}`);
  }
}