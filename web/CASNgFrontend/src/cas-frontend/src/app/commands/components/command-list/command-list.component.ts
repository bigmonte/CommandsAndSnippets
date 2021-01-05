import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Command } from '../../models/Command'

@Component({
  selector: 'app-command-list',
  templateUrl: './command-list.component.html',
  styleUrls: ['./command-list.component.scss']
})
export class CommandListComponent {
  @Input() commands : Command[];
  @Output() onCommandClicked = new EventEmitter<Command>()
  
  handleCommandClick (command: Command) {
    this.onCommandClicked.next(command)
  }
}
