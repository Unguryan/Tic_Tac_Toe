import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { Board, BoardCoords } from '../models';

@Component({
  selector: 'app-boards',
  templateUrl: './boards.component.html',
  styleUrls: ['./boards.component.css']
})
export class BoardsComponent implements OnChanges{

  ngOnChanges(){
    if(this.boards != null){
      for (let i = 0; i < this.boards.length; i++) {
        for (let j = 0; j < this.boards[i].length; j++) {
          this.boards[i][j].selected = false;
        }
      }

      if(this.coords != null){
        this.boards[this.coords.x][this.coords.y].selected = true;
      }
    }
  }

  @Input("boards")
  boards: Board[][] | undefined;

  @Input("coords")
  coords: BoardCoords | undefined | null;

  @Output() 
  boardSelected = new EventEmitter<Board>();

  SelectBoard(board: Board){
    if(board.winner != null){
      return;
    }
    if(this.coords == null){
      this.boardSelected.emit(board);
    }
  }
}
