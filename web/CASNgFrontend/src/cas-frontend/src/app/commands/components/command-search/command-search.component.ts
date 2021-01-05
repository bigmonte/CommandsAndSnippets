import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-command-search',
  templateUrl: './command-search.component.html',
  styleUrls: ['./command-search.component.scss']
})
export class CommandSearchComponent {

  @Output() onSearch = new EventEmitter<Event>()

}
