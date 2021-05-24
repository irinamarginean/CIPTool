import { SimilarityService } from './_services/similarity.service';
import { JwtInterceptor } from './_services/JwtInterceptor';
import { IdeaService } from './_services/idea.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from './_services/auth.service';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSliderModule } from '@angular/material/slider';
import { MatCheckboxModule } from '@angular/material/checkbox'
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule} from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EditorModule } from 'primeng/editor';
import { MatTableModule } from '@angular/material/table';
import { DialogModule } from 'primeng/dialog';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TimelineModule } from 'primeng/timeline';
import { TooltipModule } from 'primeng/tooltip';
import { NavbarService } from './_services/navbar.service';
import { NavigateAwayFromLoginDeactivatorService } from './_services/navigateAwayFromLoginDeactivator.service';
import { WinAuthInterceptor } from './_services/winAuthInterceptor';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { InputNumberModule } from 'primeng/inputnumber';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ChartModule } from 'primeng/chart';
import { StatisticsService } from './_services/statistics.service';
import { ChartsModule } from 'ng2-charts';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ProgressBarModule } from 'primeng/progressbar';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { AddIdeaComponent } from './ideas/add-idea/add-idea.component';
import { MyIdeasComponent } from './ideas/my-ideas/my-ideas.component';
import { AllIdeasComponent } from './ideas/all-ideas/all-ideas.component';
import { LeaderResponseOverviewComponent } from './leader-response/leader-response-overview/leader-response-overview.component';
import { LeaderResponseDetailsComponent } from './leader-response/leader-response-details/leader-response-details.component';
import { IdeaDetailsComponent } from './ideas/idea-details/idea-details.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { EditComponent } from './ideas/edit/edit.component';
import { HasUnsavedDataGuard } from './_guards/hasUnsavedDataGuard';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
      LoginComponent,
      NavComponent,
      HomeComponent,
      AddIdeaComponent,
      MyIdeasComponent,
      AllIdeasComponent,
      IdeaDetailsComponent,
      LeaderResponseOverviewComponent,
      LeaderResponseDetailsComponent,
      StatisticsComponent,
      EditComponent
   ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
         tokenGetter
      }
  }),
    BrowserAnimationsModule,
    MatSliderModule,
    MatCheckboxModule,
    MatGridListModule,
    MatButtonModule,
    MatIconModule,
    EditorModule,
    MatTableModule,
    DialogModule,
    TimelineModule,
    TooltipModule,
    ToastModule,
    ButtonModule,
    MessageModule,
    MessagesModule,
    InputNumberModule,
    MatProgressSpinnerModule,
    ChartModule,
    ChartsModule,
    InputTextareaModule,
    ProgressBarModule
  ],
  providers: [
    AuthService,
    IdeaService,
    SimilarityService,
    StatisticsService,
    NavbarService,
    NavigateAwayFromLoginDeactivatorService,
    HasUnsavedDataGuard,
    // { provide: HTTP_INTERCEPTORS, useClass: WinAuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
