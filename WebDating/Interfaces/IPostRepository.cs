using WebDating.DTOs.Post;
using WebDating.Entities;

namespace WebDating.Interfaces
{
    public interface IPostRepository : IBaseInsertRepository<Post>
        , IBaseUpdateRepository<Post>, IBaseDeleteRepository<Post>
        , IBaseGetAllRepository<Post>, IBaseGetByIdRepository<Post>
    {
        void DeleteImages(ICollection<ImagePost> images);
        Task<AppUser> FindByNameAsync(string name);
        Task<IEnumerable<Post>> GetMyPost(int id);
        Task InsertImagePost(ImagePost image);
        Task InsertComment(PostComment comment);
        Task<IEnumerable<PostComment>> GetCommentsByPostId(int id);
        Task<PostComment> GetCommentById(int id);
        void DeleteComment(PostComment comment);
        void UpdateComment(PostComment postComment);
        Task<PostLike> GetLikeByMultiId(int userId, int postId);
        Task InsertPostLike(PostLike postLike);
        void DeletePostLike(PostLike checkLike);
        Task<IEnumerable<PostLike>> GetPostLikesByPostId(int postId);
        Task<IEnumerable<PostReportDetail>> GetAllReport();
        Task InsertPostReport(PostReportDetail report);
        Task<PostReportDetail> GetReport(int userId, int postId);
        void UpdatePostReport(PostReportDetail check);
    }
}
