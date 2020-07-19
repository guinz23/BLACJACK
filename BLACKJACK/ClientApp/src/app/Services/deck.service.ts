import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Deck } from '../interfaces/deck.interface';
import { LocalStorageService } from '../Services/localstorage.services'

@Injectable()

export class DeckService {
  public deck: Deck;
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') public baseUrl: string, private _localStorage: LocalStorageService) {

  }
   createDeck() {
    let count = 1;
    return this.httpClient.get<Deck>(this.baseUrl + `api/deck/?deck_count=${count}`);
   }
  shuffle() {
    this.deck = this._localStorage.getDeckId()
    this.deck = {
      deck_id: this.deck.deck_id,
      remaining: this.deck.remaining,
      shuffled: true,
      success: this.deck.success,
    };
    return this.httpClient.put<Deck>(this.baseUrl + `api/deck/${this.deck.deck_id}/shuffle`,this.deck, this.headers());
  }
  private headers() {
    return {
      headers: { "Accept": "application/json" }
    }
  }
}