using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Http;
using VidlyWithAuth.Dtos;
using VidlyWithAuth.Models;
using System.Net;

namespace VidlyWithAuth.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // Create Rentals
        [HttpPost]
        public IHttpActionResult CreateRental(NewRentalDto newRental)
        {

            //Get Customer whom rented the movies
            var customerInDb = _context.Customers.SingleOrDefault(customer => customer.Id == newRental.CustomerId);

            //check if customerid is available
            if(customerInDb == null)
            {
                return BadRequest("CustomerId is not valid");
            }

            //check if user added movies

            if (newRental.MovieIds.Count == 0)
            {
                return BadRequest("No movies are supplied");
            }

            //Loop and get rented movies 
            //Then add to DB with customer

            foreach(int movieId in newRental.MovieIds)
            {
                // get movie 
                var movieInDb = _context.Movies.SingleOrDefault(movie => movie.Id == movieId);

                //check if movies is in the DB
                if (movieInDb == null)
                {
                    return BadRequest("MovieId is not found");
                }

                //check if movies is available for renting

                if (movieInDb.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available");
                }

                // update availabe movies stock
                movieInDb.NumberAvailable -= 1;
                // add new rental record with movie and customer
                var rental = new Rental();
                rental.Customer = customerInDb;
                rental.Movie = movieInDb;
                rental.DateRented = DateTime.Now;
                _context.Rentals.Add(rental);

            }

            _context.SaveChanges();
            return Ok("Added");


        }

        // GET: Rentals
        public IHttpActionResult GetRentals()
        {
            var rentalQuery = _context.Rentals.Include(rental => rental.Movie).Include(rental => rental.Customer).Where(rental => rental.DateReturned == null);

            var rentalsDtos = rentalQuery
                .ToList()
                .Select(Mapper.Map<Rental, RentalDto>);

            return Ok(rentalsDtos);
        }

        // GET: Rental
        public IHttpActionResult GetRental(int id)
        {
            var rental = _context.Rentals
                .Include(rent => rent.Movie)
                .Include(rent => rent.Customer)
                .SingleOrDefault(rent => rent.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Rental, RentalDto>(rental));
        }

        [HttpPut]
        // Edit: Rental
        //public IHttpActionResult UpdateRental(int id, NewRentalDto newRentalDto)
        //{
        //    var rentalInDb = _context.Rentals.SingleOrDefault(rental => rental.Id == id);
        //    var customerInDb = _context.Customers.SingleOrDefault(cus => cus.Id == newRentalDto.CustomerId);

        //    if (rentalInDb == null)
        //    {   
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }

        //    foreach (int movieId in newRentalDto.MovieIds)
        //    {
        //        // get movie 
        //        var movieInDb = _context.Movies.SingleOrDefault(movie => movie.Id == movieId);

        //        //check if movies is in the DB
        //        if (movieInDb == null)
        //        {
        //            return BadRequest("MovieId is not found");
        //        }

        //        //check if movies is available for renting

        //        if (movieInDb.NumberAvailable == 0)
        //        {
        //            return BadRequest("Movie is not available");
        //        }

        //        // update availabe movies stock
        //        movieInDb.NumberAvailable -= 1;
        //        // add new rental record with movie and customer
        //        var rental = new Rental();
        //        rental.Customer = customerInDb;
        //        rental.Movie = movieInDb;
        //        rental.DateRented = DateTime.Now;
        //        _context.Rentals.Add(rental);

        //    }


        //    //we invoke the two argument Mapper.Map method
        //    // which track changes in (source, destination)
        //    Mapper.Map(newRentalDto, rentalInDb);

        //    _context.SaveChanges();
        //}

        // DELETE /api/customers/{id}
        [HttpDelete]
        public void DeleteRental(int id)
        {
            var rentalInDb = _context.Rentals
                .Include(rent => rent.Movie)
                .Include(rent => rent.Customer)
                .SingleOrDefault(rent => rent.Id == id);

            if (rentalInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            rentalInDb.DateReturned = DateTime.Now;
            // update availabe movies stock
            rentalInDb.Movie.NumberAvailable += 1;
            // add new rental record with movie and customer
            _context.SaveChanges();

        }

    }
}
