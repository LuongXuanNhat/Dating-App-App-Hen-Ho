import { Component, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Toast, ToastrService } from 'ngx-toastr';
import { async, take } from 'rxjs';
import { CreatePostDto } from 'src/app/_models/PostModels';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_service/account.service';
import { PostService } from 'src/app/_service/post-service.service';

@Component({
  selector: 'app-createpost',
  templateUrl: './createpost.component.html',
  styleUrls: ['./createpost.component.css'],
})
export class CreatepostComponent {
  @ViewChild('CreatePostTemplate') CreatePostTemplate!: TemplateRef<any>;
  createPost: CreatePostDto;
  modalRef?: BsModalRef;
  selectedImages: string[] = [];
  formData: FormData;
  username!: string;
  constructor(
    private service: PostService,
    private modalService: BsModalService,
    private router: Router,
    private accountService: AccountService,
    private toastr: ToastrService
  ) {
    this.createPost = {
      id: 0,
      content: '',
      image: undefined,
    };
    this.formData = new FormData();
  }

  AddImage(event: Event) {
    this.selectedImages = [];
    const selectedFiles = (event.target as HTMLInputElement).files;
    if (selectedFiles) {
      for (let i = 0; i < selectedFiles.length; i++) {
        this.formData.append('Image', selectedFiles[i]);

        const file = selectedFiles[i];
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.selectedImages.push(e.target.result);
        };
        reader.readAsDataURL(file);
      }
    }
  }

  Post() {
    if (this.createPost.content == '') {
      this.toastr.info("You haven't written anything yet :>");
      return;
    }
    this.formData.append('Content', this.createPost.content);
    console.log(this.createPost);
    this.service.CreatePost(this.formData).subscribe(
      (data: any) => {
        console.log(data);
        if (data.isSuccessed) {
          const user$ = this.accountService.currentUser$; // Không sử dụng pipe async ở đây
          user$.pipe(take(1)).subscribe((user) => {
            if (user) {
              this.modalService.hide();
              this.router.navigate(['persionalpage/' + user.userName]);
              this.toastr.success(
                'Congratulations! You have updated dating profile success'
              );
            }
          });
        }
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
