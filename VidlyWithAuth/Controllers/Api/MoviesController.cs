using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Http;
using VidlyWithAuth.Dtos;
using VidlyWithAuth.Models;

namespace VidlyWithAuth.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // /api/movies
        public IHttpActionResult GetMovies(string query = null)
        {

            var moviesQuery = _context.Movies
                .Include(movie => movie.Genre);

            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(mov => mov.NumberAvailable > 0);
                moviesQuery = moviesQuery.Where(mov => mov.Name.Contains(query));
            }

            var movieDtos = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        // GET /api/customers/1
        public IHttpActionResult GetMovie(int id)
        {   
            var movie = _context.Movies
                .Include(mov => mov.Genre)
                .SingleOrDefault(mov => mov.Id == id);

            if (movie == null)  
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]

        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            // check if customer fields are valid and not empty 
            // otherwise throw bad request 
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            // check if customer fields are valid and not empty 
            // otherwise throw bad request 
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var movieInDb = _context.Movies.SingleOrDefault(mov => mov.Id == id);

            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //we invoke the two argument Mapper.Map method
            // which track changes in (source, destination)
            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();
        }

        // DELETE /api/customers/{id}
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]

        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(mov => mov.Id == id);
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //if customer is found using supplied id 
            //then remove customer from DB

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

        }
    }
}
