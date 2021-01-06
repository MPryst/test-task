import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParams,
  HttpEventType
} from '@angular/common/http';
import { map, catchError, tap } from 'rxjs/operators';
import { Subject, throwError } from 'rxjs';

import { Transaction } from './transaction.model';

@Injectable({ providedIn: 'root' })
export class TransactionsService {
  url:string = 'https://localhost:44355';
  error = new Subject<string>();

  constructor(private http: HttpClient) { }  

  fetchBalance() {    
    return this.http
      .get<{ [key: string]: number }>(
        this.url + '/api/default', { responseType: 'json' }
      )
      .pipe(
        map(responseData => {          
          return responseData;          
        }),
        catchError(errorRes => {
          // Send to analytics server
          return throwError(errorRes);
        })
      );

  }

  fetchPosts() {    
    return this.http
      .get<{ [key: string]: Transaction }>(
        this.url + '/api/transactions',
        { 
          responseType: 'json'
        }
      )
      .pipe(
        map(responseData => {
          const postsArray: Transaction[] = [];
          for (const key in responseData) {
            if (responseData.hasOwnProperty(key)) {
              postsArray.push({ ...responseData[key], id: key });
            }
          }
          return postsArray;
        }),
        catchError(errorRes => {
          // Send to analytics server
          return throwError(errorRes);
        })
      );
  }  
}
