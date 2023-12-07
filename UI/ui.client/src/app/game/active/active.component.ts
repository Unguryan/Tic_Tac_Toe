import { Component, Input, ViewChild } from '@angular/core';
import { Board, BoardCoords, Cell, Game } from '../models';
import { BoardsComponent } from '../boards/boards.component';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-active',
  templateUrl: './active.component.html',
  styleUrls: ['./active.component.css']
})
export class ActiveComponent {

  @Input("activeGame")
  game: Game | null;
  
  constructor(private http: HttpClient, private router: Router) {}

  selectedBoard: Board | undefined;

  SetGame(gameToSet: Game | null){
    if(gameToSet == null){
      alert("Not Found");
      return;
    }

    this.game = gameToSet;
    if(this.game.boardToPlay != null){
      var coords = this.game.boardToPlay;
      console.dir(coords);
      this.selectedBoard = this.game.boards[coords.x][coords.y];
      console.dir(this.selectedBoard);
    }
  }

  StartNew(){
    //remove old game
    localStorage.removeItem("game");
    this.router.navigate(['/']);
    window.location.reload();
  }

  BoardCellSelectedHandler(event: Cell){
    console.dir(event);
    this.Move(event?.x, event?.y);
  }

  BoardSelectedHanlder(event: Board){
    this.selectedBoard = event;
    this.Move(event.boardCoords?.x, event.boardCoords?.y);
  }

  private Move(x: number|undefined, y: number|undefined){
    var id = localStorage.getItem("game");
    if(id != null){
      this.http.get<Game | null>(`/api/game/move?id=${id}&x=${x}&y=${y}`).subscribe(resp => {
        this.game = resp;
        if(this.game?.boardToPlay != null){
          var coords = this.game.boardToPlay;
          this.selectedBoard = this.game.boards[coords.x][coords.y];
        }
        else{
          this.selectedBoard = undefined;
        }
      });
    }
  }
}
