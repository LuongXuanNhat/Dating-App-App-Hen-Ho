import { Component } from '@angular/core';
import { AccountService } from '../_service/account.service';
import { PostService } from '../_service/post-service.service';
import { take } from 'rxjs';
import { User } from '../_models/user';
import {
  NumberReponse,
  PostFpkDto,
  PostLike,
  PostResponseDto,
  UserShortDto,
} from '../_models/PostModels';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UpdatepostComponent } from '../post/updatepost/updatepost.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ChatComponent } from '../post/chat/chat.component';
import { Overlay } from '@angular/cdk/overlay';
import { ReportComponent } from '../post/report/report.component';

@Component({
  selector: 'app-personalpage',
  templateUrl: './personalpage.component.html',
  styleUrls: ['./personalpage.component.css'],
})
export class PersonalpageComponent {
  user!: User;
  posts: PostResponseDto[] = [];
  userShort!: UserShortDto;
  postLike: PostFpkDto = {
    postId: 0,
    userId: 0,
  };
  numberReponse!: NumberReponse;
  constructor(
    private accountService: AccountService,
    private postService: PostService,
    private modalService: BsModalService,
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
    this.getUserShort();
    this.getPosts();
  }
  getPosts() {
    this.postService.getAll().subscribe(
      (data: any) => {
        this.posts = data.resultObj;
        console.log(this.posts);
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
  getUserShort() {
    this.postService.getUserShort().subscribe(
      (data: any) => {
        console.log(data);
        this.userShort = data.resultObj;
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
  updatePost(id: number) {
    const initialState = { id: id };
    this.modalService.show(UpdatepostComponent, {
      class: 'modal-lg',
      initialState: initialState,
    });
  }
  LikeOrDisLike(postId: number) {
    this.postLike.postId = postId;
    this.postLike.userId = this.userShort.id;
    this.postService.LikeOrDisLike(this.postLike).subscribe(
      (data: any) => {
        this.posts = data.resultObj;
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
  GetLike(postLikes: PostLike[]): any {
    return postLikes.some((postLike) => postLike.userId === this.userShort.id);
  }
}
