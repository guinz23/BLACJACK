import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeckService } from '../Services/deck.service';
import { UserService } from '../Services/user.services';
import { LocalStorageService } from '../Services/localstorage.services';
import { DrawService } from '../Services/draw.service';
import { Deck } from '../interfaces/deck.interface';
import { User } from '../interfaces/user.interface'
import { Local } from 'protractor/built/driverProviders';
import Swal from 'sweetalert2';
import { trigger, transition, style, animate, query, stagger } from '@angular/animations';
import { Draw } from '../interfaces/draw.interface';
import { interval } from 'rxjs';
import { take } from 'rxjs/operators';
  
@Component({
  selector: 'app-deck',
  templateUrl: './deck.component.html',
  animations: [
    trigger('listAnimation', [
      transition('* => *', [ // each time the binding value changes
        query(':leave', [
          stagger(100, [
            animate('0.5s', style({ opacity: 0 }))
          ])
        ], { optional: true }),
        query(':enter', [
          style({ opacity: 0 }),
          stagger(100, [
            animate('0.5s', style({ opacity: 1 }))
          ])
        ], { optional: true })
      ])
    ])
  ],
  styleUrls: ['./deck.component.css']
})
export class DeckComponent {
  public itemsplayer = [];
  public itemsCrupier = [];
  public decks: Deck[]; 
  public deck: Deck;
  public showForm = false;
  public showBoard = false;
  public showMainForm = true;
  public starGame = false;
  public hiddenStart = true;
  public user: User;
  public draw: Draw;
  public deal=true;
  public totalpointsPlayer: number;
  public totalpointsCrupier: number;
  public gameswon: number = 0;
  public lostGames: number = 0;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private _deckService: DeckService, private _userService: UserService, private _localStorage: LocalStorageService, private drawServices: DrawService) {
    this.user = {
      id: "00000000-0000-0000-0000-000000000000",
      username: "",
      total_money: 0
    }
  }
  createDeck() {
    this._deckService.createDeck().subscribe(result => {
      this._localStorage.saveDeckId(result);
      this.showForm = false;
    });
  }
  saveUser() {
    this._userService.saveUser(this.user).subscribe(result => {
   if (this._localStorage.saveCurrentPlayer(result)) {
     const Toast = Swal.mixin({
       toast: true,
       position: 'top-end',
       showConfirmButton: false,
       timer: 3000,
       timerProgressBar: true,
       onOpen: (toast) => {
         toast.addEventListener('mouseenter', Swal.stopTimer)
           toast.addEventListener('mouseleave', Swal.resumeTimer)
          }
        })

        Toast.fire({
          icon: 'success',
          title: 'Usuario Agregado correctamente'
        })
      }
      this.createDeck();
      this.user.username = null;
      this.user.total_money = null;
      this.user = this._localStorage.getCurrentPlayer();
      this.showForm = false;
      this.showBoard = true;
      this.showMainForm = false;
    });
  }
  initGame() {
    this.drawServices.getAllDeck(2).subscribe(result => {
     // console.log(result);
    });
    this.starGame = true;
    this.hiddenStart = false;
    this.timer();
  }

  dealOptionPlayer() {
    this.drawServices.getAllDeck(2).subscribe(
      result => {
        if (result.remaining > 0) {
          this.showItems(result['cards'], 'crupier');
        } else {
          Swal.fire({
            icon: 'error',
            title: 'El mazo no posee mas cartas',
          });
        }
      });

  }
  dealOptionCrupier() {
    this.drawServices.getAllDeck(2).subscribe(
      result => {
        if (result.remaining > 0) {
          this.showItems(result['cards'], 'player');
        } else {
          Swal.fire({
            icon: 'error',
            title: 'El mazo no posee mas cartas',
          });
        }
      });
  }

  dealOption() {
    this.dealOptionPlayer();
    this.dealOptionCrupier();
    this.deal = false;

  }

  hitOption() {
    if (this.totalpointsPlayer < 21) {
      this.drawServices.getAllDeck(1).subscribe(
        result => {
          if (result.remaining > 0) {
            this.showItems(result['cards'], 'player');
          } else {
            Swal.fire({
              icon: 'error',
              title: 'El mazo no posee mas cartas',
            });
          }
        });
    } else {
    }
  }
  standOption() {
   this.pointsCardCrupier();
    if (this.totalpointsCrupier <= 21 && this.totalpointsPlayer < this.totalpointsCrupier) {
      Swal.fire({
        icon: 'error',
        title: 'You Lost',
      });
      this.lostGames = this.lostGames + 1;
      this.hideItems();
      this.deal = true;
    } else if (this.totalpointsPlayer <= 21 && this.totalpointsPlayer > this.totalpointsCrupier) {
      Swal.fire({
        icon: 'success',
        title: 'You Win',
      });
      this.wager(25);
      this.gameswon = this.gameswon + 1;
      this.hideItems();
      this.deal = true;
    } else if (this.totalpointsCrupier >= 21 && this.totalpointsPlayer < this.totalpointsCrupier) {
      Swal.fire({
        icon: 'success',
        title: 'You Win',
      });
      this.wager(25);
      this.gameswon = this.gameswon + 1;
      this.hideItems();
      this.deal = true;
    } else if (this.totalpointsPlayer >= 21 && this.totalpointsCrupier < this.totalpointsPlayer) {
      Swal.fire({
        icon: 'error',
        title: 'You Lost',
      });
      this.lostGames = this.lostGames + 1;
      this.hideItems();
      this.deal = true;
    } else {
      this.hideItems();
      this.deal = true;
    }
  }

  logAnimation(_event) {
  }
  showItems(array, type: string) {
    if (type === 'player') {
      array.map((i) => {
        this.itemsplayer.push(i)
      });
    } else if(type === 'crupier') {
      array.map((i) => {
        this.itemsCrupier.push(i)
      });
    }
  }

  hideItems() {
    this.itemsplayer = [];
    this.itemsCrupier = [];
  }

  toggle() {
    this.itemsplayer.length ? this.hideItems() : this.showItems(0,'player');
  }
  pointsCardPlayer() {
    let total = 0;
    for (const prop in this.itemsplayer) {
      total += parseInt(this.itemsplayer[prop].value);
    }
    return total;
  }

  pointsCardCrupier() {
    let total = 0;
    console.log(this.itemsCrupier);
    for (const prop in this.itemsCrupier) {
      total += parseInt(this.itemsCrupier[prop].value);
    }
    this.totalpointsCrupier = total;
    return total;
  }
  timer() {
    const timer = interval(1000);
    return timer.subscribe(x => this.totalpointsPlayer = this.pointsCardPlayer());
  }
  mixCards() {
    this._deckService.shuffle().subscribe(result => {
      if (result.shuffled == true) {
        Swal.fire(
          'Exito!',
          'Las cartas se revolvieron exitosamente',
          'success'
        );
      }
    });
  }
  wager(value) {
    let totalmoney = this._localStorage.getCurrentPlayer().total_money;
    let wager = value >= 25 ? (totalmoney) + (25*2) : (totalmoney) - 25;
    this.user = {
      id: this._localStorage.getCurrentPlayer().id,
      username: this._localStorage.getCurrentPlayer().username,
      total_money: wager,
    }
   this._localStorage.saveCurrentPlayer(this.user);
  }
}
