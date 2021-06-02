using Database.DataSetTableAdapters;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
namespace Database
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Name;
        public string Password;
        private UsersTableAdapter adapter = new UsersTableAdapter();
        private DataSet set = new DataSet();
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            adapter.Fill(set.Users);
            DataContext = set.Users;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var query = from user in set.Users
                        where (user.Name == txtName.Text)
                        where (user.Password == txtPassword.Text)
                        select user;

            if(query.Count() > 0)
            {
                MainWindow window = new MainWindow();
                window.Show();
                Close();
            } else
            {
                MessageBoxResult box = MessageBox.Show("User doesn't exist. Check your values or register a new user :)", "Submit", MessageBoxButton.OK, MessageBoxImage.Error);
                txtName.Clear();
                txtPassword.Clear();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            DataSet.UsersRow row = (DataSet.UsersRow)set.Users.NewRow();
            row.Name = txtName.Text;
            row.Password = txtPassword.Text;
            set.Users.AddUsersRow(row);
            adapter.Update(set);
            MessageBoxResult box = MessageBox.Show("User registered!", "Register", MessageBoxButton.OK, MessageBoxImage.Information);
            txtName.Clear();
            txtPassword.Clear();
        }
    }
}
