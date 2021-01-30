import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
//import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
//import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

// services
import { HttpService } from './services/http.service';

// employee-list
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeListService } from './employee-list/employee-list.service';

import { BenefitFormComponent } from './employee-list/benefit-form/benefit-form.component';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    EmployeeListComponent,
    BenefitFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] }, <-- enable and setup route guard after authentication complete
    ])
  ],
  providers: [
    //{ provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }, // <-- remove the interceptor until security in place
    HttpService, EmployeeListService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
