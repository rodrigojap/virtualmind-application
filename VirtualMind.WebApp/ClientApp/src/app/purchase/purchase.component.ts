import { Component, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html'
})
export class PurchaseComponent {

  private messages: Array<string> = [];
  private loading: boolean = false;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
  }

  onSubmit(f: NgForm) {

    this.messages = [];

    if (f.valid) {

      this.loading = true;

      const requestObject = {
        userId: parseInt(f.value.userId),
        requestedAmount: parseFloat(f.value.amount),
        currencyType: f.value.currency
      };

      this.http.post(this.baseUrl + 'exchange', requestObject)
        .subscribe(result => {
          this.messages.push('Wow, Purchase successful! :)');
          f.reset();
        }, error => {
          error.error.errors.InvalidOperation.forEach(element => {
            this.messages.push(element);
          });          
        });

        setTimeout(() => {
          this.loading = false;
        }, 1000);
    }
    else {
      if (!f.value.userId) {
        this.messages.push("User Id is required!");
      }
      if (!f.value.amount) {
        this.messages.push("Pesos is required!");
      }
      if (!f.value.currency) {
        this.messages.push("Currency Id is required!");
      }
    }
  }
}
