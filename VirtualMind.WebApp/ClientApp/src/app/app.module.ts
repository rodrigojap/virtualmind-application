import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgSpinnerModule } from 'ng-bootstrap-spinner';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PurchaseComponent } from './purchase/purchase.component';

import { StoreModule } from '@ngrx/store';
import { QuoteReducer } from './store/quote/quoteReducer';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,    
    FetchDataComponent,
    PurchaseComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    NgSpinnerModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: FetchDataComponent },      
      { path: 'quote', component: FetchDataComponent }, 
      { path: 'purchase', component: PurchaseComponent },      
    ]),
    StoreModule.forRoot({
      quotes: QuoteReducer
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
