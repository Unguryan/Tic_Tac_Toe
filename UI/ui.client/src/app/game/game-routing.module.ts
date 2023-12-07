import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BaseGameComponent } from './base-game/base-game.component';

const routes: Routes = [
  { path:"",  component: BaseGameComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GameRoutingModule { }
