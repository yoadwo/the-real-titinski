import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { StickyHeaderComponent } from './components/sticky-header/sticky-header.component';
import { RantDetailComponent } from './components/rant-detail/rant-detail/rant-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    StickyHeaderComponent,
    RantDetailComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
