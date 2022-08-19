using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Tutorial.Commands;
using Wpf_Tutorial.Models;
using Wpf_Tutorial.Services;

namespace Wpf_Tutorial.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        EmployeeService ObjEmployeeService;

        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        private ObservableCollection<Employee> employeesList;

        public ObservableCollection<Employee> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value; OnPropertyChanged("EmployeesList"); }
        }

        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>( ObjEmployeeService.GetAll());
        }
        #endregion

        private Employee currentEmployee;

        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChanged("CurrentEmployee"); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region SaveOperation
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                var isSaved = ObjEmployeeService.Add(CurrentEmployee);
                LoadData();
                if (isSaved)
                {
                    Message = "Employee Details Saved";
                }
                else
                {
                    Message = "Save Failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region SearchOperation
        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        
        public void Search()
        {
            try
            {
                var ObjEmployee = ObjEmployeeService.Search(CurrentEmployee.Id);
                if(ObjEmployee != null)
                {
                    CurrentEmployee.Name = ObjEmployee.Name;
                    CurrentEmployee.Age = ObjEmployee.Age;
                    Message = "Employee Details Below!";
                }
                else
                {
                    Message = "Employee Not Found!";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;

            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeeService.Update(CurrentEmployee);
                if (IsUpdated)
                {
                    Message = "Employee Updated!";
                    LoadData();
                }
                else
                {
                    Message = "Update Failed!";
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion 

        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        
        public void Delete()
        {
            try
            {
                var IsDeleted = ObjEmployeeService.Delete(CurrentEmployee.Id);
                if (IsDeleted)
                {
                    Message = "Employee Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Employee Not Found!";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}
