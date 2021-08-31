using System;
using System.Collections.Generic;
using System.Text;
using University.BL.Services.Implements;
using Xamarin.Forms;
using University.Bl.DTOs;
using University.Helpers;
using University.Views.Forms;

namespace University.ViewModels.Forms
{
     public class CourseItemViewModel   : CourseDTO
    {
        #region Fields 
        private ApiService _apiService;


        #endregion



        #region Methods 


       



        async void EditCourse ()
        {
            MainViewModel.GetInstance().EditCourse = new EditCourseViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditCoursePage());

        }




        async void DeleteCourse()
        {
            try
            {

                var answer = await Application.Current.MainPage.DisplayAlert("Notificación", 
                    "Delete Confirm",
                    "yes",
                    "No"
                    );

                if (!answer)
                return;



                var connection = await _apiService.CheckConnection();
                if (connection)
                {

                    await Application.Current.MainPage.DisplayAlert("Notificación", "No hay internet", "Cerrar");
                    return;

                }


                var message = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<CourseDTO>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.DELETE_COURSES + this.CourseID,
                    null,
                    ApiService.Method.Delete);

                if (responseDTO.Code < 200 || responseDTO.Code > 299)
                    message = responseDTO.Message;

                await Application.Current.MainPage.DisplayAlert("Notification",
                        message,
                        "Cancel");




            }
            catch (Exception ex)
            {



                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");

            }
        }

        #endregion


            #region Commands

        public Command EditCourseCommand { get; set; }
        public Command DeleteCourseCommand { get; set; }


        #endregion


        #region Constructor 
        public CourseItemViewModel()
        {


            this._apiService = new ApiService();
            this.EditCourseCommand = new Command(EditCourse);
        this.DeleteCourseCommand = new Command(DeleteCourse);
        }
        #endregion



    }
}
