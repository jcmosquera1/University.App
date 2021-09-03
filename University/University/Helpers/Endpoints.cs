using System;
using System.Collections.Generic;
using System.Text;

namespace University.Helpers
{
   public class Endpoints
    {
        public static string URL_BASE_UNIVERSITY_API { get; set; } = "https://university-api.azurewebsites.net/";

        #region Courses
        public static string GET_COURSES { get; set; } = "/api/Courses/GetCourses/";
        public static string GET_STUDENTS { get; set; } = "/api/Courses/GetStudents";
        public static string POST_COURSES { get; set; } = "api/Courses";
        public static string PUT_COURSES { get; set; } = "api/Courses";
        public static string DELETE_COURSES { get; set; } = "api/Courses/";

        #endregion





        #region  Instructors
        public static string GET_INSTRUCTORS { get; set; } = "api/Instructors/GetInstructors/";

        #endregion


        public static string POST_OFFICES { get; set; } = "api/OfficeAssignments/";


    }



}
