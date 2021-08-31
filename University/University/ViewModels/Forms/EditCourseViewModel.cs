using System;
using University.Bl.DTOs;
using University.BL.Services.Implements;
using University.Helpers;
using Xamarin.Forms;

namespace University.ViewModels.Forms
{
    public   class EditCourseViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private CourseDTO _course;
        private bool _isEnable;
        private bool _isRunning;
        #endregion


        #region Properties

        public bool IsEnable
        {
            get { return this._isEnable; }
            set { this.SetValue(ref this._isEnable, value); }
        }


        public bool IsRunning
        {
            get { return this._isRunning; }
            set { this.SetValue(ref this._isRunning, value); }
        }


        public CourseDTO Course
        {
            get { return this._course; }
            set { this.SetValue(ref this._course, value); }
        }

             #endregion


        #region Constructor 

        public EditCourseViewModel(CourseDTO course)
        {
            this._apiService = new ApiService();
            this.EditCourseCommand = new Command(EditCourses);
            this.IsEnable = true;
            this.Course = course;


        }

        #endregion


        #region Methods
        async void EditCourses()
        {
            try
            {

                if (string.IsNullOrEmpty(this.Course.Title) || this.Course.Credits == 0 || this.Course.CourseID == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Notificación", "todos los campos son requeridos", "Cerrar");
                    return;

                }



                this.IsEnable = false;
                this.IsRunning = true;

                var connection = await _apiService.CheckConnection();
                if (connection)
                {
                    this.IsEnable = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert("Notificación", "No hay internet", "Cerrar");
                    return;

                }

             

                var ResponseDTO = await _apiService.RequestAPI<CourseDTO>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.PUT_COURSES  + this.Course.CourseID,
                     this.Course,
                     ApiService.Method.Put);
                this.IsEnable = true;
                this.IsRunning = false;

                this.Course.CourseID = this.Course.Credits = 0;
                this.Course.Title = string.Empty;
                await Application.Current.MainPage.DisplayAlert("Notificación", "SU PROCESO FUE EXITOSO", "Cerrar");
                return;

            }
            catch (Exception ex)
            {

                this.IsEnable = true;
                this.IsRunning = false;

                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");

            }



        }
        #endregion 

        #region Commands

        public Command EditCourseCommand { get; set; }




        #endregion
    }
}
