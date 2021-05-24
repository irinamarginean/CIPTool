import { Attachment } from './../../_models/attachment';
import { AuthService } from './../../_services/auth.service';
import { IdeaDetails } from './../../_models/ideaDetails';
import { Component, OnInit, AfterViewInit, ViewChild, EventEmitter, Output, Input } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { IdeaService } from 'src/app/_services/idea.service';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';

export interface PeriodicElement {
  name: number;
  weight: number;
  symbol: number;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { name: -999999999.00, weight: 499.00, symbol: 0.00 },
  { name: 500.00, weight: 999.00, symbol: 50.00 },
  { name: 1000.00, weight: 1999.00, symbol: 90.00 },
  { name: 2000.00, weight: 2999.00, symbol: 160.00 },
  { name: 3000.00, weight: 5999.00, symbol: 210.00 },
  { name: 6000.00, weight: 9999.00, symbol: 300.00 },
  { name: 10000.00, weight: 19999.00, symbol: 500.00 },
  { name: 20000.00, weight: 29999.00, symbol: 900.00 },
  { name: 30000.00, weight: 39999.00, symbol: 1100.00 },
  { name: 40000.00, weight: 49999.00, symbol: 1200.00 },
  { name: 50000.00, weight: 999999999.0, symbol: 1500.00 },
];

@Component({
  selector: 'app-idea-details',
  templateUrl: './idea-details.component.html',
  styleUrls: ['./idea-details.component.css', './idea-details.component.scss'],
  providers: [MessageService]
})
export class IdeaDetailsComponent implements OnInit {

  model: IdeaDetails = {} as IdeaDetails;
  displayedColumns: string[] = ['lower-bound', 'upper-bound', 'award'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  @ViewChild(MatSort) sort: MatSort;

  timeline: any[];

  selectedCategories: any[] = [
    { text: 'ECC Strategy', selected: false },
    { text: 'Organization', selected: false },
    { text: 'Process', selected: false },
    { text: 'R&D', selected: false },
    { text: 'Comfort related', selected: false },
    { text: 'Test IDEA', selected: false },
    { text: 'Imported', selected: false },
  ];
  attachmentList: Array<File> = new Array<File>();

  constructor(private ideaService: IdeaService, public authService: AuthService, private route: ActivatedRoute, private router: Router,
              private messageService: MessageService) { }

  ngOnInit() {
    this.loadIdeaDetails();
  }

  loadIdeaDetails() {
    const datepipe: DatePipe = new DatePipe('en-US')
    this.ideaService.getIdeaById(this.route.snapshot.params.id).subscribe(ideaDetails => {
      this.model = ideaDetails;
      if (ideaDetails.ideaOwnerDetails.isLeader) {
        this.model.ideaOwnerDetails.groupLeaderName = ideaDetails.ideaOwnerDetails.associateName;
      }
      this.timeline = [
        { text: 'Plan date on', date: this.model.planDate },
        { text: 'Planned implementation date', date: this.model.doDate },
        { text: 'Leader response on', date: this.model.leaderResponseAt },
        { text: 'Actual start implementation date', date: this.model.checkDate },
        { text: 'Actual implementation date ', date: this.model.actDate }
      ];
    });
  }

  getSelectedCategories() {
    let existingCategories = this.selectedCategories.map(x => {
      let category: any = { };
      category.text = x.text;
      category.selected = this.model.categories?.includes(x.text) ? true : false;
      return category;
    });
    let newCategories = [];
    let newCategoriesTitles = [];
    newCategoriesTitles =  this.model.categories?.filter(x => !this.selectedCategories.map(category => category?.text).includes(x));
    if (newCategoriesTitles?.length > 0) {
    newCategories = newCategoriesTitles?.map(x => {
        let category: any = { };
        category.text = x;
        category.selected = true;
        return category;
      });
    }
    return [...existingCategories, ...newCategories];
  }

  confirmImplementation(ideaId: string) {
    return this.ideaService.confirmImplementation(ideaId).subscribe(() => {
      this.router.navigate(['ideas/my-ideas']);
    },
    error => {
      console.log(error);
    });
  }

  downloadFile(ideaId: string, fileId: string, filename: string) {
    this.ideaService.downloadFile(ideaId, fileId)
      .subscribe((response: HttpResponse<Blob>) => {
          let binaryData = [];
          binaryData.push(response.body);
          let downloadLink = document.createElement('a');
          downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, { type: 'blob' }));
          downloadLink.setAttribute('download', filename);
          document.body.appendChild(downloadLink);
          downloadLink.click();
        },
        error => {
          this.messageService.add({severity: 'error', summary: `Could not download file ${filename}!`});
        });
  }

  downloadAllFiles() {
    let allFiles: Array<Attachment> = this.model.attachments;
    for (let attachment of allFiles) {
      this.downloadFile(this.model.id, attachment.id, attachment.fileName);
    }
  }

  isIdeaOfCurrentAssociate() {
    return this.model.ideaOwnerDetails?.associateName === this.authService.getFirstNameAndLastName();
  }

  canIdeaBeEditedByCurrentUser() {
    return this.isIdeaOfCurrentAssociate() ||
           this.authService.isAdmin() ||
           (this.authService.isLeader() && this.model?.reviewerName === this.authService.getDisplayName());
  }

  redirectToEditIdea() {
    this.router.navigate(['ideas/my-ideas/edit/' + this.model.id]);
  }

  getBonusValue(balance: number) {
    if (balance === undefined) {
      balance = 0;
    }
    let bonus = ELEMENT_DATA.filter(x => x.name <= balance && x.weight > balance)[0]?.symbol;
    if (bonus === undefined) {
      bonus = 0;
    }
    return bonus;
  }
}
