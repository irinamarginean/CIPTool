import { IdeaStatisticsDto } from './../_models/ideaStatisticsDto';
import { StatisticsService } from './../_services/statistics.service';
import { Component, OnInit } from '@angular/core';
import jspdf from 'jspdf';
import html2canvas from 'html2canvas';
import ChartDataLabels from 'chartjs-plugin-datalabels';


@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {

  todayDate: Date;

  ideaStatistics: IdeaStatisticsDto;

  waitingForApprovalData: any;
  approvedData: any;
  postponedData: any;
  declinedData: any;
  implementedData: any;

  waitingForApprovalPercentage: any;
  approvedPercentage: any;
  postponedPercentage: any;
  declinedPercentage: any;
  implementedPercentage: any;

  registeredIdeasData: any;
  approvedIdeasData: any;

  otdDecisionData: any;
  otdImplementationData: any;

  financialBenefitsData: any;
  bonusData: any;

  financialData: any;

  options: any;
  registeredApprovedOptions: any;
  financialBenefitsOptions: any;
  financialOptions: any;
  bonusOptions: any;

  plugin: any;
  public barChartPlugins = [ChartDataLabels];

  constructor(private statisticsService: StatisticsService) { }

  async ngOnInit() {
    await this.loadStatistics();
    this.todayDate = new Date();

    this.renderWaitingForImplementation();
    this.renderOnlyApprovedPieChart();
    this.renderPostponedPieChart();
    this.renderDeclinedPieChart()
    this.renderImplementedPieChart();

    this.renderRegisteredIdeas();
    this.renderApprovedPieChart();
    this.renderOTDDecisionPieChart();
    this.renderOTDImplementationPieChart();
    this.renderFinancialBenefitsPieChart();

    this.renderFinancialInformationBarChart();
    this.renderBonusesBarChart();

    this.plugin = [ChartDataLabels];
  }

  async loadStatistics() {
    this.ideaStatistics = await this.statisticsService.getIdeaStatistics().toPromise();
  }

  renderWaitingForImplementation() {
    let waitingForApprovalIdeasNumber = this.ideaStatistics.waitingForApprovalIdeasNumber;
    let allIdeasNumber = this.ideaStatistics.allIdeasNumber;
    let otherIdeasNumber = allIdeasNumber - waitingForApprovalIdeasNumber;
    this.waitingForApprovalPercentage = waitingForApprovalIdeasNumber * 100 / allIdeasNumber;

    this.waitingForApprovalData = {
      labels: ['Waiting for approval', 'Others'],
      datasets: [
        {
            data: [waitingForApprovalIdeasNumber, otherIdeasNumber],
            backgroundColor: [
                "white",
                "#7FAAC8"
            ],
            hoverBackgroundColor: [
                "#BFD5E3",
                "#4080AD"
            ]
        }]
    };
    this.options = {
      legend: {
        display: false
      },
      tooltips: {
        enabled: true,
     }
    };
  }

  renderOnlyApprovedPieChart() {
    let approvedIdeasNumber = this.ideaStatistics.approvedIdeasNumber;
    let allIdeasNumber = this.ideaStatistics.allIdeasNumber;
    let otherIdeasNumber = allIdeasNumber - approvedIdeasNumber;
    this.approvedPercentage = approvedIdeasNumber * 100 / allIdeasNumber;

    this.approvedData = {
      labels: ['Approved', 'Others'],
      datasets: [
        {
            data: [approvedIdeasNumber, otherIdeasNumber],
            backgroundColor: [
                "white",
                "#7FB0A4"
            ],
            hoverBackgroundColor: [
                "#BFD8D1",
                "#408977"
            ]
        }]
    };
  }

  renderPostponedPieChart() {
    let postponedIdeasNumber = this.ideaStatistics.postponedIdeasNumber;
    let allIdeasNumber = this.ideaStatistics.allIdeasNumber;
    let otherIdeasNumber = allIdeasNumber - postponedIdeasNumber;
    this.postponedPercentage = postponedIdeasNumber * 100 / allIdeasNumber;

    this.postponedData = {
      labels: ['Postponed', 'Others'],
      datasets: [
        {
            data: [postponedIdeasNumber, otherIdeasNumber],
            backgroundColor: [
                "white",
                "#A8AFB5"
            ],
            hoverBackgroundColor: [
                "#D4D7DA",
                "#7D8790"
            ]
        }]
    };
  }

  renderDeclinedPieChart() {
    let declinedIdeasNumber = this.ideaStatistics.declinedIdeasNumber;
    let allIdeasNumber = this.ideaStatistics.allIdeasNumber;
    let otherIdeasNumber = allIdeasNumber - declinedIdeasNumber;
    this.declinedPercentage = declinedIdeasNumber * 100 / allIdeasNumber;

    this.declinedData = {
      labels: ['Declined', 'Others'],
      datasets: [
        {
            data: [declinedIdeasNumber, otherIdeasNumber],
            backgroundColor: [
                "white",
                "#F4808B"
            ],
            hoverBackgroundColor: [
                "#FABFC5",
                "#EF4050"
            ]
        }]
    };
  }

  renderImplementedPieChart() {
    let implementedIdeasNumber = this.ideaStatistics.implementedIdeasNumber;
    let allIdeasNumber = this.ideaStatistics.allIdeasNumber;
    let otherIdeasNumber = allIdeasNumber - implementedIdeasNumber;
    this.implementedPercentage = implementedIdeasNumber * 100 / allIdeasNumber;

    this.implementedData = {
      labels: ['Implemented', 'Others'],
      datasets: [
        {
            data: [implementedIdeasNumber, otherIdeasNumber],
            backgroundColor: [
                "white",
                "#A791BF"
            ],
            hoverBackgroundColor: [
                "#D3C8DF",
                "#7C5A9F"
            ]
        }]
    };
  }

  renderRegisteredIdeas() {
    this.registeredIdeasData = {
      labels: ['Approved', 'Postponed', 'Declined'],
      datasets: [
          {
              data: [this.ideaStatistics.approvedIdeasNumber, this.ideaStatistics.postponedIdeasNumber, this.ideaStatistics.declinedIdeasNumber],
              backgroundColor: [
                "#006249",
                "#7D8790",
                "#EA0016"
              ],
              hoverBackgroundColor: [
                "#408977",
                "#A8AFB5",
                "#F4808B"
              ]
          }]
      };
      this.registeredApprovedOptions = {
        legend: {
          display: true,
          position: 'right',
          labels: {
            fontColor: '#495057',
            fontSize: 16,
            fontFamily: 'BoschSans',
            padding: 20,
            lineHeight: 1.2
          }
        },
        tooltips: {
          enabled: true
       }
    };
  }

  renderApprovedPieChart() {
    this.approvedIdeasData = {
      labels: ['Implementation ongoing', 'Implemented'],
      datasets: [
          {
              data: [this.ideaStatistics.approvedIdeasNumber, this.ideaStatistics.implementedIdeasNumber],
              backgroundColor: [
                  "#006249",
                  "#50237F"
              ],
              hoverBackgroundColor: [
                  "#408977",
                  "#7C5A9F"
              ]
          }]
      };
  }

  renderOTDDecisionPieChart() {
    this.otdDecisionData = {
      labels: ['< 10 days', '10 to 20 days', '> 20 days'],
      datasets: [
          {
              data: [this.ideaStatistics.otdDecisionGreenCategoryNumber, this.ideaStatistics.otdDecisionYellowCategoryNumber, this.ideaStatistics.otdDecisionYellowCategoryNumber],
              backgroundColor: [
                  "#9ACE58",
                  "#FDC351",
                  "#EF4050"
              ],
              hoverBackgroundColor: [
                  "#BBDE8F",
                  "#FDD78B",
                  "#F4808B"
              ]
          }]
      };
  }

  renderOTDImplementationPieChart() {
    this.otdImplementationData = {
      labels: ['< 10 days', '10 to 20 days', '> 20 days'],
      datasets: [
          {
              data: [this.ideaStatistics.otdImplementationGreenCategoryNumber, this.ideaStatistics.otdImplementationYellowCategoryNumber, this.ideaStatistics.otdImplementationRedCategoryNumber],
              backgroundColor: [
                "#9ACE58",
                "#FDC351",
                "#EF4050"
            ],
            hoverBackgroundColor: [
                "#BBDE8F",
                "#FDD78B",
                "#F4808B"
            ]
          }]
    };
  }

  renderFinancialBenefitsPieChart() {
    this.financialBenefitsData = {
      labels: ['Financial benefits', 'No financial benefits'],
      datasets: [
          {
              data: [this.ideaStatistics.financialBenefitsIdeasNumber, this.ideaStatistics.noFinancialBenefitsIdeasNumber],
              backgroundColor: [
                  "#00A8B0",
                  "#BFC0C2"
              ],
              hoverBackgroundColor: [
                  "#40BEC4",
                  "#CFD0D1"
              ]
          }]
    };
    this.financialBenefitsOptions = {
      legend: {
        display: true,
        position: 'bottom',
        align: 'center',
        labels: {
          fontColor: '#495057',
          fontSize: 16,
          fontFamily: 'BoschSans',
          padding: 30,
          lineHeight: 1.2
        }
      },
      tooltips: {
        enabled: true
     },

    };
  }

  renderBonusesBarChart() {
    this.bonusData = {
      labels: this.ideaStatistics.bonusValues.labels,
      datasets: [
          {
              label: 'Total bonuses',
              backgroundColor: '#B90276',
              data: this.ideaStatistics.bonusValues.data
          }
      ]
    };
    this.bonusOptions = {
      legend: {
          labels: {
              fontColor: '#495057'
          }
      },
      scales: {
          xAxes: [{
              ticks: {
                  fontColor: '#495057'
              }
          }],
          yAxes: [{
              ticks: {
                  fontColor: '#495057'
              },
              beginAtZero: true
          }]
        }
      };
  }

  renderFinancialInformationBarChart() {
    this.financialData = {
      labels: this.ideaStatistics.savingsValues.labels,
      datasets: [
          {
              label: 'Total savings',
              backgroundColor: '#42A5F5',
              data: this.ideaStatistics.savingsValues.data
          },
          {
              label: 'Total expenses',
              backgroundColor: '#FFA726',
              data: this.ideaStatistics.expensesValues.data
          },
          {
            label: 'Total balance',
            backgroundColor: '#9ACE58',
            data: this.ideaStatistics.balanceValues.data
        }
      ],
      plugin: [ ChartDataLabels ]
    };
    this.plugin = {
      labels: {
        render: 'value'
      }
    };
    this.financialOptions = {
      legend: {
          labels: {
              fontColor: '#495057'
          }
      },
      scales: {
          xAxes: [{
              ticks: {
                  fontColor: '#495057'
              }
          }],
          yAxes: [{
              ticks: {
                  fontColor: '#495057'
              }
          }]
      },
      plugins: {
        datalabels: {
          anchor: 'end',
          align: 'end',
        }
      }
    };
  }

  downloadStatistics() {
    const data = document.getElementById('statistics');
    html2canvas(data).then(canvas => {
      const imgWidth = 208;
      const pageHeight = 295;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      const heightLeft = imgHeight;
      const contentDataURL = canvas.toDataURL('image/png');
      const pdf = new jspdf('p', 'mm', 'a4');
      const position = 0;
      const today = new Date().toLocaleDateString();
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
      pdf.save(`CIPTool_Statistics_${today}.pdf`);
    });
  }
}
