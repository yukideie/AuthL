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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace AuthL
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<AppData> appDatas = new List<AppData>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            var userAuth = AppData.context.Person.ToList().
                Where(i => i.Login == txbLog.Text && i.Password == pbPass.Password).
                FirstOrDefault();
            if (userAuth != null)
            {
                Base secw = new Base();

                this.Hide();
                secw.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }
    }
}
