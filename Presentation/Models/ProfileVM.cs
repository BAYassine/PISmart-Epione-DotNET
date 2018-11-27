using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class ProfileVM
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string telephone { get; set; }
    }
}