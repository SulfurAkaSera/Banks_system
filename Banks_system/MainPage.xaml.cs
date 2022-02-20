using System.Windows.Controls;

namespace Banks_system
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public readonly object MainControl;
        public MainPage(Users users)
        {
            InitializeComponent();

            MainControl = this.Content;

            using (var db = new DataBase())
            {
                UserNameMainLab.Content = users.Name;

                var tempusers = new Users();
                foreach (var user in db.Users)
                {
                    if (user.Name == users.Name)
                    {
                        tempusers = user;
                        break;
                    }
                }    
                   
                var tempbank = new Bank();
                foreach (var card in db.Cards)
                {
                    if (card.User == tempusers)
                    {
                        CardNumLab.Content = card.Num;
                        CardCVVLab.Content = card.CVV;
                        CardUserNameLab.Content = users.Name;
                        BalanceLab.Content = card.Balance;

                        foreach (var bank in db.Banks)
                        {
                            tempbank = bank;
                            if (tempbank.Id == bank.Id && card.Bank == tempbank)
                            {
                                CardBankLab.Content = bank.Name;
                                break;

                            }
                        }
                    }
                }
            }
        }
    }
}
