using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class RatingVM
    {
        public int id { get; set; }

        public string comment { get; set; }

        public DateTime? created_at { get; set; }

        public int rate { get; set; }


    }
}