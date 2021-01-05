import { Component, Input } from '@angular/core';
import { Command } from '../../models/Command';

@Component({
  selector: 'app-command-detail-card',
  templateUrl: './command-detail-card.component.html',
  styleUrls: ['./command-detail-card.component.scss']
})
export class CommandDetailCardComponent  {
  @Input() command: Command

}
