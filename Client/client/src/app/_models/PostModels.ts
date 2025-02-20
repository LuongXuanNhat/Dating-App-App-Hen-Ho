export interface UserShortDto {
  id: number;
  fullName: string;
  image: string;
}
export interface CreatePostDto {
  id: number;
  content: string;
  image?: FileList;
}

export interface PostResponseDto {
  id: number;
  content: string;
  images: string[];
  createdAt: Date;
  updatedAt?: Date;
  userShort: UserShortDto;
  viewNumber: number;
  commentNumber: number;
  likeNumber: number;
  postComments: PostComment[];
  postLikes: PostLike[];
}

export interface PostComment {
  id: number;
  postId: number;
  userId?: number;
  content: string;
  createdAt: Date;
  updatedAt?: Date;
  postSubComments: PostSubComment[];
}

export interface PostSubComment {
  id: number;
  preCommentId?: number;
  userId?: number;
  content: string;
  createdAt?: Date;
  updatedAt?: Date;
}

export interface PostLike {
  id: number;
  postId: number;
  userId?: number;
}

export interface CommentPostDto {
  id: number;
  userId?: number;
  postId: number;
  userShort?: UserShortDto | null;
  content: string;
  createdAt: Date | string;
  updatedAt: Date | string | null;
  subComment?: SubCommentDto[] | null;
}

export interface SubCommentDto {
  id: number;
  content?: string | null;
  createdAt?: Date | null;
  updatedAt?: Date | null;
  userShort?: UserShortDto | null;
}

export interface PostFpkDto {
  postId: number;
  userId: number;
}

export interface NumberReponse {
  check: boolean;
  quantity: number;
}

export interface PostReportDto {
  id: number;
  userId: number;
  postId: number;
  description: string;
  reportDate: Date;
  checked: boolean;
  report: number;
}
