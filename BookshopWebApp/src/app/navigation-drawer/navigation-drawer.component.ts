import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { DrawerItem, DrawerSelectEvent } from "@progress/kendo-angular-layout";
import { Router } from "@angular/router";
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-navigation-drawer',
  templateUrl: './navigation-drawer.component.html',
  styleUrls: ['./navigation-drawer.component.css']
})
export class NavigationDrawerComponent implements OnInit {

  public selected = "Home";
  public items: Array<any> = [];

  constructor(private router: Router, private authService: AuthService) {
    this.items = this.mapItems(router.config);
    this.items[0].selected = true;
  }

  ngOnInit(): void {
  }

  public onSelect(ev: DrawerSelectEvent): void {
    this.router.navigate([ev.item.path]);
  }

  public mapItems(routes: any[], path?: string): any[] {
    return routes.map((item) => {
      if (!this.authService.isAdmin() && item.path == "inventory") {
        return undefined;
      }
      return {
        text: item.text,
        icon: item.icon,
        path: item.path ? item.path : "",
        canActivate: item.canActivate ? item.canActivate : []
      };
    });
  }
}
