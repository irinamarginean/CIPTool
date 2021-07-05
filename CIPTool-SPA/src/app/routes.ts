import { HasUnsavedDataGuard } from './_guards/hasUnsavedDataGuard';
import { EditComponent } from './ideas/edit/edit.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { NavigateAwayFromLoginDeactivatorService } from './_services/navigateAwayFromLoginDeactivator.service';
import { LeaderResponseOverviewComponent } from './leader-response/leader-response-overview/leader-response-overview.component';
import { AllIdeasComponent } from './ideas/all-ideas/all-ideas.component';
import { MyIdeasComponent } from './ideas/my-ideas/my-ideas.component';
import { AddIdeaComponent } from './ideas/add-idea/add-idea.component';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { IdeaDetailsComponent } from './ideas/idea-details/idea-details.component';
import { LeaderResponseDetailsComponent } from './leader-response/leader-response-details/leader-response-details.component';
import { AuthGuard } from './_guards/auth.guard';
import { RoleGuard } from './_guards/role.guard';

export const appRoutes: Routes = [
  { path: 'login',
    component: LoginComponent,
    canDeactivate: [NavigateAwayFromLoginDeactivatorService] ,
  },
  { path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard]
  },
  { path: 'ideas/add-idea',
    component: AddIdeaComponent,
    canDeactivate: [HasUnsavedDataGuard],
    canActivate: [AuthGuard]
  },
  { path: 'ideas/my-ideas',
    component: MyIdeasComponent,
    canActivate: [AuthGuard]
  },
  { path: 'ideas/all-ideas',
    component: AllIdeasComponent,
    canActivate: [AuthGuard]
  },
  { path: 'ideas/my-ideas/details/:id',
    component: IdeaDetailsComponent,
    canActivate: [AuthGuard]
  },
  { path: 'ideas/my-ideas/edit/:id',
    component: EditComponent,
    canDeactivate: [HasUnsavedDataGuard],
    canActivate: [AuthGuard]
  },
  { path: 'ideas/all-ideas/details/:id',
    component: IdeaDetailsComponent,
    canActivate: [AuthGuard]
  },
  { path: 'leader-response',
    component: LeaderResponseOverviewComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
      expectedRoles: ['Leader', 'Admin']
  }},
  { path: 'leader-response/details/:id',
    component: LeaderResponseDetailsComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: {
      expectedRoles: ['Leader', 'Admin']
  }},
  { path: 'statistics',
    component: StatisticsComponent,
    canActivate: [AuthGuard]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full'}
];
