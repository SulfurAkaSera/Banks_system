using System.Windows;

namespace Banks_system
{
    /// <summary>
    /// Логика взаимодействия для MainUserPage.xaml
    /// </summary>
    public partial class MainUserPage : Window
    {
        public MainPage mainPage;
        public TransactionsPage transactionsPage;
        public MainUserPage(Users users)
        {
            mainPage = new MainPage(users);
            transactionsPage = new TransactionsPage(users);
            transactionsPage.ExitBut.Click += ExitBut_Click;
            mainPage.ToTransBut.Click += ToTransBut_Click;
            mainPage.DelAccountBut.Click += DelAccountBut_Click;
            InitializeComponent();
            CPage.Navigate(mainPage);
        }

        private async void DelAccountBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы точно хотите удалить аккаунт? Все ваши даннные(в том числе и привязанная карта) удалятся.", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (var db = new DataBase())
                {
                    foreach (var card in db.Cards)
                    {
                        if (card.Num == (string)mainPage.CardNumLab.Content)
                            db.Cards.Remove(card);
                    }
                    await db.SaveChangesAsync();

                    foreach (var user in db.Users)
                    {
                        if (user.Name == (string)mainPage.UserNameMainLab.Content)
                            db.Users.Remove(user);
                    }
                    await db.SaveChangesAsync();
                }
                this.Close();
            }
        }

        private void ToTransBut_Click(object sender, RoutedEventArgs e)
        {
            CPage.Navigate(transactionsPage);
        }

        private void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            CPage.Navigate(mainPage);
            Update();
        }

        public void Update()
        {
            using (var db = new DataBase())
            {
                foreach (var card in db.Cards)
                {
                    if (card.Num == (string)mainPage.CardNumLab.Content)
                    {
                        mainPage.BalanceLab.Content = card.Balance;
                        break;
                    }
                }
                        
            }
        }
    }
}
