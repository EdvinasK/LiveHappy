using System;
using System.Collections.Generic;
using System.Linq;
using LiveHappy.Domain.Extensions;
using LiveHappy.Domain.Models;
using LiveHappy.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiveHappy.Application.Controllers
{
    public class HumourController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HumourController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Humour
        public ActionResult Anecdotes(string searchTags = "")
        {
            var anecdotes = new List<Anecdote>();

            if (!string.IsNullOrWhiteSpace(searchTags))
                searchTags = searchTags.Trim().Replace("#", string.Empty);
            else
                searchTags = "";

            var searchTagArray = searchTags.Any(st => Char.IsWhiteSpace(st))
                                        ? searchTags.Split(' ')
                                        : new string[] { searchTags };

            if (string.IsNullOrWhiteSpace(searchTagArray.First()))
            {
                anecdotes = _context.Anecdotes
                    .Include(a => a.AnecdoteTags)
                    .ThenInclude(at => at.Tag)
                    .OrderByDescending(a => a.Id)
                    .ToList();
            }
            else
            {
                anecdotes = _context.Anecdotes
                    .Include(a => a.AnecdoteTags)
                    .ThenInclude(at => at.Tag)
                    .OrderByDescending(a => a.Id)
                    .Where(a => a.AnecdoteTags.Any(at => searchTagArray.Contains(at.Tag.Name))) //at.Tag.Name.Contains(searchTags)))
                    .ToList();
            }

            if(!string.IsNullOrWhiteSpace(searchTags))
            {
                searchTags.Split(' ').ForEach(st =>
                {
                    ViewBag.SearchTag += "#" + searchTags;
                });
            }

            //var asd = anecdotes.FirstOrDefault().AnecdoteTags.Select(asdd => asdd.Tag);

            return View(anecdotes);
        }

        // GET: Humour/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Humour/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Humour/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Anecdote anecdote)
        {
            try
            {
                var tags = anecdote.AnecdoteTags
                                .Select(at => at.Tag)
                                .Distinct()
                                .ToList();

                tags.ForEach(t => t.Id = 0); // Improve. Should be removed after ViewModel is added

                var existingTags = _context.Tags.Where(t => tags.Select(ta => ta.Name).Contains(t.Name)).ToList(); // Improve. There could be a way to join db and request collections

                foreach (var tag in tags)
                {
                    tag.Id = existingTags.FirstOrDefault(et => et.Name == tag.Name)?.Id ?? 0;
                }

                anecdote.AnecdoteTags = new List<AnecdoteTag>();

                foreach (var tag in tags)
                {
                    if(tag.Id == 0)
                        _context.Add(new AnecdoteTag() { Anecdote = anecdote, Tag = tag });
                    else
                        _context.Add(new AnecdoteTag() { Anecdote = anecdote, TagId = tag.Id });
                }
                

                _context.Anecdotes.Add(anecdote);
                _context.SaveChanges();

                return RedirectToAction(nameof(Anecdotes));
            }
            catch
            {
                return View();
            }
        }

        // GET: Humour/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Humour/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Anecdotes));
            }
            catch
            {
                return View();
            }
        }

        // GET: Humour/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Humour/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Anecdotes));
            }
            catch
            {
                return View();
            }
        }
    }
}