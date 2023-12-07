import { Component, Output, EventEmitter } from '@angular/core';
import { Game } from '../models';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css']
})
export class StartComponent {

  @Output() 
  gameIdEvent = new EventEmitter<string>();

  constructor(private http: HttpClient) {}

  CreateGame(){
    this.http.get<Game | null>(`/api/game/create`).subscribe(resp => {
      if(resp == null){
        alert("Error. Game was not created.")
        return;
      }

      localStorage.removeItem("game");
      localStorage.setItem("game", resp.id.toString());
      this.gameIdEvent.emit(resp.id.toString());
    });
  }

}
