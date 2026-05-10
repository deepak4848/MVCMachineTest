using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTesting.Models
{
    public class Student
    {

        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
         [System.ComponentModel.DataAnnotations.Required]
        public string Department { get; set; }
         [System.ComponentModel.DataAnnotations.Required]
        public decimal Salary { get; set; }
         [System.ComponentModel.DataAnnotations.Required]
        public string IsFullTime { get; set; }

        public List<String> Skills { get; set; }
            

    }
}