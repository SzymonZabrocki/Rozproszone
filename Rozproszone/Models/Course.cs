using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rozproszone.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CoverPhoto { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Technologies { get; set; }
        public string Level { get; set; }
        public int NumberOfLessons { get; set; }
        public double Duration { get; set; }
    }
}
