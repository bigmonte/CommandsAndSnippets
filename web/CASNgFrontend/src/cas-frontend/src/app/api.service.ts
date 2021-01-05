import { Injectable } from '@angular/core'
import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { Observable, throwError } from 'rxjs'
import { Command } from './commands/models/Command'
import { catchError } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  constructor (private http: HttpClient) { }

  private handleError (errorRes: HttpErrorResponse): Observable<never> {
    let message;

    if(errorRes) {
      message = errorRes.error.title // NET Core MVC error title
    }
    return throwError(message)
  }

  fetchCommands (): Observable<Command[]> {
    return this.http
      .get<Command[]>('/api/commands')
      .pipe(catchError(this.handleError))
  }

  updateCommand (command: Command): Observable<Command>  {
    return this.http
      .put<Command>(`/api/commands/${command.id}`, command)
      .pipe( catchError(this.handleError))
  }
  
  addCommand (command: Command): Observable<Command>  {
    return this.http
      .post<Command>('/api/commands/', command)
      .pipe( catchError(this.handleError))
  }
  
  getCommand (id: number): Observable<Command> {
     return this.http
      .get<Command>(`/api/commands/${id}`)
      .pipe( catchError(this.handleError))
  }

  deleteCommand (id: number): Observable<Command>  {
    return this.http
      .delete<Command>(`/api/commands/${id}`)
      .pipe(catchError(this.handleError))
  }

  searchCommand (text: string): Observable<Command[]> {
    return this.http  
      .get<Command[]>(`api/search/commands/${text}`)
      .pipe(catchError(this.handleError))
  }

}