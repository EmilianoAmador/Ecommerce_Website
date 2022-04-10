import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';


  constructor (private basketService: BasketService, private accountService: AccountService) {}   // injecting the http service

  ngOnInit(): void {    
    this.loadBasket();  
    this.loadCurrentUser();                                                                                 // life cycle hook
    
}

/**
 * loads Current user observable only if user is logged in.
 */
loadCurrentUser() {
  const token = localStorage.getItem('token');
  if (token) {
    this.accountService.loadCurrentUser(token).subscribe(() => {
      console.log('loaded user');
    }, error => {
      console.log(error);
    });
  }
}

/**
 * Method that loads basket. Created to clean up the initialization ngOnInit.
 */
loadBasket() {
  const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log('initialised basket');
      }, error => {
        console.log(error);
      });
    }
  }
}