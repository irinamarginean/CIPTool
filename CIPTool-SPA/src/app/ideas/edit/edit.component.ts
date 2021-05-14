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
import { HttpClient, HttpEventType, HttpResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { AttachmentDetails } from 'src/app/_models/attachmentDetails';

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
export class EditComponent implements OnInit {

  model: IdeaDetails = {} as IdeaDetails;
  displayedColumns: string[] = ['lower-bound', 'upper-bound', 'award'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  @ViewChild(MatSort) sort: MatSort;

  fileUploadProgress: number;
  fileUploadMessage: string;
  @Output() public onUploadFinished = new EventEmitter();

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
  currentDate = new Date();
  attachmentNumber = 0;

  existingFilesToDelete: Array<string> = new Array<string>();

  constructor(private httpClient: HttpClient, private ideaService: IdeaService, public authService: AuthService, private route: ActivatedRoute, private router: Router,
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
      this.loadTimeline();
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
    this.timeline[3].date = this.model.actualStartImplementationDate;
    this.timeline[4].date = this.model.actualImplementationDate;
  }

  getSelectedCategories() {
    return this.selectedCategories.map(x => {
      let category: any = { };
      category.text = x.text;
      category.selected = this.model.categories?.includes(x.text) ? true : false;
      return category;
    });
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
    this.existingFilesToDelete.push(attachment.fileName);
    this.model.attachments = this.model.attachments.filter(x => x.fileName !== attachment.fileName);
  }

  isIdeaOfCurrentAssociate() {
    return this.model.ideaOwnerDetails?.associateName === this.authService.getFirstNameAndLastName();
  }

  redirectToEditIdea() {
    this.router.navigate(['my-ideas/edit/' + this.model.id]);
  }
}
