import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Game } from '../models';
import { ActiveComponent } from '../active/active.component';

@Component({
  selector: 'app-base-game',
  templateUrl: './base-game.component.html',
  styleUrls: ['./base-game.component.css'],
})
export class BaseGameComponent implements OnInit {
  
  public id: string | null;

  public game: Game | null;

  @ViewChild("active")
  activeGameComp : ActiveComponent;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.id = localStorage.getItem("game");
    if(this.id != null){
      this.http.get<Game | null>(`/api/game/get?id=${this.id}`).subscribe(resp => {
        this.game = resp;
        console.dir(this.game);
        this.activeGameComp.SetGame(this.game);
      });
    }
  }

  GameCreatedHandler(event: string){
    this.id = event;
    this.http.get<Game | null>(`/api/game/get?id=${this.id}`).subscribe(resp => {
      this.game = resp;
      this.activeGameComp.SetGame(this.game);
    });
  }
}
