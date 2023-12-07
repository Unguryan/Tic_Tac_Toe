import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GameRoutingModule } from './game-routing.module';
import { StartComponent } from './start/start.component';
import { ActiveComponent } from './active/active.component';
import { BaseGameComponent } from './base-game/base-game.component';
import { BoardComponent } from './board/board.component';
import { BoardsComponent } from './boards/boards.component';
import { CellsComponent } from './cells/cells.component';
import { CellsReadonlyComponent } from './cells-readonly/cells-readonly.component';


@NgModule({
  declarations: [
    StartComponent,
    ActiveComponent,
    BaseGameComponent,
    BoardComponent,
    BoardsComponent,
    CellsComponent,
    CellsReadonlyComponent
  ],
  imports: [
    CommonModule,
    GameRoutingModule
  ]
})
export class GameModule { }
