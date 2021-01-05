import { Component, OnInit, OnDestroy } from '@angular/core'
import { Alert, Command } from './models/Command'
import { ApiService } from '../api.service'
import { AlertComponent } from '../shared/header/alert.component'

@Component({
  selector: 'app-commands',
  templateUrl: 'commands.component.html',
})
export class CommandsComponent extends AlertComponent implements OnInit, OnDestroy {

  public selectedCommand: Command
  public isDetailView = true

  public commands: Command[] = []

  constructor (private apiService: ApiService) { 
    super()
  }

  public onUpdateFormChangedCb: Function;


  disableUpdateFormSubmit: boolean = true
  onUpdateFormInputChanged (commandForm: Command) {
    // Do safety checks to the update input
    // TODO move to its own update form (ps. update form is being reutilized for the new form)
    if(commandForm.commandLine === this.selectedCommand.commandLine && 
      commandForm.howTo === this.selectedCommand.howTo &&
      commandForm.platform === this.selectedCommand.platform) {
        this.disableUpdateFormSubmit = true
        return
      }
    this.disableUpdateFormSubmit = false
  }

  public ngOnInit(): void {
    this.fetchCommands()
    this.onUpdateFormChangedCb = this.onUpdateFormInputChanged.bind(this)
  }
  searchResult: Command[]

  onSearchText(event: Event) {
    var text = (<HTMLInputElement>event.target).value
    if(text) {
      this.apiService
      .searchCommand(text)
      .subscribe(results => {
        this.searchResult = results
        this.commands = results
      })
      return
    }
    this.fetchCommands()
  }
 

  ngOnDestroy(): void {
    this.clearAlertTimeout()
  }

  public commandsEmpty (): boolean {
    return this.commands.length === 0
  }

  public updateCommand = (command: Command) => {
    this.apiService
      .updateCommand(command)
      .subscribe(cmd => {
        this.refreshCommands(cmd)
        this.setAlert('success', 'Command was updated')
      }, (error: string) => {
        this.setAlert('error', error)
      })
    this.selectedCommand = command
    this.onUpdateFormInputChanged(command)
  }

  private fetchCommands () {
    this.apiService
      .fetchCommands()
      .subscribe((commands: Command[]) => {
        this.commands = commands
        this.selectedCommand = this.commands[0]
      })
  }

  getArrIndex (command: Command) : number {
    return this.commands.findIndex(c => command.id === c.id)
  }
  
  public refreshCommands (command: Command) {
    const index = this.getArrIndex(command)
    this.commands[index] = command
    this._selectCommand(command)
  }

  public addCommand (command: Command) {
    this.apiService
      .addCommand(command)
      .subscribe(c => {
        this.commands.unshift(c)
      })
  }

  public deleteCommand () {
    const isConfirmed = confirm('Are your sure you want to delete the command?')
    if(isConfirmed) {
      this.apiService
        .deleteCommand(this.selectedCommand.id)
        .subscribe(c => {
          const i = this.getArrIndex(c)
          this.commands.splice(i, 1)
          this.selectedCommand = this.commands[0]
        })
    }
  }

  public toggleView () {
    this.isDetailView = !this.isDetailView
  }

  private _selectCommand (command: Command) {
    if(!command) { alert ('command is corrupted')}
    this.selectedCommand = {...command}
  }

  get btnViewClass (): String {
    return this.isDetailView ? 'btn-primary' : 'btn-secondary'
  }

  public handleCommandSelect (command: Command) {
    this._selectCommand(command)
  }
}

