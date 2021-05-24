import { Component, OnInit } from '@angular/core';
import { IdeaSummary } from '../../_models/ideaSummary';
import { IdeaService } from 'src/app/_services/idea.service';
import { AuthService } from './../../_services/auth.service';
import { FinancialReportSummary } from 'src/app/_models/financialReportSummary';

@Component({
  selector: 'app-leader-response-overview',
  templateUrl: './leader-response-overview.component.html',
  styleUrls: ['./leader-response-overview.component.css']
})
export class LeaderResponseOverviewComponent implements OnInit {

  financialReport: FinancialReportSummary = {} as FinancialReportSummary;

  waitingForApprovalIdeas: IdeaSummary[] = [];
  leaderResponses: IdeaSummary[] = [];

  isFinancialReportDisplayed: boolean = false;

  constructor(private ideaService: IdeaService, private authService: AuthService) { }

  ngOnInit() {
    this.loadIdeas();
  }

  loadIdeas() {
    this.ideaService.getLeaderIdeasByUsername(this.authService.getUsername()).subscribe(leaderResponseDto => {
      this.waitingForApprovalIdeas = leaderResponseDto.waitingForApprovalIdeasOverview
        .filter(x => x.status === 0)
        .sort((a, b) => +new Date(b.planDate) - +new Date(a.planDate));
      this.leaderResponses = leaderResponseDto.waitingForApprovalIdeasOverview
        .filter(x => x.status !== 0)
        .sort((a, b) => +new Date(b.leaderResponseAt) - +new Date(a.leaderResponseAt));
    });
  }

  showFinancialReportDialog(financialReport: FinancialReportSummary) {
    this.financialReport = financialReport;
    this.isFinancialReportDisplayed = true;
    console.log(financialReport);
  }
}
