import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Cell } from '../models';

@Component({
  selector: 'app-cells',
  templateUrl: './cells.component.html',
  styleUrls: ['./cells.component.css']
})
export class CellsComponent {

  @Input("cells")
  cells: Cell[][] | undefined;

  @Output() 
  cellSelected = new EventEmitter<Cell>();

  SelectCell(event: Cell){
    this.cellSelected.emit(event);
  }
}
