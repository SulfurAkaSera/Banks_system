using System.Windows;

namespace Banks_system
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegBut_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regwin = new RegisterWindow();
            regwin.Show();
        }

        private void SignBut_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new DataBase())
            {
                bool dist = false;
                foreach(var checkUsers in db.Users)
                {
                    if(checkUsers.Name == SignUserTextBox.Text)
                    {
                        dist = true;
                        MainUserPage mainUserPage = new MainUserPage(checkUsers);
                        mainUserPage.Show();
                        this.Close();
                    }
                }
                if(!dist)
                {
                    MessageBox.Show("Такого пользователя не существует!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
