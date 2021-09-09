using System;
using System.Collections.Generic;
using System.Text;
using University.Bl.DTOs;
using University.BL.Services.Implements;
using University.Helpers;
using Xamarin.Forms;

namespace University.ViewModels.Forms
{
    public class EditOfficeViewModel : BaseViewModel
    {

        #region Fields
        private ApiService _apiService;
        private OfficeDTO _office;
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


        public OfficeDTO Office
        {
            get { return this._office; }
            set { this.SetValue(ref this._office, value); }
        }

        #endregion


        #region Constructor 

        public EditOfficeViewModel(OfficeDTO office)
        {
            this._apiService = new ApiService();
            this.EditOfficeCommand = new Command(EditOffices);
            this.IsEnable = true;
            this.Office = office;


        }

        #endregion



        #region Methods
        async void EditOffices()
        {
            try
            {

                if ( string.IsNullOrEmpty(this.Office.Location))
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



                var ResponseDTO = await _apiService.RequestAPI<OfficeDTO>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.PUT_OFFICES + this.Office.InstructorID,
                     this.Office,
                     ApiService.Method.Put);
                this.IsEnable = true;
                this.IsRunning = false;

                this.Office.Location = string.Empty;
                this.Office.InstructorID = Office.InstructorID = 0;
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

        public Command EditOfficeCommand { get; set; }




        #endregion



    }
}
