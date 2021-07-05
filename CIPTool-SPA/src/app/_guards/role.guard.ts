import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(
      private authService: AuthService,
      private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRoles = route.data.expectedRoles;

    console.log(route.data);
    console.log(this.authService.getDecodedToken()['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);

    if (this.authService.loggedIn() &&
        expectedRoles.includes(this.authService.getDecodedToken()['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'])) {
      return true;
    }

    this.router.navigate(['/home']);
    }
}
