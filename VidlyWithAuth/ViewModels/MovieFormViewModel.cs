using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyWithAuth.Models;

namespace VidlyWithAuth.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }


    }
}