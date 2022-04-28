using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoDesktopDisplayer.Models;

namespace UserInfoDesktopDisplayer.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private UserModel _model;

        public UserViewModel(UserModel model)
        {
            _model = model;
        }

        public UserViewModel() : this("first_name", "last_name", new DateTime(default)) { }

        public UserViewModel(string firstName, string lastName, DateTime dateOfBirth)
        {
            _model = new UserModel
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };
        }

        public string FirstName
        {
            get { return _model.FirstName; }
            set
            {
                if (_model.FirstName != value)
                {
                    _model.FirstName = value;
                    //ComputeCompleteName();
                    OnNotifying(nameof(FirstName));
                    OnNotifying(nameof(CompleteName));
                }
            }
        }

        public string LastName
        {
            get { return _model.LastName; }
            set
            {
                if (value != _model.LastName)
                {
                    _model.LastName = value;
                    //ComputeCompleteName();
                    OnNotifying(nameof(LastName));
                    OnNotifying(nameof(CompleteName));
                }
            }
        }

        public string CompleteName
        {
            get { return $"{_model.FirstName} {_model.LastName}"; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] names = value.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                    if (names.Length == 1)
                    {
                        FirstName = names[0];
                    }
                    else if (names.Length == 2)
                    {
                        FirstName = names[0];
                        LastName = names[1];
                        // The update is already managed by the FirstName/LastName set method
                    }
                    else
                    {
                        FirstName = "";
                        LastName = "";
                    }
                }
            }
        }

        public DateTime? DateOfBirth
        {
            get { return _model.DateOfBirth; }
            set
            {
                if (_model.DateOfBirth != value)
                {
                    _model.DateOfBirth = value;
                    OnNotifying(nameof(DateOfBirth));
                    OnNotifying(nameof(Age));
                }
            }  
        }

        public int Age
        {
            get
            {
                if (_model.DateOfBirth != null && _model.DateOfBirth.HasValue)
                {
                    int age = DateTime.Today.Year - _model.DateOfBirth.Value.Year;

                    if (_model.DateOfBirth.Value.Date > DateTime.Today.AddYears(-age))
                        return age--;

                    return age;
                }
                else
                {
                    return 0;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //private void ComputeAge()
        //{
        //    Age = (DateTime.Now - DateOfBirth).Hours / (24 * 365);
        //}

        //private void ComputeCompleteName()
        //{
        //    CompleteName = $"{FirstName} {LastName}";
        //}

        private void OnNotifying(string propName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
