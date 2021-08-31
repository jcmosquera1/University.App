using System;
using System.Collections.Generic;
using System.Text;
using University.Bl.DTOs;
using University.BL.Services.Implements;
using University.Helpers;
using Xamarin.Forms;

namespace University.ViewModels.Forms
{
    public class CreateCourseViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private int _courseID;
        private string _tittle;
        private int _credits;
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


        public int CourseID
        {
            get { return this._courseID; }
            set { this.SetValue(ref this._courseID, value); }
        }

        public string Tittle
        {
            get { return this._tittle; }
            set { this.SetValue(ref this._tittle, value); }
        }


        public int Credits
        {
            get { return this._credits; }
            set { this.SetValue(ref this._credits, value); }
        }

        #endregion


        #region Constructor 

        public CreateCourseViewModel()
        {
            this._apiService = new ApiService();
            this.CreateCourseCommand = new Command(CreateCourses);
            this.IsEnable = true;
            
        }

        #endregion


        #region Methods
        async void CreateCourses()
        {
            try
            {

                if (string.IsNullOrEmpty(this.Tittle) || this.Credits == 0|| this.CourseID == 0)
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

                var CourseDTO = new CourseDTO
                {
                    CourseID = this.CourseID,
                    Title = this.Tittle,
                    Credits = this.Credits
                };

                var ResponseDTO = await _apiService.RequestAPI<CourseDTO>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.POST_COURSES,
                     CourseDTO,
                     ApiService.Method.Post);
                this.IsEnable = true;
                this.IsRunning = false;

                this.CourseID = this.Credits = 0;
                this.Tittle = string.Empty; 
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

        public Command CreateCourseCommand { get; set; }




        #endregion


    }
}
