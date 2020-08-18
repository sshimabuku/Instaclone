﻿using Instaclone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Instaclone.Controllers
{
    public class PostsController : Controller
    {
        public ApplicationDbContext _context { get; set; }
        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);

            post.DateTime = DateTime.Now;
            post.UserId = User.Identity.GetUserId();


            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Explore()
        {
            var posts = _context.Posts
                .Include(p => p.User)
                .ToList()
                .OrderByDescending(p => p.DateTime);
            return View(posts);
        }
    }
}