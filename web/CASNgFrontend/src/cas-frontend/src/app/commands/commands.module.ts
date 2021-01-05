import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { HttpClientModule } from '@angular/common/http'
import { CommonModule } from '@angular/common'
import { FormsModule } from '@angular/forms'
import { CommandsComponent } from './commands.component';
import { CommandSearchComponent } from './components/command-search/command-search.component';
import { CommandListComponent } from './components/command-list/command-list.component';
import { CommandDetailCardComponent } from './components/command-detail-card/command-detail-card.component';
import { CommandDetailComponent } from './components/command-detail/command-detail.component';
import { CommandUpdateComponent } from './components/command-update/command-update.component';
import { CommandNewComponent } from './components/command-new/command-new.component';
import { CommandOutletComponent } from './components/command-outlet.component';

const routes: Routes = [
  {path: 'commands', component: CommandOutletComponent, 
    children: [
      { path: '', component: CommandsComponent},
      { path: 'new', component: CommandNewComponent},
      { path: ':id', component: CommandDetailComponent}
    ] }
]
@NgModule({
  declarations: [
    CommandsComponent, 
    CommandSearchComponent, 
    CommandListComponent, 
    CommandDetailCardComponent, 
    CommandUpdateComponent, 
    CommandNewComponent, 
    CommandOutletComponent,
    CommandDetailComponent
  ],
  exports : [CommandsComponent],
  imports : [CommonModule, HttpClientModule, FormsModule, RouterModule.forChild(routes)]
})

export class CommandsModule {}