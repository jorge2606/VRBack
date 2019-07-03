import { Title } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-not-authorized',
  templateUrl: './not-authorized.component.html',
  styleUrls: ['./not-authorized.component.css']
})
export class NotAuthorizedComponent implements OnInit {

  constructor(
    private title : Title
  ) { }

  ngOnInit() {
    this.title.setTitle('Acceso Restringido');
  }

}
