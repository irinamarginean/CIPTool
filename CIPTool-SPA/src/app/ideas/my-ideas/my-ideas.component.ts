import { FinancialReportSummary } from './../../_models/financialReportSummary';
import { AuthService } from './../../_services/auth.service';
import { IdeaService } from './../../_services/idea.service';
import { Component, Input, OnInit } from '@angular/core';
import { IdeaSummary } from '../../_models/ideaSummary';

@Component({
  selector: 'app-my-ideas',
  templateUrl: './my-ideas.component.html',
  styleUrls: ['./my-ideas.component.css']
})
export class MyIdeasComponent implements OnInit {

  financialReport: FinancialReportSummary = {} as FinancialReportSummary;

  waitingForApprovalIdeas: IdeaSummary[] = [];
  approvedIdeas: IdeaSummary[] = [];
  postponedIdeas: IdeaSummary[] = [];
  declinedIdeas: IdeaSummary[] = [];
  implementedIdeas: IdeaSummary[] = [];

  isFinancialReportDisplayed: boolean = false;

  constructor(private ideaService: IdeaService, private authService: AuthService) { }

  ngOnInit() {
    this.ideaService.getIdeasByUsername(this.authService.getUsername()).subscribe(ideas => {
      this.waitingForApprovalIdeas = ideas.filter(x => x.status === 0).sort((a, b) => +new Date(b.planDate) - +new Date(a.planDate));
      this.approvedIdeas = ideas.filter(x => x.status === 1).sort((a, b) => +new Date(b.leaderResponseAt) - +new Date(a.leaderResponseAt));
      this.postponedIdeas = ideas.filter(x => x.status === 2).sort((a, b) => +new Date(b.leaderResponseAt) - +new Date(a.leaderResponseAt));
      this.declinedIdeas = ideas.filter(x => x.status === 3).sort((a, b) => +new Date(b.leaderResponseAt) - +new Date(a.leaderResponseAt));
      this.implementedIdeas = ideas.filter(x => x.status === 4).sort((a, b) => +new Date(b.modifiedAt) - +new Date(a.modifiedAt));
    });
  }

  showFinancialReportDialog(financialReport: FinancialReportSummary) {
    this.financialReport = financialReport;
    this.isFinancialReportDisplayed = true;
  }
}
