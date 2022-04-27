using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExamples.DataBindingExample3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> _users;

        public MainWindow()
        {
            InitializeComponent();

            _users = new ObservableCollection<User>
            {
                new User
                {
                    Name = "User 1",
                    Id = 1
                },

                new User
                {
                    Name = "User 2",
                    Id = 2
                },

                new User
                {
                    Name = "User 3",
                    Id = 3
                },
            };

            lbUsers.ItemsSource = _users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var id = _users.Max(u => u.Id) + 1;
            _users.Add(new User{ Name = $"User {id}", Id = id });
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                (lbUsers.SelectedItem as User).Name = $"User_{DateTime.Now.Millisecond}";
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                _users.Remove(lbUsers.SelectedItem as User);
        }
    }
}