using System;
using System.Collections.Generic;
using System.Text;
using University.Bl.DTOs;
using University.BL.Services.Implements;
using University.Helpers;
using Xamarin.Forms;

namespace University.ViewModels.Forms
{
 public   class EditStudentViewModel :BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private StudentDTO _student ;
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


        public StudentDTO Student
        {
            get { return this._student; }
            set { this.SetValue(ref this._student, value); }
        }

        #endregion

        #region Constructor 

        public EditStudentViewModel(StudentDTO student)
        {
            this._apiService = new ApiService();
            this.EditStudentCommand = new Command(EditStudents);
            this.IsEnable = true;
            this.Student = student;

        }
        #endregion


        #region Methods
        async void EditStudents()
        {
            try
            {

                if (string.IsNullOrEmpty(this.Student.LastName) || string.IsNullOrEmpty(this.Student.FirstMidName) )
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
                    Endpoints.PUT_COURSES + this.Student.LastName,
                     this.Student,
                     ApiService.Method.Put);
                this.IsEnable = true;
                this.IsRunning = false;

                this.Student.ID = this.Student.ID = 0;
                this.Student.LastName = string.Empty;
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

        public Command EditStudentCommand { get; set; }




        #endregion
    }
}
