using System;
using System.Collections.Generic;
using System.Text;
using University.ViewModels.Forms;
using University.Views.Forms;
using Xamarin.Forms;

namespace University.ViewModels
{
   public  class MainViewModel
    {
        public CoursesViewModel Courses { get; set; }
        public CreateCourseViewModel CreateCourse { get; set; }
        public EditCourseViewModel EditCourse { get; set; }
        public CreateOfficeViewModel CreateOffice { get; set; }


        public MainViewModel ()
        {
            instance = this;
            this.Courses = new CoursesViewModel();

            this.CreateCourseCommand = new Command(GoToCreateCourse);


        }


        #region Commands
         public Command CreateCourseCommand { get; set; }

        #endregion

        #region  Methods
        async void GoToCreateCourse() {

            GetInstance().CreateCourse = new CreateCourseViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateCoursePage());
        }

        #endregion


        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance() { 
        
            if (instance == null)
                return  new MainViewModel();

            return instance;
        }
        
        #endregion

    }
}
