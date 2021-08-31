using System;
using System.Collections.Generic;
using System.Text;

namespace University.Helpers
{
   public class Endpoints
    {
        public static string URL_BASE_UNIVERSITY_API { get; set; } = "https://university-api.azurewebsites.net/";

        public static string GET_COURSES { get; set; } = "/api/Courses/GetCourses/";
        public static string GET_STUDENTS { get; set; } = "/api/Courses/GetStudents";
        public static string POST_COURSES { get; set; } = "api/Courses";
        public static string PUT_COURSES { get; set; } = "api/Courses";
        public static string DELETE_COURSES { get; set; } = "api/Courses/";



    }


    
}
