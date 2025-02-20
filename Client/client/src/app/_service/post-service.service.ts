import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {
  CommentPostDto,
  PostFpkDto,
  PostReportDto,
} from '../_models/PostModels';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  Report(report: PostReportDto) {
    const postReport = new FormData();
    postReport.append('userId', report.userId.toString());
    postReport.append('postId', report.postId.toString());
    postReport.append('description', report.description);
    postReport.append('reportDate', report.reportDate.toISOString());
    postReport.append('checked', report.checked.toString());
    postReport.append('report', report.report.toString());

    return this.http.post(this.baseUrl + 'Post/Report', postReport);
  }
  GetContentReport() {
    return this.http.get(this.baseUrl + 'Post/ContentReport');
  }
  LikeOrDisLike(postFpk: PostFpkDto) {
    const params = new HttpParams()
      .set('postId', postFpk.postId)
      .set('userId', postFpk.userId);
    return this.http.post(this.baseUrl + 'Post/Like', postFpk);
  }
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  constructor(private http: HttpClient) {}

  getUserShort() {
    return this.http.get(this.baseUrl + 'Post/UserShort');
  }

  getAll() {
    return this.http.get(this.baseUrl + 'Post/MyPost');
  }

  getPostUpdate(id: number) {
    return this.http.get(this.baseUrl + 'Post/' + id);
  }

  deletePost(Id: number) {
    return this.http.delete(this.baseUrl + 'Post/delete/' + Id);
  }
  getPosts() {
    return this.http.get(this.baseUrl + 'Post');
  }
  UpdatePost(requestDto: FormData) {
    return this.http.put(this.baseUrl + 'Post', requestDto);
  }
  CreatePost(requestDto: FormData) {
    return this.http.post(this.baseUrl + 'Post', requestDto);
  }

  getPostComment(postId: any) {
    return this.http.get(this.baseUrl + 'Post/Chat?PostId=' + postId);
  }
  getChatSignRl(): string {
    return this.hubUrl + 'commentHub';
  }
  CreatePostComment(data: CommentPostDto) {
    return this.http.post(this.baseUrl + 'Post/Chat', data);
  }
  UpdatePostComment(data: CommentPostDto) {
    return this.http.put(this.baseUrl + 'Post/Chat', data);
  }
  deleteComment(id: number) {
    return this.http.delete(this.baseUrl + 'Post/Chat?commentId=' + id);
  }
}
