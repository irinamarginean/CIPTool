import { HasUnsavedData } from './../../_interfaces/hasUnsavedData';
import { IdeaSimilarityDto } from './../../_models/ideaSimilarityDto';
import { AddIdeaSimilarityDto } from './../../_models/addIdeaSimilarityDto';
import { SimilarityService } from './../../_services/similarity.service';
import { AddIdeaInfoDto } from './../../_models/addIdeaInfoDto';
import { FinancialReport } from './../../_models/financialReport';
import { Attachment } from './../../_models/attachment';
import { IdeaService } from './../../_services/idea.service';
import { Idea } from './../../_models/idea';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, OnInit, AfterViewInit, ViewChild, EventEmitter, Output, Input } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FormGroup, FormBuilder, Validators, FormControl, NgForm } from '@angular/forms';

export interface PeriodicElement {
  name: any;
  weight: any;
  symbol: any;
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
  selector: 'app-add-idea',
  templateUrl: './add-idea.component.html',
  styleUrls: ['./add-idea.component.css'],
  providers: [MessageService]
})
export class AddIdeaComponent implements OnInit, AfterViewInit, HasUnsavedData {
  addIdeaInfoDto: AddIdeaInfoDto = {} as AddIdeaInfoDto;
  addIdeaSimilarityDto: AddIdeaSimilarityDto = {} as AddIdeaSimilarityDto;
  similarIdeas: IdeaSimilarityDto[] = [] as IdeaSimilarityDto[];

  model: Idea = {} as Idea;

  selectedCategories: any[] = [
    { text: 'ECC Strategy', selected: false },
    { text: 'Organization', selected: false },
    { text: 'Process', selected: false },
    { text: 'R&D', selected: false },
    { text: 'Comfort related', selected: false },
    { text: 'Test IDEA', selected: false },
    { text: 'Imported', selected: false },
  ];

  isOtherCategoryDisplayed: boolean = false;
  otherCategoryTitle: string;

  plannedSavings: number = 0;
  plannedExpenses: number = 0;

  actualSavings: number = 0;
  actualExpenses: number = 0;

  isDescriptionEditorTouched: boolean = false;
  isSubmissionButtonClicked: boolean = false;

  attachmentList: Array<File> = new Array<File>();

  currentDate = new Date();
  attachmentNumber = 0;

  fileUploadProgress: number;
  fileUploadMessage: string;
  @Output() public onUploadFinished = new EventEmitter();

  isIdeaSimilarityDialogDisplayed: boolean = false;

  form: FormGroup;

  constructor(private httpClient: HttpClient, private ideaService: IdeaService, private router: Router, private messageService: MessageService,
              private similarityService: SimilarityService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.loadIdeaInfo();
    this.model.isAssociateResponsible = true;
  }

  hasUnsavedData(): boolean {
    return this.submitForm.dirty;
  }

  createForm() {
    this.form = new FormGroup({
      title: new FormControl(this.model.title, Validators.required),
      context: new FormControl(this.model.context, Validators.required),
      target: new FormControl(this.model.target, Validators.required),
      plannedImplementationDate: new FormControl(this.model.doDate, Validators.required),
      description: new FormControl(this.model.richTextDescription, Validators.required)
    });
  }

  displayedColumns: string[] = ['lower-bound', 'upper-bound', 'award'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('submitIdeaForm') submitForm: NgForm;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  loadIdeaInfo() {
    this.ideaService.getAddIdeaInfo().subscribe(addIdeaInfoDto => {
      this.addIdeaInfoDto = addIdeaInfoDto;
      if (this.addIdeaInfoDto.isLeader) {
        this.addIdeaInfoDto.groupLeaderName = this.addIdeaInfoDto.associateName;
      }
    });
  }

  scroll(el: HTMLElement) {
    el.scrollIntoView({behavior: 'smooth'});
  }

  redirectToHome() {
    this.router.navigate(['home']);
  }

  selectCategory(category) {
    category.selected = !category.selected;
  }

  touchDescriptionEditor() {
    this.isDescriptionEditorTouched = true;
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

  selectOtherCategory() {
    this.isOtherCategoryDisplayed = !this.isOtherCategoryDisplayed;
  }

  addOtherCategory() {
    if (this.otherCategoryTitle === undefined || this.otherCategoryTitle === "") {
      this.messageService.add({severity:'error', summary:'The category cannot be empty!'});
    } else {
      let newCategory = {text: this.otherCategoryTitle, selected: true }
      this.selectedCategories.push(newCategory);
    }
    this.otherCategoryTitle = "";
    this.isOtherCategoryDisplayed = false;
  }

  removeAttachment(attachment: File) {
    this.ideaService.removeFile(this.addIdeaInfoDto.id, attachment.name).subscribe();
    this.attachmentList =  this.attachmentList.filter(x => x.name !== attachment.name);
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

    this.httpClient.post('http://localhost:5000/api/ideas/upload-files/' + this.addIdeaInfoDto.id, filesFormData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.fileUploadProgress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.messageService.add({severity: 'info', summary: `File upload was successful!`});
          this.onUploadFinished.emit(event.body);
        }
      });
  }

  showIdeaSimilarityDialog() {
    this.isSubmissionButtonClicked = true;
    this.createForm();
    if (this.form.valid) {
      let addIdeaSimilarityDto: AddIdeaSimilarityDto = {
        title: this.model.title,
        context: this.model.context,
        target: this.model.target,
        description: document.getElementById('idea-description-editor').innerText,
        categories: this.selectedCategories.filter(category => category.selected).map(category => category.text)
      };
        this.similarityService.addIdea(addIdeaSimilarityDto).subscribe((similarIdeas: IdeaSimilarityDto[]) => {
          this.similarIdeas = similarIdeas;
      },
      error => {
        this.messageService.add({severity: 'error', summary: 'Similarity computation was unsuccessful!'});
      });
      this.isIdeaSimilarityDialogDisplayed = true;
    } else {
      this.messageService.add({severity: 'error', summary: 'Mandatory fields not set!'});
    }
  }

  hideIdeaSimilarityDialog() {
    this.isIdeaSimilarityDialogDisplayed = false;
    this.messageService.add({severity: 'warn', summary: 'Submission was cancelled!'});
  }

  submitIdea() {
    this.model.description = document.getElementById('idea-description-editor').innerText;
    this.model.planDate = new Date();
    this.model.modifiedAt = new Date();
    this.model.categories = this.selectedCategories.filter(category => category.selected).map(category => category.text);
    this.model.attachments = new Array<Attachment>();
    for (let file of this.attachmentList) {
      let currentAttachment = {} as Attachment;
      currentAttachment.fileName = file.name;
      currentAttachment.uploadedAt = new Date();
      this.model.attachments.push(currentAttachment);
    }
    this.model.financialReport = {} as FinancialReport;
    this.model.financialReport.plannedSavings = this.plannedSavings as number;
    this.model.financialReport.plannedExpenses = this.plannedExpenses as number;
    this.model.financialReport.actualSavings = this.actualSavings as number;
    this.model.financialReport.actualExpenses = this.actualExpenses as number;
    this.model.financialReport.uploadedAt = new Date();
    this.model.financialReport.modifiedAt = new Date();
    this.model.isIdeaSavingMoney = false;

    this.ideaService.addIdea(this.addIdeaInfoDto.id, this.model).subscribe(() => {
      this.submitForm.form.markAsPristine();
      this.router.navigate(['ideas/my-ideas']);
    },
    error => {
      this.messageService.add({severity:'error', summary:'Submission was unsuccessful!'});
    });
  }
}
