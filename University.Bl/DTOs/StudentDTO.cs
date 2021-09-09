using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace University.Bl.DTOs
{
      public class StudentDTO
    {

        [Required(ErrorMessage = "The ID is required")]
        public int ID { get; set; }
        [Required(ErrorMessage = "The LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The FirstMidName is required")]
        public string FirstMidName { get; set; }
        public string FullName { get; set; }
   

    }
}
