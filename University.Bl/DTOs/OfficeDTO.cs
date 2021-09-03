using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Bl.DTOs
{
 public class OfficeDTO
    {
        public int InstructorID { get; set; }
        public string Location { get; set; }
        public InstructorDTO instructor { get; set; }

    }
}
