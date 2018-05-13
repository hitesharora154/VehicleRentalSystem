import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ListVehicleComponent } from './list-vehicle/list-vehicle.component';
import { VehicleService } from './services/vehicle.service';
import { MaterialModule } from './material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { RegisterService } from './services/register.service';
import { HomeScreenComponent } from './home-screen/home-screen.component';
import { CarouselComponent } from './carousel/carousel.component';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { LoginDialogService } from './login-dialog/login-dialog.service';
import { RegisterDialogComponent } from './register-dialog/register-dialog.component';
import { RegisterDialogService } from './register-dialog/register-dialog.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginService } from './services/login.service';
import { AuthGuard } from './services/auth-guard.service';
import { AddBookingComponent } from './add-booking/add-booking.component';
import { BookingService } from './services/booking.service';
import { ViewBookingsComponent } from './view-bookings/view-bookings.component';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { DeleteDialogService } from './delete-dialog/delete-dialog.service';

@NgModule({
  declarations: [
    AppComponent,
    ListVehicleComponent,
    RegisterComponent,
    HomeScreenComponent,
    CarouselComponent,
    LoginDialogComponent,
    RegisterDialogComponent,
    DashboardComponent,
    AddBookingComponent,
    ViewBookingsComponent,
    DeleteDialogComponent
  ],
  entryComponents: [
    LoginDialogComponent,
    RegisterDialogComponent,
    DeleteDialogComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [AuthGuard]
      },
      {
        path: '',
        component: HomeScreenComponent
      }
    ])
  ],
  providers: [
    VehicleService,
    RegisterService,
    LoginDialogService,
    RegisterDialogService,
    LoginService,
    AuthGuard,
    BookingService,
    DeleteDialogService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
