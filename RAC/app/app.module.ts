import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { AppComponent } from './app.component';
import { UsersServices } from './app.services'

@NgModule({
    imports: [BrowserModule,
        FormsModule,
        HttpModule,
        JsonpModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent],
    providers: [UsersServices]
})
export class AppModule { }