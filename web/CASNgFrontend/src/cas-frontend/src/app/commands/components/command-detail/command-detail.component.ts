import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { ApiService } from 'src/app/api.service';
import { Command } from '../../models/Command';

@Component({
  selector: 'app-command-detail',
  templateUrl: './command-detail.component.html',
  styleUrls: ['./command-detail.component.scss']
})
export class CommandDetailComponent implements OnInit {

  constructor (private route: ActivatedRoute, private apiService: ApiService) { }

  command: Command

  ngOnInit (): void {
    this.route.params.subscribe(params => {
      this.apiService
        .getCommand(params['id'])
        .subscribe(cmd => this.command = cmd)
    })
  }

}
