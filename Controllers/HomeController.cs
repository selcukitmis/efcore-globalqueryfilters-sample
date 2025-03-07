﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCore.GlobalQueryFiltersSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.GlobalQueryFiltersSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly SampleDbContext _context;

        public HomeController(SampleDbContext context)
        {
            _context = context;

            var users = new List<User>
                {
                    new User {
                        UserId = Guid.NewGuid(),
                        UserName = "mennan",
                        Name = "Mennan",
                        IsRemoved = false
                    },
                    new User {
                        UserId = Guid.NewGuid(),
                        UserName = "anil",
                        Name = "Anıl",
                        IsRemoved = true
                    }
                };

            _context.Users.AddRange(users);

            var books = new List<Book>
                {
                    new Book {
                        BookId = Guid.NewGuid(),

                        Name = "iki",
                        IsRemoved = false
                    },
                    new Book {
                        BookId = Guid.NewGuid(),
                        Name = "Anıl",
                        IsRemoved = true
                    }
                };
            _context.Books.AddRange(books);


            _context.SaveChanges();
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            var allUsers = _context.Users.IgnoreQueryFilters().ToList();

            ViewBag.Users = users;
            ViewBag.AllUsers = allUsers;

            var books = _context.Books.ToList();
            var allBooks = _context.Books.IgnoreQueryFilters().ToList();

            ViewBag.Books = books;
            ViewBag.AllBooks = allBooks;


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
