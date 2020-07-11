import { Injectable } from '@angular/core';
import { User } from '../interfaces/user.interface';
import { Deck } from '../interfaces/deck.interface';


@Injectable()

export class LocalStorageService {

  public userKey: string;
  public deckKey: string;
  public user: User;
  public deck: Deck;
  constructor() {
    this.userKey = "user";
    this.deckKey = "deck";
  }

  saveCurrentPlayer(user) {
    localStorage.setItem(this.userKey, JSON.stringify(user));
    return true;
  }

   getCurrentPlayer() {
    this.user = JSON.parse(localStorage.getItem(this.userKey));
     return this.user;
  }
   saveDeckId(deck) {
     localStorage.setItem(this.deckKey, JSON.stringify(deck));
  }
   getDeckId() {
    this.deck = JSON.parse(localStorage.getItem(this.deckKey));
     return this.deck;
  }
}
