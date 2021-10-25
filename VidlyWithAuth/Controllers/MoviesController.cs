using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyWithAuth.ViewModels;
using VidlyWithAuth.Models;
using System.Collections.Specialized;

namespace VidlyWithAuth.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {

            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("Index");
            }

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {

            var movie = _context.Movies.Include(mov => mov.Genre).SingleOrDefault(mov => mov.Id == id);

            ViewBag.Movie = movie;

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create(MovieFormViewModel viewModel)
        {
            //Add movie genre using Genre model
            //This will be fixed in the future to accept a collection of genre instead of one

            if (!ModelState.IsValid)
            {
                var movieViewModel = new MovieFormViewModel
                {
                    Genre = new Genre(),
                    Movie = new Movie()
                };

                return View("Create", movieViewModel);
            }

            var addedGenre = new Genre
            {
                Name = viewModel.Genre.Name,
                Movie = viewModel.Movie,
                MovieId = viewModel.Movie.Id
            };

            _context.Genre.Add(addedGenre);

            viewModel.Movie.DateAdded = DateTime.Now;
            _context.Movies.Add(viewModel.Movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(cus => cus.Id == id);
            var genre = _context.Genre.SingleOrDefault(gen => gen.MovieId == movie.Id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genre = genre
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]

        public ActionResult Update(MovieFormViewModel viewModel)
        {

            var movieInDb = _context.Movies.Single(mov => mov.Id == viewModel.Movie.Id);
            var genreInDb = _context.Genre.SingleOrDefault(gen => gen.MovieId == movieInDb.Id);

            //check if each model attribute is validated otherwise return with validation messages
            if (!ModelState.IsValid)
            {
                var movieViewModel = new MovieFormViewModel
                {
                    Genre = genreInDb,
                    Movie = movieInDb
                };

                return View("Edit", movieViewModel);
            }

            if(viewModel.Movie.NumberInStock < movieInDb.NumberAvailable)
            {
                return HttpNotFound();
            }

            movieInDb.Name = viewModel.Movie.Name;
            movieInDb.ReleaseDate = viewModel.Movie.ReleaseDate;
            movieInDb.NumberInStock = viewModel.Movie.NumberInStock;
            movieInDb.NumberAvailable = viewModel.Movie.NumberInStock - movieInDb.NumberAvailable;
            genreInDb.Name = viewModel.Genre.Name;

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");

        }


        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, byte month)
        {
            return Content(year + "/" + month);

        }

        
    }
}