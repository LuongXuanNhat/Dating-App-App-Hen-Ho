import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { EItem } from 'src/app/_models/DatingProfile';
import { PostReportDto, UserShortDto } from 'src/app/_models/PostModels';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_service/account.service';
import { PostService } from 'src/app/_service/post-service.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css'],
})
export class ReportComponent implements OnInit {
  userShort!: UserShortDto;
  report: EItem[] = [];
  postReport: PostReportDto = {
    id: 0,
    checked: false,
    description: '',
    postId: 0,
    userId: 0,
    reportDate: new Date(),
    report: -1,
  };
  postId!: number;
  constructor(
    private service: PostService,
    private modalService: BsModalService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getUserShort();
    this.getContentReport();
  }
  getUserShort() {
    this.service.getUserShort().subscribe(
      (data: any) => {
        console.log(data);
        this.userShort = data.resultObj;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
  getContentReport() {
    this.service.GetContentReport().subscribe(
      (data: any) => {
        this.report = data;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
  Report() {
    this.postReport.userId = this.userShort.id;
    this.postReport.postId = this.postId;
    if (this.postReport.report == -1) {
      this.toastr.info('Please choose report content!');
      return;
    }

    this.service.Report(this.postReport).subscribe(
      (data: any) => {
        if (data == true) {
          this.modalService.hide();
          this.toastr.success('Report success!');
        }
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
