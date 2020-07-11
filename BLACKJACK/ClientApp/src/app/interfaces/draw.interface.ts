import { Card } from './card.interface';
export interface Draw{
  id: string,
  success: boolean,
  cards: Card[],
  deck_id: string,
  remaining:number
}