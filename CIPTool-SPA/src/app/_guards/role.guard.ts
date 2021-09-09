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

    if (this.authService.loggedIn() &&
        expectedRoles.some((x: string) => this.authService.getDecodedToken()['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'].includes(x))) {
      return true;
    }

    this.router.navigate(['/home']);
    }
}
