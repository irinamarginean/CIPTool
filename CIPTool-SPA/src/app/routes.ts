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

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent, canDeactivate: [NavigateAwayFromLoginDeactivatorService] },
  { path: 'home', component: HomeComponent },
  { path: 'ideas/add-idea', component: AddIdeaComponent, canDeactivate: [HasUnsavedDataGuard] },
  { path: 'ideas/my-ideas', component: MyIdeasComponent },
  { path: 'ideas/all-ideas', component: AllIdeasComponent },
  { path: 'ideas/my-ideas/details/:id', component: IdeaDetailsComponent },
  { path: 'ideas/my-ideas/edit/:id', component: EditComponent, canDeactivate: [HasUnsavedDataGuard] },
  { path: 'ideas/all-ideas/details/:id', component: IdeaDetailsComponent },
  { path: 'leader-response', component: LeaderResponseOverviewComponent },
  { path: 'leader-response/details/:id', component: LeaderResponseDetailsComponent },
  { path: 'statistics', component: StatisticsComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full'}
];
