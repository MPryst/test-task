import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { Transaction } from './transaction.model';
import { TransactionsService } from './transactions.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  loadedTransactions: Transaction[] = [];
  balance: number = 0.0;
  isFetching = false;
  error = null;
  private errorSub: Subscription;

  constructor(private http: HttpClient, private postsService: TransactionsService) { }

  ngOnInit() {
    this.errorSub = this.postsService.error.subscribe(errorMessage => {
      this.error = errorMessage;
    });

    this.isFetching = true;
    this.onFetchPosts();
  }   

  loadBalance(){    
    this.postsService.fetchBalance().subscribe(
      result => {        
        this.balance = result.balance;
      }
    )
  }  

  onFetchPosts() {
    // Send Http request
    this.loadBalance();
    this.isFetching = true;
    this.postsService.fetchPosts().subscribe(
      posts => {
        this.isFetching = false;
        this.loadedTransactions = posts;
      },
      error => {
        this.isFetching = false;
        this.error = error.message;
        console.log(error);
      }
    );
  }
  

  onHandleError() {
    this.error = null;
  }

  ngOnDestroy() {
    this.errorSub.unsubscribe();
  }

  onToggleInfo(id: string) {        

    const element = this.loadedTransactions.find( x => x.uuid === id);
    element.showAll = !element.showAll    
    debugger;
  }
}
