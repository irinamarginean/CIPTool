import { NavbarService } from './navbar.service';
import { LoginComponent } from './../login/login.component';
import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NavigateAwayFromLoginDeactivatorService implements CanDeactivate<LoginComponent> {

  constructor(public navbar: NavbarService) { }

  canDeactivate(target: LoginComponent) {
    this.navbar.show();
    return true;
  }
}
