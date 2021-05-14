import { AuthService } from './../_services/auth.service';
import { NavbarService } from './../_services/navbar.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css', './nav.component.scss']
})
export class NavComponent implements OnInit {

  constructor(public nav: NavbarService, public authService: AuthService) { }

  displayName: string;

  ngOnInit() {
    this.displayName = this.authService.getDisplayName();
  }
}
