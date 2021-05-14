import { NavbarService } from './../_services/navbar.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private router: Router, private nav: NavbarService) { }

  ngOnInit() {
    this.nav.hide();
  }

  logIn() {
    this.authService.login(this.model).subscribe(next => {
        console.log('Logged in successfully!');
        this.router.navigate(['/home']);
      },
      error => {
        console.log(error);
      });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
