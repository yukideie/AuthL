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
    /// Логика взаимодействия для Change.xaml
    /// </summary>
    public partial class Change : Window
    {
        UseerEntities context = new UseerEntities();

        private Person editPerson = new Person();
        public Change(Person person)
        {
            InitializeComponent();
            
            cbGender.ItemsSource = context.Gender.ToList();
            cbGender.DisplayMemberPath = "GName";
            cbGender.SelectedIndex = person.idGender - 1;

            cbRole.ItemsSource = context.Role.ToList();
            cbRole.DisplayMemberPath = "RName";
            cbRole.SelectedIndex = person.idRole - 1;

            PersonData.IdPers = person.idPerson;

            tbxLogin.Text = person.Login;
            tbxPassword.Text = person.Password;
            tbxFName.Text = person.FName;
            tbxMName.Text = person.MName;
            tbxLName.Text = person.LName;
            cbGender.SelectedIndex = person.idGender;
            cbRole.SelectedIndex = person.idRole;

            editPerson = person;
        }
        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var chek = MessageBox.Show($"Вы хотите изменить данные ", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (string.IsNullOrEmpty(tbxLogin.Text))
            {
                MessageBox.Show("Пустое поле логина");
                return;
            }
            if (string.IsNullOrEmpty(tbxPassword.Text))
            {
                MessageBox.Show("Пустое поле пароля");
                return;
            }
            if (chek == MessageBoxResult.Yes)
            {
                editPerson.Login = tbxLogin.Text.Trim();
                editPerson.Password = tbxPassword.Text.Trim();
                editPerson.FName = tbxFName.Text.Trim();
                editPerson.LName = tbxLName.Text.Trim();
                editPerson.MName = tbxMName.Text.Trim();
                editPerson.idGender = cbGender.SelectedIndex + 1;
                editPerson.idRole = cbRole.SelectedIndex + 1;

                context.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не ввели значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
