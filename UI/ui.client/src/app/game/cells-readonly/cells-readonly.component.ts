import { Component, Input } from '@angular/core';
import { Cell } from '../models';

@Component({
  selector: 'app-cells-readonly',
  templateUrl: './cells-readonly.component.html',
  styleUrls: ['./cells-readonly.component.css']
})
export class CellsReadonlyComponent {

  @Input("cells")
  cells: Cell[][] | undefined;
}
