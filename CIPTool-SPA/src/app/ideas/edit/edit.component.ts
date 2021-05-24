import { EditIdeaDto } from './../../_models/editIdeaDto';
import { HasUnsavedData } from './../../_interfaces/hasUnsavedData';
import { AuthService } from './../../_services/auth.service';
import { IdeaDetails } from './../../_models/ideaDetails';
import { Component, OnInit, ViewChild, EventEmitter, Output, ChangeDetectorRef, Input } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { IdeaService } from 'src/app/_services/idea.service';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient, HttpEventType, HttpResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { AttachmentDetails } from 'src/app/_models/attachmentDetails';
import { NgForm } from '@angular/forms';
import { Attachment } from 'src/app/_models/attachment';
import { FinancialReport } from 'src/app/_models/financialReport';

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
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css', './edit.component.scss'],
  providers: [MessageService]
})
export class EditComponent implements OnInit, HasUnsavedData {

  model: IdeaDetails = {} as IdeaDetails;
  displayedColumns: string[] = ['lower-bound', 'upper-bound', 'award'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('submitIdeaForm') submitForm: NgForm;

  fileUploadProgress: number;
  fileUploadMessage: string;
  @Output() public onUploadFinished = new EventEmitter();

  @Input() selectedCategories: any[];

  timeline: any[];

  isOtherCategoryDisplayed: boolean = false;
  otherCategoryTitle: string;

  attachmentList: Array<File> = new Array<File>();
  currentDate = new Date();
  attachmentNumber = 0;

  existingFilesToDelete: Array<string> = new Array<string>();

  plannedSavings: number = 0;
  plannedExpenses: number = 0;

  actualSavings: number = 0;
  actualExpenses: number = 0;

  constructor(private httpClient: HttpClient, private ideaService: IdeaService, public authService: AuthService, private route: ActivatedRoute, private router: Router,
              private messageService: MessageService, private changeDetection: ChangeDetectorRef) { }

  ngOnInit() {
    this.loadIdeaDetails();
    this.selectedCategories = [
      { text: 'ECC Strategy', selected: false },
      { text: 'Organization', selected: false },
      { text: 'Process', selected: false },
      { text: 'R&D', selected: false },
      { text: 'Comfort related', selected: false },
      { text: 'Test IDEA', selected: false },
      { text: 'Imported', selected: false },
    ];
  }

  hasUnsavedData(): boolean {
    return this.submitForm.dirty;
  }

  loadIdeaDetails() {
    const datepipe: DatePipe = new DatePipe('en-US')
    this.ideaService.getIdeaById(this.route.snapshot.params.id).subscribe(ideaDetails => {
      this.model = ideaDetails;
      this.plannedSavings = this.model.financialReport.plannedSavings;
      this.plannedExpenses = this.model.financialReport.plannedExpenses;
      this.actualSavings = this.model.financialReport.actualSavings;
      this.actualExpenses = this.model.financialReport.actualExpenses;
      if (ideaDetails.ideaOwnerDetails.isLeader) {
        this.model.ideaOwnerDetails.groupLeaderName = ideaDetails.ideaOwnerDetails.associateName;
      }
      this.loadTimeline();
      this.selectedCategories = this.getSelectedCategories();
    });
  }

  loadTimeline() {
    this.timeline = [
      { text: 'Plan date on', date: new Date() },
      { text: 'Planned implementation date', date: new Date() },
      { text: 'Leader response on', date: this.model.leaderResponseAt },
      { text: 'Actual start implementation date', date: new Date() },
      { text: 'Actual implementation date ', date: new Date() }
    ];
    this.timeline[0].date = this.model.planDate;
    this.timeline[1].date = this.model.doDate;
    this.timeline[3].date = this.model.checkDate;
    this.timeline[4].date = this.model.actDate;
  }

  getSelectedCategories() {
    this.selectedCategories.forEach(x => {
      x.selected = this.model.categories?.includes(x.text) ? true : false;
    })
    let newCategories = [];
    let newCategoriesTitles = [];
    newCategoriesTitles =  this.model.categories?.filter(x => !this.selectedCategories.map(category => category?.text).includes(x));
    if (newCategoriesTitles?.length > 0) {
      newCategoriesTitles?.forEach(x => {
        let category: any = { };
        category.text = x;
        category.selected = true;
        this.selectedCategories.push(category);
      });
    }
    return this.selectedCategories;
  }

  selectOtherCategory() {
    this.isOtherCategoryDisplayed = !this.isOtherCategoryDisplayed;
  }

  selectCategory(category) {
    category.selected = !category.selected;
  }

  addOtherCategory() {
    let newCategory = {text: this.otherCategoryTitle, selected: true }
    this.selectedCategories.push(newCategory);
    this.isOtherCategoryDisplayed = false;
  }

  confirmImplementation(ideaId: string) {
    return this.ideaService.confirmImplementation(ideaId).subscribe(() => {
      this.router.navigate(['my-ideas']);
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

  uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let filesToUpload: File[] = files;
    this.attachmentList.push(filesToUpload[0]);
    const filesFormData = new FormData();

    Array.from(filesToUpload).map((file, index) => {
      return filesFormData.append('file' + index, file, file.name);
    });

    this.httpClient.post('http://localhost:5000/api/ideas/upload-files/' + this.model.id, filesFormData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.fileUploadProgress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.messageService.add({severity: 'info', summary: 'File upload was successful!'});
          this.onUploadFinished.emit(event.body);
        }
      });
  }

  removeFile(attachment: File) {
    this.ideaService.removeFile(this.model.id, attachment.name).subscribe();
    this.attachmentList = this.attachmentList.filter(x => x.name !== attachment.name);
  }

  removeAttachment(attachment: AttachmentDetails) {
    this.ideaService.removeFile(this.model.id, attachment.fileName).subscribe();
    this.model.attachments = this.model.attachments.filter(x => x.fileName !== attachment.fileName);
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

  isIdeaOfCurrentAssociate() {
    return this.model.ideaOwnerDetails?.associateName === this.authService.getFirstNameAndLastName();
  }

  redirectToIdeaDetails() {
    this.router.navigate(['ideas/my-ideas/details/' + this.model.id]);
  }

  saveIdea() {
    let updateIdeaDto: EditIdeaDto = {
      title: this.model.title,
      context: this.model.context,
      target: this.model.target,
      doDate: this.model.doDate,
      checkDate: this.model.checkDate,
      actDate: this.model.actDate,
      modifiedAt: new Date(),
      isAssociateResponsible: this.model.isAssociateResponsible,
      description: document.getElementById('idea-description-editor').innerText,
      richTextDescription: this.model.richTextDescription,
      categories: this.selectedCategories.filter(category => category.selected).map(category => category.text),
      attachments: this.model.attachments,
      financialReport: {} as FinancialReport
    };
    for (let file of this.attachmentList) {
      let currentAttachment = {} as Attachment;
      currentAttachment.fileName = file.name;
      currentAttachment.uploadedAt = new Date();
      updateIdeaDto.attachments.push(currentAttachment);
    }
    updateIdeaDto.financialReport.plannedSavings = this.plannedSavings as number;
    updateIdeaDto.financialReport.plannedExpenses = this.plannedExpenses as number;
    updateIdeaDto.financialReport.actualSavings = this.actualSavings as number;
    updateIdeaDto.financialReport.actualExpenses = this.actualExpenses as number;

    this.ideaService.updateIdea(this.model.id, updateIdeaDto).subscribe(() => {
      this.submitForm.form.markAsPristine();
      this.router.navigate(['ideas/my-ideas/details/' + this.model.id]);
    },
    error => {
      this.messageService.add({severity:'error', summary:'Submission was unsuccessful!'});
    });
  }
}
