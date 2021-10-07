import { Component, Input, OnInit } from '@angular/core';
import { Rant } from './models/rant';
import { RantHttpService } from './services/rantHttpService/rant-http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {  
  @Input() name: string = '';
  
  title = 'Titinski-Client';
  rants: Rant[];
  
  constructor(private rantHttpService : RantHttpService) {
    this.rants = [];
  }

  ngOnInit(): void {
    this.rantHttpService.list().subscribe((data: Rant[]) => {
      this.rants = data;
    });
  }

  
}
