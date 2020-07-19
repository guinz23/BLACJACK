import { Injectable,Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LocalStorageService } from './localstorage.services';
import { Deck } from '../interfaces/deck.interface';
import { async } from '@angular/core/testing';
import { Draw } from '../interfaces/draw.interface';
@Injectable()


export class DrawService {
  public deck:Deck;
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') public baseUrl: string, private _localStorage: LocalStorageService) {

  }
  getAllDeck(count: number) {
    if (count==0) {
      this.deck = this._localStorage.getDeckId();
      return this.httpClient.get<Draw>(this.baseUrl + `api/deck/${this.deck.deck_id}/draw/?count=${count}`);
    } else {
      this.deck = this._localStorage.getDeckId();
      return this.httpClient.get<Draw>(this.baseUrl + `api/deck/${this.deck.deck_id}/draw/?count=${count}`);
    }
  }
}