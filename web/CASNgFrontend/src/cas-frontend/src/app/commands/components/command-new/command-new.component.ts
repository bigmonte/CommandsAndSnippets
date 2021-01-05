import { Component, OnInit } from '@angular/core';
import { Command } from '../../models/Command';
import { ApiService } from 'src/app/api.service';
import { AlertComponent } from 'src/app/shared/header/alert.component';

@Component({
  selector: 'app-command-new',
  templateUrl: './command-new.component.html',
  styleUrls: ['./command-new.component.scss']
})
export class CommandNewComponent extends AlertComponent implements OnInit {

  command: Command = new Command()

  disableFormSubmission: boolean = true

  constructor(private apiService: ApiService) { 
    super()
  }

  ngOnInit(): void {
    this.onNewFormChangedCb = this.onFormChanged.bind(this)
  }

  public onNewFormChangedCb: Function;

  onFormChanged (command: Command) {
    this.doFormChecks(command)
  }

  doFormChecks (command: Command): void {
    if(!command.howTo || !command.platform || !command.commandLine){
      this.disableFormSubmission = true
      return
    } else {
      this.disableFormSubmission = false
    }
  }

  createCommand = (command: Command) => {
    this.apiService
      .addCommand(command)
      .subscribe(cmd => {
        this.setAlert('success', 'Command created!')
      }, (error: string) => {
        this.setAlert('error', error)
      })
  }

}
