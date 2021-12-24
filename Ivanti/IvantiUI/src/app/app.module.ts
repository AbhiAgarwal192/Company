import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TriangleComponent } from './triangle/triangle.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TriangleapiService } from './triangleapi.service';

@NgModule({
  declarations: [
    AppComponent,
    TriangleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    HttpClient,
    TriangleapiService
  ],
  bootstrap: [TriangleComponent]
})
export class AppModule { }
