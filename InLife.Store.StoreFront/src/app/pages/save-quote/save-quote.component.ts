import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-save-quote',
  templateUrl: './save-quote.component.html',
  styleUrls: ['./save-quote.component.scss']
})
export class SaveQuoteComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  copyCode(){
    console.log('hello');
    
  }
}
