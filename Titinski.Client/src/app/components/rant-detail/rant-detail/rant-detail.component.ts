import { Component, Input, OnInit } from '@angular/core';
import { Rant } from 'src/app/models/rant';

@Component({
  selector: 'app-rant-detail',
  templateUrl: './rant-detail.component.html',
  styleUrls: ['./rant-detail.component.css']
})
export class RantDetailComponent implements OnInit {
  @Input() rantDetail?: Rant;
  constructor() { }

  ngOnInit(): void {
  }

}
