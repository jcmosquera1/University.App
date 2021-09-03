using System;
using System.Collections.Generic;
using System.Text;
using University.Bl.DTOs;
using University.BL.Services.Implements;
using University.Helpers;
using Xamarin.Forms;

namespace University.ViewModels.Forms
{
    public class CreateStudentViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;

        private string _lastName;
        private string _firstMidName;
        private DateTime enrollmentDate;
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




        public string LastName
        {
            get { return this._lastName; }
            set { this.SetValue(ref this._lastName, value); }
        }


        public string FirstMidName
        {
            get { return this._firstMidName; }
            set { this.SetValue(ref this._firstMidName, value); }
        }

        #endregion

        #region Constructor 

        public CreateStudentViewModel()
        {
            this._apiService = new ApiService();
            this.CreateStudentCommand = new Command(CreateStudents);
            this.IsEnable = true;

        }

        #endregion


        #region Methods
        async void CreateStudents()
        {
            try
            {

                if (string.IsNullOrEmpty(this.LastName) || string.IsNullOrEmpty(this.FirstMidName))
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

                var StudentDTO = new StudentDTO ()
                {
                    
                    FirstMidName = this.FirstMidName,
                    LastName = this.LastName
                };

                var ResponseDTO = await _apiService.RequestAPI<StudentDTO>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.GET_STUDENTS,
                     StudentDTO,
                     ApiService.Method.Post);
                this.IsEnable = true;
                this.IsRunning = false;

               
                this.LastName = string.Empty;
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

        public Command CreateStudentCommand { get; set; }




        #endregion

    }
}