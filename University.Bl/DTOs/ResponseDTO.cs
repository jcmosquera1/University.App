using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Bl.DTOs
{
   public class ResponseDTO
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

    }
}
