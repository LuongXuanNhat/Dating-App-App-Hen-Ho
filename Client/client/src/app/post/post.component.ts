import { Component, OnInit } from '@angular/core';
import { PostService } from '../_service/post-service.service';
import {
  NumberReponse,
  PostFpkDto,
  PostLike,
  PostResponseDto,
  UserShortDto,
} from '../_models/PostModels';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CreatepostComponent } from './createpost/createpost.component';
import { UpdatepostComponent } from './updatepost/updatepost.component';
import { User } from '../_models/user';
import { AccountService } from '../_service/account.service';
import { take } from 'rxjs';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ChatComponent } from './chat/chat.component';
import { ToastrService } from 'ngx-toastr';
import { Overlay } from '@angular/cdk/overlay';
import { ReportComponent } from './report/report.component';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css'],
})
export class PostComponent implements OnInit {
  userShort!: UserShortDto;
  posts: PostResponseDto[] = [];
  user!: User;
  postLike: PostFpkDto = {
    postId: 0,
    userId: 0,
  };
  numberReponse!: NumberReponse;

  constructor(
    private accountService: AccountService,
    private service: PostService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private dialog: MatDialog,
    private overlay: Overlay
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) {
          this.user = user;
        }
      },
    });
  }
  ngOnInit(): void {
    this.getUserShort();
    this.getPosts();
  }
  GetLike(postLikes: PostLike[]): any {
    return postLikes.some((postLike) => postLike.userId === this.userShort.id);
  }
  LikeOrDisLike(postId: number) {
    this.postLike.postId = postId;
    this.postLike.userId = this.userShort.id;
    this.service.LikeOrDisLike(this.postLike).subscribe(
      (data: any) => {
        this.posts = data.resultObj;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
  getPosts() {
    this.service.getPosts().subscribe(
      (data: any) => {
        console.log(data);
        if (data.isSuccessed === true) {
          this.posts = data.resultObj as PostResponseDto[];
        } else {
        }
      },
      (error: any) => {
        console.log(error);
      }
    );
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

  openCreatePostModal() {
    this.modalService.show(CreatepostComponent, {
      class: 'modal-lg',
    });
  }

  updatePost(id: number) {
    const initialState = { id: id };
    this.modalService.show(UpdatepostComponent, {
      class: 'modal-lg',
      initialState: initialState,
    });
  }

  deletePost(id: number) {
    this.service.deletePost(id).subscribe(
      (data: any) => {
        console.log(data);
        if (data.isSuccessed === true) {
          this.getPosts();
          this.toastr.success('Deleted post success');
        } else {
        }
      },
      (error: any) => {
        console.log(error);
      }
    );
  }

  Comment(postId: number) {
    this.openDialogComment('10ms', '10ms', postId);
  }

  openDialogComment(enteranimation: any, exitanimation: any, postId: number) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.hasBackdrop = false;
    const popup = this.dialog.open(ChatComponent, {
      enterAnimationDuration: enteranimation,
      exitAnimationDuration: exitanimation,
      width: '414px',
      height: '100%',
      data: {
        SubId: postId,
      },
      panelClass: 'right-aligned-dialog',
      backdropClass: 'custom-backdrop',
      // Enable scoll page
      scrollStrategy: this.overlay.scrollStrategies.noop(),
    });
  }

  Report(id: number) {
    const initialState = { postId: id };
    this.modalService.show(ReportComponent, {
      class: 'modal-lg',
      initialState: initialState,
    });
  }
}
