using System;
using System.Collections.Generic;
using System.Text;
using University.BL.Services.Implements;
using Xamarin.Forms;
using University.Bl.DTOs;
using University.Helpers;

namespace University.ViewModels.Forms
{
 public   class CreateOfficeViewModel :BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private string _location;
        private bool _isEnable;
        private bool _isRunning;
        private List<InstructorDTO> _instructors;
        private InstructorDTO _instructorSelected;

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

        public string Location
        {
            get { return this._location; }
            set { this.SetValue(ref this._location, value); }
        }


        public List<InstructorDTO> Instructors
        {
            get { return this._instructors; }
            set { this.SetValue(ref this._instructors, value); }
        }

        public InstructorDTO InstructorSelected
        {
            get { return this._instructorSelected; }
            set { this.SetValue(ref this._instructorSelected, value); }
        }


        #endregion


        #region Constructor 

        public CreateOfficeViewModel()
        {
            this._apiService = new ApiService();
            this.CreateOfficeCommand = new Command(CreateOffice);
            this.GetInstructorsCommand = new Command(GetInstructor);
            this.GetInstructorsCommand.Execute(null);
            this.IsEnable = true;



        }

        #endregion

        #region Methods

        async void GetInstructor()
        {
            try
            {
                var connection = await _apiService.CheckConnection();
                if (connection)
                {
                    this.IsEnable = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert("Notificación", "No hay internet", "Cerrar");
                    return;

                }



                var responseDTO = await _apiService.RequestAPI<List<InstructorDTO>>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.GET_INSTRUCTORS,
                     null,
                     ApiService.Method.Get);


                this.Instructors = (List<InstructorDTO>)responseDTO.Data;

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }
        }



        async void CreateOffice()
        {
            try
            {

                if (string.IsNullOrEmpty(this.Location) || this.InstructorSelected == null )
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

                var officeDTO = new OfficeDTO
                {
                    InstructorID = this.InstructorSelected.ID,
                    Location = this.Location
                
                    
                };


                var message = "The process is successful";

                var responseDTO = await _apiService.RequestAPI<OfficeDTO>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.POST_OFFICES,
                     officeDTO,
                     ApiService.Method.Post);
                if (responseDTO.Code < 200 || responseDTO.Code > 299)
                    message = responseDTO.Message;

                this.IsEnable = true;
                this.IsRunning = false;

                this.Location = string.Empty;
                await Application.Current.MainPage.DisplayAlert("Notificación",message, " Cerrar");
              

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

        public Command CreateOfficeCommand { get; set; }
        public Command GetInstructorsCommand { get; set; }




        #endregion

    }
}
