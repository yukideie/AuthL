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
    /// Логика взаимодействия для Base.xaml
    /// </summary>
    public partial class Base : Window
    {
        List<Role> roleList = new List<Role>();
        
        public Base()
        {
            InitializeComponent();

            dgUser.ItemsSource = AppData.context.Person.ToList();

            roleList = AppData.context.Role.ToList();
            roleList.Insert(0, new Role { RName = "Все роли" });
            cbRoleSearch.ItemsSource = roleList;
            cbRoleSearch.DisplayMemberPath = "RName";
            cbRoleSearch.SelectedIndex = 0;

            Filter();
        }

        private void btnEd_Click(object sender, RoutedEventArgs e)
        {
            if (dgUser.SelectedItem is Person person)
            {
                var resMAss = MessageBox.Show($"Вы хотите изменить пользователя?", "Предупреждение", MessageBoxButton.YesNo);
                if (resMAss == MessageBoxResult.Yes)
                {
                    Change change = new Change(person);
                    Filter();
                    change.ShowDialog();
                    Filter();
                }
                else
                {
                    return;
                }
                    
            }
            else
            {
                MessageBox.Show($"Вы не выбрали пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
            Filter();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dgUser.SelectedItem is Person person)
            {
                var resMass = MessageBox.Show($"Удалить пользователя?", "Предупреждение", MessageBoxButton.YesNo);
                if (resMass == MessageBoxResult.Yes)
                {
                    context.Person.Remove(context.Person.
                    Where(i => i.idPerson == person.idPerson).FirstOrDefault());
                    context.SaveChanges();
                    Filter();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show($"Вы не выбрали пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Filter()
        {
            var people = AppData.context.Person.ToList();

            if (cbRoleSearch.SelectedIndex != 0)
            {
                people = people.Where(i => i.idRole == cbRoleSearch.SelectedIndex).ToList();
            }

            people = people.
                Where(i => i.FName.Contains(tbSearch.Text) ||
                i.MName.Contains(tbSearch.Text) ||
                i.FName.Contains(tbSearch.Text)).ToList();

            dgUser.ItemsSource = people;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbRoleSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.ShowDialog();
            this.Close();
            Filter();
            
        }
    }
}
