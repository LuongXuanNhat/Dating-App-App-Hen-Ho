using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebDating.DTOs;
using WebDating.DTOs.Post;
using WebDating.Entities;
using AutoMapper;
using WebDating.Interfaces;
using WebDating.Data;
using Microsoft.AspNetCore.SignalR;
using WebDating.SignalR;
using Microsoft.Extensions.Hosting;

namespace WebDating.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;
        private readonly IHubContext<CommentSignalR> _commentHubContext;

        public PostService(IMapper mapper, 
            IUnitOfWork uow,
            UserManager<AppUser> userManager,
            IPhotoService photoService,
            IHubContext<CommentSignalR> chatSignalR)
        {
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
            _photoService = photoService;
            _commentHubContext = chatSignalR;
        }

        public async Task<ResultDto<List<PostResponseDto>>> AddOrUnLikePost(PostFpkDto postFpk)
        {
            var checkLike = await _uow.PostRepository.GetLikeByMultiId(postFpk.UserId, postFpk.PostId);
            if (checkLike is null)
            {
                PostLike postLike = new()
                {
                    PostId = postFpk.PostId,
                    UserId = postFpk.UserId,
                };
                await _uow.PostRepository.InsertPostLike(postLike);
            }
            else
            {
                _uow.PostRepository.DeletePostLike(checkLike);
            }
            await _uow.Complete();

           
            return await GetAll();
        }

        public async Task<ResultDto<PostResponseDto>> Create(CreatePostDto requestDto, string name)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(name);
                var post = new Post() { Content = requestDto.Content };
                post.CreatedAt = DateTime.UtcNow;
                post.IsDeleted = false;
                post.UserId = user.Id;

                await _uow.PostRepository.Insert(post);
                await _uow.Complete();

                if(requestDto.Image != null && requestDto.Image.Count > 0)
                {
                    var images = await _photoService.AddRangerPhotoAsync(requestDto.Image);
                    foreach (var image in images)
                    {
                        var img = new ImagePost(post.Id, image.SecureUrl.AbsoluteUri, image.PublicId);
                        await _uow.PostRepository.InsertImagePost(img);
                    }
                    await _uow.Complete();
                }
                
                var result = _mapper.Map<PostResponseDto>(post);
                return new SuccessResult<PostResponseDto>(result);
            }
            catch (Exception error)
            {
                // Recording error log
                return new ErrorResult<PostResponseDto>(error.Message);
            }

        }

        public async Task<ResultDto<List<CommentPostDto>>> CreateComment(CommentPostDto comment, string username)
        {
            var post = await _uow.PostRepository.GetById(comment.PostId);
            if (post == null)
            {
                return new ErrorResult<List<CommentPostDto>>("Không tìm thấy bài đọc bạn bình luận, có thể nó đã bị xóa");
            }

            var postComment = new PostComment() { PostId = post.Id, UserId = comment.UserId, Content = comment.Content };
            postComment.UpdatedAt = DateTime.UtcNow;

            await _uow.PostRepository.InsertComment(postComment);
            await _uow.Complete();

            var comments = await GetComment(post);
            await _commentHubContext.Clients.All.SendAsync("ReceiveComment", comments);

            return comments;
        }

        public async Task<ResultDto<string>> Delete(int Id)
        {
            var post = await _uow.PostRepository.GetById(Id);
            _uow.PostRepository.Delete(post);
            await _uow.Complete();
            return new SuccessResult<string>();
        }

        public async Task<ResultDto<PostResponseDto>> Detail(int Id)
        {
            var post = await _uow.PostRepository.GetById(Id);
            return new SuccessResult<PostResponseDto>(new() { Id = post.Id, Content = post.Content, Images = post.Images.Select(x => x.Path).ToList() });
        }

        public async Task<ResultDto<List<CommentPostDto>>> DeteleComment(int id)
        {
            var comment = await _uow.PostRepository.GetCommentById(id);
            var post = await _uow.PostRepository.GetById(comment.PostId);

            if (comment == null)
            {
                return new ErrorResult<List<CommentPostDto>>("Không tìm thấy bình luận");
            }
            _uow.PostRepository.DeleteComment(comment);
            await _uow.Complete();

            var comments = await GetComment(post);
            await _commentHubContext.Clients.All.SendAsync("ReceiveComment", comments);
            return comments;
        }

        public async Task<ResultDto<List<PostResponseDto>>> GetAll()
        {
            var result = await _uow.PostRepository.GetAll();
            return new SuccessResult<List<PostResponseDto>>(_mapper.Map<List<PostResponseDto>>(result));
        }

        public async Task<Post> GetById(int postId)
        => await _uow.PostRepository.GetById(postId);

        public async Task<ResultDto<List<CommentPostDto>>> GetComment(Post post)
        {
            var users = await _uow.UserRepository.GetAll();
            var postComments = await _uow.PostRepository.GetCommentsByPostId(post.Id);
            if (postComments is null) return new SuccessResult<List<CommentPostDto>>(new());
            return new SuccessResult<List<CommentPostDto>>(_mapper.Map<List<CommentPostDto>>(postComments));
        }

        public async Task<ResultDto<NumberReponse>> GetLike(PostFpkDto postFpk)
        {
            var postLikes = await _uow.PostRepository.GetPostLikesByPostId(postFpk.PostId);
            NumberReponse numberReponse = new()
            {
                Check = postLikes.Where(x => x.UserId == postFpk.UserId).FirstOrDefault() is not null ? true : false,
                Quantity = postLikes.Count()
            };
            return new SuccessResult<NumberReponse>(numberReponse);
        }

        public async Task<ResultDto<List<PostResponseDto>>> GetMyPost(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            var result = await _uow.PostRepository.GetMyPost(user.Id);
            return new SuccessResult<List<PostResponseDto>>(_mapper.Map<List<PostResponseDto>>(result));
        }

        public async Task<ResultDto<List<PostReportDto>>> GetReport()
        {
            var result = await _uow.PostRepository.GetAllReport();
            return new SuccessResult<List<PostReportDto>>(_mapper.Map<List<PostReportDto>>(result));
        }

        public async Task<ResultDto<UserShortDto>> GetUserShort(string name)
        {
            var result = await _uow.PostRepository.FindByNameAsync(name);
            
            return new SuccessResult<UserShortDto>(new UserShortDto()
            {
                FullName = result.KnownAs,
                Id = result.Id,
                Image = result.Photos.Select(x=>x.Url).FirstOrDefault()
            });
        }

        public async Task<bool> Report(PostReportDto postReport)
        {
            var check = await _uow.PostRepository.GetReport(postReport.UserId, postReport.PostId);
            if(check is not null)
            {
                check.Report = postReport.Report;
                check.Description = postReport.Description;

                _uow.PostRepository.UpdatePostReport(check);
                await _uow.Complete();
            } else
            {
                var report = new PostReportDetail()
                {
                    Checked = false,
                    Description = postReport.Description ?? "",
                    PostId = postReport.PostId,
                    UserId = postReport.UserId,
                    Report = postReport.Report,
                    ReportDate = DateTime.Now
                };

                await _uow.PostRepository.InsertPostReport(report);
                await _uow.Complete();
            }
            

            return true;
        }

        public async Task<ResultDto<PostResponseDto>> Update(CreatePostDto requestDto, string name)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(name);
                var post = await _uow.PostRepository.GetById(requestDto.Id);

                post.Content = requestDto.Content;  
                
                _uow.PostRepository.Update(post);

                if (requestDto.Image != null && requestDto.Image.Count > 0)
                {
                    await _photoService.DeleteRangerPhotoAsync(post.Images);
                    _uow.PostRepository.DeleteImages(post.Images);

                    var images = await _photoService.AddRangerPhotoAsync(requestDto.Image);
                    foreach (var image in images)
                    {
                        var img = new ImagePost(post.Id, image.SecureUrl.AbsoluteUri, image.PublicId);
                        await _uow.PostRepository.InsertImagePost(img);
                    }
                    await _uow.Complete();
                }
                    

                var result = _mapper.Map<PostResponseDto>(post);
                return new SuccessResult<PostResponseDto>(result);
            }
            catch (Exception error)
            {
                // Recording error log
                return new ErrorResult<PostResponseDto>(error.Message);
            }
        }

        public async Task<ResultDto<List<CommentPostDto>>> UpdateComment(CommentPostDto comment)
        {
            var postComment = await _uow.PostRepository.GetCommentById(comment.Id);
            var post = await _uow.PostRepository.GetById(comment.PostId);

            if (postComment == null)
            {
                return new ErrorResult<List<CommentPostDto>>("Không tìm thấy bài đọc bạn bình luận!");
            }
            postComment.Content = comment.Content;
            postComment.UpdatedAt = DateTime.UtcNow;

            _uow.PostRepository.UpdateComment(postComment);
            await _uow.Complete();

            var comments = await GetComment(post);
            await _commentHubContext.Clients.All.SendAsync("ReceiveComment", comments);

            return comments;
        }
    }

}
