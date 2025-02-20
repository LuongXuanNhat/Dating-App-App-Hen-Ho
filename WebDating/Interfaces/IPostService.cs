using WebDating.DTOs;
using WebDating.DTOs.Post;
using WebDating.Entities;

namespace WebDating.Services
{
    public interface IPostService
    {
        Task<ResultDto<PostResponseDto>> Create(CreatePostDto requestDto, string name);
        Task<ResultDto<PostResponseDto>> Update(CreatePostDto requestDto, string name);
        Task<ResultDto<PostResponseDto>> Detail(int Id);
        Task<ResultDto<string>> Delete(int Id);
        Task<ResultDto<List<PostResponseDto>>> GetAll();
        Task<ResultDto<UserShortDto>> GetUserShort(string name);
        Task<ResultDto<List<PostResponseDto>>> GetMyPost(string name);

        Task<ResultDto<List<CommentPostDto>>> GetComment(Post post);
        Task<ResultDto<List<CommentPostDto>>> DeteleComment(int id);
        Task<ResultDto<List<CommentPostDto>>> UpdateComment(CommentPostDto comment);
        Task<ResultDto<List<CommentPostDto>>> CreateComment(CommentPostDto comment, string username);
        Task<Post> GetById(int postId);
        Task<ResultDto<NumberReponse>> GetLike(PostFpkDto postFpk);
        Task<ResultDto<List<PostResponseDto>>> AddOrUnLikePost(PostFpkDto postFpk);
        Task<bool> Report(PostReportDto postReport);
        Task<ResultDto<List<PostReportDto>>> GetReport();
    }

}
