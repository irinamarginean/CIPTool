import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  isVisible: boolean = true;

  constructor(public authService: AuthService) { }

  hide() { this.isVisible = false; }
  show() { this.isVisible = true; }
}
