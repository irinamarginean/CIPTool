import { Component } from '@angular/core';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import {
  Router,
  Event as RouterEvent,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError
} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CIP Tool';
  public showOverlay = true;

  constructor(private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer, private router: Router) {
    router.events.subscribe((event: RouterEvent) => {
      this.navigationInterceptor(event)
    });

    this.matIconRegistry
      .addSvgIcon(
        'lightbulb',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/lightbulb.svg')
      ).addSvgIcon(
        'lightbulb-dark',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/lightbulb-dark.svg')
      ).addSvgIcon(
        'lightbulb-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/lightbulb-blue.svg')
      ).addSvgIcon(
        'globe',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/globe.svg')
      ).addSvgIcon(
        'globe-dark',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/globe-dark.svg')
      ).addSvgIcon(
        'globe-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/globe-blue.svg')
      ).addSvgIcon(
        'add',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/add.svg')
      ).addSvgIcon(
        'add-dark',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/add-dark.svg')
      ).addSvgIcon(
        'add-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/add-blue.svg')
      ).addSvgIcon(
        'home',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/home.svg')
      ).addSvgIcon(
        'home-light',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/home-light.svg')
      ).addSvgIcon(
        'chart',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/chart-OEE.svg')
      ).addSvgIcon(
        'user',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/user.svg')
      ).addSvgIcon(
        'close',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/close.svg')
      ).addSvgIcon(
        'edit',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/edit.svg')
      ).addSvgIcon(
        'edit-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/edit-blue.svg')
      ).addSvgIcon(
        'upload',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/upload.svg')
      ).addSvgIcon(
        'download',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/download.svg')
      ).addSvgIcon(
        'link',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/link.svg')
      ).addSvgIcon(
        'down-small',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/down-small.svg')
      ).addSvgIcon(
        'checkmark-frame-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/checkmark-frame-blue.svg')
      ).addSvgIcon(
        'arrow-right',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/arrow-right.svg')
      ).addSvgIcon(
        'arrow-right-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/arrow-right-blue.svg')
      ).addSvgIcon(
        'watch-on',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/watch-on.svg')
      ).addSvgIcon(
        'watch-on-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/watch-on-blue.svg')
      ).addSvgIcon(
        'watch-off-disabled-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/watch-off-disabled-blue.svg')
      ).addSvgIcon(
        'hourglass',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/hourglass.svg')
      ).addSvgIcon(
        'abort-frame',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/abort-frame.svg')
      ).addSvgIcon(
        'alert-success',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/alert-success.svg')
      ).addSvgIcon(
        'clock-24-7',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/clock-24-7.svg')
      ).addSvgIcon(
        'arrow-down-frame',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/arrow-down-frame.svg')
      ).addSvgIcon(
        'box-arrow-down',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/box-arrow-down.svg')
      ).addSvgIcon(
        'delivery-checkmark',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/delivery-checkmark.svg')
      ).addSvgIcon(
        'flag',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/flag.svg')
      ).addSvgIcon(
        'coin',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/coin.svg')
      ).addSvgIcon(
        'money-euro',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/money-euro.svg')
      ).addSvgIcon(
        'bank',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/bank.svg')
      ).addSvgIcon(
        'box-questionmark',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/box-questionmark.svg')
      ).addSvgIcon(
        'box-delivery',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/box-delivery.svg')
      ).addSvgIcon(
        'box-falling-off-disabled-light',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/box-falling-off-disabled-light.svg')
      ).addSvgIcon(
        'alert-warning',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/alert-warning.svg')
      ).addSvgIcon(
        'alert-error',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/alert-error.svg')
      ).addSvgIcon(
        'address-consumer-data-upright',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/address-consumer-data-upright.svg')
      ).addSvgIcon(
        'address-book',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/address-book.svg')
      ).addSvgIcon(
        'summary',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/summary.svg')
      ).addSvgIcon(
        'plan-a-to-b',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/plan-a-to-b.svg')
      ).addSvgIcon(
        'cash-frame',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/cash-frame.svg')
      ).addSvgIcon(
        'hourglass-white',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/hourglass-white.svg')
      ).addSvgIcon(
        'alert-success-white',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/alert-success-white.svg')
      ).addSvgIcon(
        'clock-24-7-white',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/clock-24-7-white.svg')
      ).addSvgIcon(
        'abort-frame-white',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/abort-frame-white.svg')
      ).addSvgIcon(
        'flag-white',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/flag-white.svg')
      ).addSvgIcon(
        'document-arrow-down',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/document-arrow-down.svg')
      ).addSvgIcon(
        'square-add',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/square-add.svg')
      ).addSvgIcon(
        'registration',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/registration.svg')
      ).addSvgIcon(
        'close-blue',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/close-blue.svg')
      ).addSvgIcon(
        'save',
        this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/save.svg')
      )
  }

  navigationInterceptor(event: RouterEvent): void {
    if (event instanceof NavigationStart) {
      this.showOverlay = true;
    }
    if (event instanceof NavigationEnd) {
      this.showOverlay = false;
    }
    if (event instanceof NavigationCancel) {
      this.showOverlay = false;
    }
    if (event instanceof NavigationError) {
      this.showOverlay = false;
    }
  }

}
