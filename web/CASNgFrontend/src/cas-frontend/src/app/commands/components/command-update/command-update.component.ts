import { Component, EventEmitter, Input, Output, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Alert, Command } from '../../models/Command';

@Component({
  selector: 'app-command-form',
  templateUrl: './command-update.component.html',
  styleUrls: ['./command-update.component.scss']
})
export class CommandUpdateComponent {

  // TODO make form model
  
  @Input() alert: Alert
  @Output() onCommandUpdated = new EventEmitter<Command>()
  @Input() onSubmit: (cmd: Command) => Observable<Command>
  @Input() disableSubmit: boolean
  @Input() onFormInputChanged : Function
  @Input() set command (selectedCommand: Command) {
    this.selectedCommand = { ...selectedCommand}
  }

  selectedCommand: Command

  submitForm () {
    this.onSubmit(this.selectedCommand)
  }

}
