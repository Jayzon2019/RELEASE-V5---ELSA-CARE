import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'group-application-status',
  templateUrl: './application-status.component.html',
  styleUrls: ['./application-status.component.scss']
})
export class ApplicationStatusComponent implements OnInit {
  constructor() { 
  }

  ngOnInit(): void {
  }

  onActivate(event) {
    let scrollToTop = window.setInterval(() => {
        let pos = window.pageYOffset;
        if (pos > 0) {
            window.scrollTo(0, pos - 20); // how far to scroll on each step
        } else {
            window.clearInterval(scrollToTop);
        }
    }, 16);
}
}
