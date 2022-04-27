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

        public UserViewModel() : this("first name", "last name", new DateTime(default)) { }

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
            set { _model.FirstName = value; ComputeCompleteName(); OnNotifying(nameof(LastName)); }
        }

        public string LastName
        {
            get { return _model.LastName; }
            set { _model.LastName = value; ComputeCompleteName(); OnNotifying(nameof(LastName)); }
        }

        public string CompleteName { get; private set; }

        public DateTime DateOfBirth
        {
            get { return _model.DateOfBirth; }
            set { _model.DateOfBirth = value; ComputeAge(); OnNotifying(nameof(DateOfBirth)); }
        }

        public int Age { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ComputeAge()
        {
            Age = (DateTime.Now - DateOfBirth).Hours / (24 * 365);
        }

        private void ComputeCompleteName()
        {
            CompleteName = $"{FirstName} {LastName}";
        }

        private void OnNotifying(string propName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
