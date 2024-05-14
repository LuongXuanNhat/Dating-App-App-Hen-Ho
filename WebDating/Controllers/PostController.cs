﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDating.DTOs;
using WebDating.DTOs.Post;
using WebDating.Entities;
using WebDating.Services;
using WebDating.Utilities;

namespace WebDating.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) { 
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            var result = await _postService.GetAll();
            return Ok(result);
        }

        [HttpGet("MyPost")]
        [Authorize]
        public async Task<IActionResult> GetMyPost()
        {
            var result = await _postService.GetMyPost(User.Identity.Name);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto requestDto)
        {
            var result = await _postService.Create(requestDto, User.Identity.Name);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePost([FromForm] CreatePostDto requestDto)
        {
            var result = await _postService.Update(requestDto, User.Identity.Name);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _postService.Detail(id);
            return Ok(result);
        }

        [HttpGet("UserShort")]
        public async Task<IActionResult> GetUserInfor()
        {
            var result = await _postService.GetUserShort(User.Identity.Name);
            return Ok(result);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _postService.Delete(Id);
            return Ok(result);
        }

        [HttpGet("Chat")]
        public async Task<IActionResult> GetComments(int PostId)
        {
            var post = await _postService.GetById(PostId);
            var result = await _postService.GetComment(post);
            return Ok(result);
        }
        [HttpPost("Chat")]
        [Authorize]
        public async Task<IActionResult> CreateComment(CommentPostDto comment)
        {
            var result = await _postService.CreateComment(comment, User.Identity.Name);
            return Ok(result);
        }
        [HttpPut("Chat")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(CommentPostDto comment)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null || !id.Equals(comment.UserId.ToString()))
            {
                return BadRequest();
            }
            var result = await _postService.UpdateComment(comment);
            return Ok(result);
        }
        [HttpDelete("Chat")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null || !id.Equals(id))
            {
                return BadRequest();
            }
            var result = await _postService.DeteleComment(commentId);
            return Ok(result);
        }
        [HttpGet("Chat/NumberComment")]
        public async Task<IActionResult> GetCommentPost(int PostId)
        {
            var post = await _postService.GetById(PostId);
            var result = await _postService.GetComment(post);
            var numberResult = new SuccessResult<int>(result.ResultObj.Count);
            return numberResult is null ? BadRequest(numberResult) : Ok(numberResult);
        }

        [HttpGet("Like")]
        public async Task<IActionResult> GetLikePost(PostFpkDto postFpk)
        {
            var result = await _postService.GetLike(postFpk);
            return Ok(result);
        }

        [HttpPost("Like")]
        public async Task<IActionResult> Like(PostFpkDto postFpk)
        {
            var result = await _postService.AddOrUnLikePost(postFpk);
            return Ok(result);
        }

        [HttpGet("ContentReport")]
        public IActionResult GetContentReport()
        {
            var result = Utils.GetAllAccountType<Report>();
            return Ok(result);
        }

        [HttpPost("Report")]
        public async Task<IActionResult> Report([FromForm] PostReportDto postReport)
        {
            var result = await _postService.Report(postReport);
            return Ok(result);
        }
        [HttpGet("Report")]
        public async Task<IActionResult> GetReport()
        {
            var result = await _postService.GetReport();
            return Ok(result);
        }
    }
}
