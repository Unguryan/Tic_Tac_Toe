import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Board, Cell } from '../models';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent {
  
  @Input("board")
  board: Board | undefined;

  @Output() 
  boardCellSelected = new EventEmitter<Cell>();

  SelectedHandler(event: Cell)
  {
    this.boardCellSelected.emit(event);
  }
}
