import { NgModule } from '@angular/core';
import { AdminModule } from './admin/admin.module';
import {HttpClientModule} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    AdminModule,
    HttpClientModule,
    BrowserModule,
    ToastrModule.forRoot({
      closeButton : true,
      progressBar: true
    })



  ],
  providers: [
    {provide:'apiUrl', useValue:'https://localhost:7146/api/'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
