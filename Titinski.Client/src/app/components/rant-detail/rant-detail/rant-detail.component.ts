import { Component, Input, OnInit } from '@angular/core';
import { Rant } from 'src/app/models/rant';
import { RantHttpService } from 'src/app/services/rantHttpService/rant-http.service';

@Component({
  selector: 'app-rant-detail',
  templateUrl: './rant-detail.component.html',
  styleUrls: ['./rant-detail.component.css']
})
export class RantDetailComponent implements OnInit {
  @Input() rantDetail!: Rant;
  imageToShow: any;
  isImageLoading: boolean;
  
  constructor(private rantHttpService : RantHttpService) { 
    this.isImageLoading = true;
  }

  ngOnInit(): void {
    if (this.rantDetail !== null){
      this.isImageLoading = true;      
      this.rantHttpService.getPhoto(encodeURIComponent(this.rantDetail.path)).subscribe(data => {
        this.createImageFromBlob(data);
        this.isImageLoading = false;
      }, error => {
        this.isImageLoading = false;
        console.log(error);
      });
    }
  }

  createImageFromBlob(image: Blob) {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
       this.imageToShow = reader.result;
    }, false);
 
    if (image) {
       reader.readAsDataURL(image);
    }
 }

}
