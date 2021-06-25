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
using static AuthL.AppData;

namespace AuthL
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        UseerEntities context = new UseerEntities();
        public AddUser()
        {
            InitializeComponent();
            cbGender.ItemsSource = context.Gender.ToList();
            cbGender.DisplayMemberPath = "GName";
            cbGender.SelectedIndex = 0;

            cbRole.ItemsSource = context.Role.ToList();
            cbRole.DisplayMemberPath = "RName";
            cbRole.SelectedIndex = 0;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Base back = new Base();
            back.ShowDialog();
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxFName.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (string.IsNullOrEmpty(tbxMName.Text))
            {
                MessageBox.Show("Введите отчество");
                return;
            }
            if (string.IsNullOrEmpty(tbxLName.Text))
            {
                MessageBox.Show("Введите фамилию");
                return;
            }


            Person person = new Person();
            if (!string.IsNullOrWhiteSpace(tbxLogin.Text))
            {
                person.Login = tbxLogin.Text;
            }
            else
            {
                MessageBox.Show("Где логин?");
                return;
            }
            if (!string.IsNullOrWhiteSpace(tbxPassword.Text))
            {
                person.Password = tbxPassword.Text;
            }
            else
            {
                MessageBox.Show("Где пароль?");
                return;
            }

            var query = context.Person.Where(p => p.Login == tbxLogin.Text).FirstOrDefault();
            if (query != null)
            {
                MessageBox.Show("Пользователь с таким логином уже есть!");
            }
            else
            {
                person.idRole = cbRole.SelectedIndex + 1;
                person.idGender = cbGender.SelectedIndex + 1;
                person.FName = tbxFName.Text.Trim();
                person.LName = tbxLName.Text.Trim();
                person.MName = tbxMName.Text.Trim();
                context.Person.Add(person);

                context.SaveChanges();

                MessageBox.Show($"Пользователь добавлен");
                this.Hide();
                Base back = new Base();
                back.ShowDialog();
                this.Close();
            }
        }
    }
}
