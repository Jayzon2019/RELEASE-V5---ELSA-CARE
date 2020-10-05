import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-prime-care',
  templateUrl: './prime-care.component.html',
  styleUrls: ['./prime-care.component.scss']
})
export class PrimeCareComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

}
