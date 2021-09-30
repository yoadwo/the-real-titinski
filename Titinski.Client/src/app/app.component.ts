import { Component, Input, OnInit } from '@angular/core';
import { Rant } from './models/rant';
import { HttpService } from './services/httpService/http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {  
  @Input() name: string = '';
  
  title = 'Titinski-Client';
  //pages = new Array(10);
  rants: Rant[];
  
  constructor(protected httpService : HttpService) {
    this.rants = [];
  }

  ngOnInit(): void {
    this.httpService.list().subscribe((res:Rant[]) => {
      this.rants = res;
    });
  }

  
}
