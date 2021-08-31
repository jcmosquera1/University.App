using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using University.Helpers;
using System.Linq;
using System.Collections.ObjectModel;
using University.BL.Services.Implements;
namespace University.ViewModels.Forms
{
    public class StudentViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private bool _isRefreshing;
        private ObservableCollection<StudentItemViewModel> _students;
        private string _filter;
        private List<StudentItemViewModel> _allStudents;
        #endregion


        #region Properties

        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { this.SetValue(ref this._isRefreshing, value); }
        }



        public ObservableCollection<StudentItemViewModel> Students
        {
            get { return this._students; }
            set { this.SetValue(ref this._students, value); }
        }


        public string Filter
        {
            get { return this._filter; }
            set
            {
                this.SetValue(ref this._filter, value);
                this.GetStudentsByFilter();
            }
        }



        #endregion

        #region Constructor 

        public StudentViewModel()
        {
            this._apiService = new ApiService();
            this.RefreshCommand = new Command(GetStudents);
            this.RefreshCommand.Execute(null);
        }

        #endregion


        #region Methods
        async void GetStudents()
        {
            try
            {
                this.IsRefreshing = true;
                var connection = await _apiService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Notificación", "No hay internet", "Cerrar");
                    return;

                }

                var responseDTO = await _apiService.RequestAPI<List<StudentItemViewModel>>(Endpoints.URL_BASE_UNIVERSITY_API,
                    Endpoints.GET_STUDENTS,
                     null,
                     ApiService.Method.Get);
                this._allStudents = (List<StudentItemViewModel>)responseDTO.Data;
                this.Students = new ObservableCollection<CourseItemViewModel>(this._allStudents);

                this.IsRefreshing = false;

            }
            catch (Exception ex)
            {

                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");

            }
        }
        void GetStudentsByFilter()
        {
            var students = this._allStudents;
            if (!string.IsNullOrEmpty(this.Filter))
                students = students.Where(x => x.Title.ToLower().Contains(this.Filter)).ToList();
            this.Students = new ObservableCollection<StudentItemViewModel>(students);
        }




        #endregion


        #region Commands

        public Command RefreshCommand { get; set; }




        #endregion

    }
}