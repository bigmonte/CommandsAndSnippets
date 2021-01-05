import { Alert } from "src/app/commands/models/Command"

export class AlertComponent {
  public alert: Alert = new Alert()
  public timeOutPointer: number


  public setAlert (status: string, message:string) {
    this.alert = new Alert()
    this.alert[status] = message

    this.timeOutPointer = window.setTimeout(() =>  this.alert = new Alert (), 2000)

  
  }
  public clearAlertTimeout () {
    if(this.timeOutPointer) {
      clearTimeout(this.timeOutPointer)
      this.alert = new Alert()
    }
  }
}
