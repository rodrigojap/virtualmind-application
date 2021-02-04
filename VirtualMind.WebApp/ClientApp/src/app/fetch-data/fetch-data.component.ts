import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { quote } from '../models/quote.model';
import QuoteState from '../store/quote/quoteState';
import { getDolar, getReal } from '../store/quote/quoteAction';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {

  quote$: Observable<QuoteState>;
  dolar: quote;
  real: quote;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private store: Store<{ quotes: QuoteState }>) {
    this.quote$ = this.store.pipe(select((state: any) => state.quotes));    
    this.getAllQuote();
  }

  ngOnInit() {
    this.quote$.pipe(
      map(x => {
        this.dolar = x.dolar;
        this.real = x.real;
      })
    )
      .subscribe();
  }

  getAllQuote() {
    console.log('fetching cote');
    this.getDollarQuote();
    this.getRealQuote();
  }

  getDollarQuote() {
    this.http.get<quote>(this.baseUrl + 'exchange?currencyType=USD').subscribe(result => {      
      this.store.dispatch(getDolar({payload: result}));
    }, error => console.error(error));
  }

  getRealQuote() {
    this.http.get<quote>(this.baseUrl + 'exchange?currencyType=BRL').subscribe(result => {
      this.store.dispatch(getReal({payload: result}));
    }, error => console.error(error));
  }
}
