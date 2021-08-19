import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';


  constructor () {}   // injecting the http service

  ngOnInit(): void {                                                                                       // life cycle hook

  }
}
