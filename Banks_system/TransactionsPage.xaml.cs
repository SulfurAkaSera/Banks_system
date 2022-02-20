using System.Windows;
using System.Windows.Controls;

namespace Banks_system
{
    public partial class TransactionsPage : Page
    {
        private Users ThisUser;
        private int Money { get; set; }
        public TransactionsPage(Users users)
        {
            InitializeComponent();
            using (var db = new DataBase())
            {
                var tempUsers = new Users();
                foreach (var user in db.Users)
                {
                    if (user.Name == users.Name)
                    {
                        tempUsers = user;
                        break;
                    }
                }

                foreach (var card in db.Cards)
                {
                    if (card.User == tempUsers)
                    {
                        TransPageBalanceLab.Content = card.Balance;
                        break;
                    }
                } 
            }
            ThisUser = users;
        }

        private void NotifyUserBut_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DataBase())
            {
                var tempUsers = new Users();
                foreach (var card in db.Cards)
                {
                    if(card.Num == EnterRecipientCardNum.Text)
                    {
                        foreach (var user in db.Users)
                        {
                            tempUsers = user;
                            if (card.User == tempUsers)
                            {
                                NotifyUserLab.Content = tempUsers.Name;
                                break;
                            }
                        }
                        break;
                    }
                    if(card.Num != EnterRecipientCardNum.Text)
                        NotifyUserLab.Content = null;
                }
            }
        }

        private async void ExecuteTransBut_Click(object sender, RoutedEventArgs e)
        {

            Money = int.Parse(EnterTransMoneyTextBox.Text);
            using (var db = new DataBase())
            {
                var tempUserSend = new Users();
                foreach(var user in db.Users)
                {
                    if(user.Name == ThisUser.Name)
                    {
                        tempUserSend = user;

                        break;
                    }
                }

                foreach(var card in db.Cards)
                {
                    if(card.User == tempUserSend)
                    {
                        if (Money <= card.Balance)
                        {
                            card.Balance -= Money;
                            break;
                        }
                        else
                            MessageBox.Show("Недостаточно средств", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                await db.SaveChangesAsync();

                var tempUserRec = new Users();
                var tempCard = new Card();
                foreach (var user in db.Users)
                {
                    foreach (var card in db.Cards)
                    {
                        if(card.User == tempUserSend)
                            tempCard = card;

                        if (card.Num == EnterRecipientCardNum.Text && card.User == user)
                        {
                            tempUserRec = user;
                            card.Balance += Money;
                            break;
                        }
                    }
                }
                await db.SaveChangesAsync();

                db.Transactions.Add(new Transactions()
                {
                    Sender = tempUserSend,
                    Recipient = tempUserRec,
                    Money = Money,
                    Card = tempCard
                }) ;
                await db.SaveChangesAsync();

                Update();
            }
        }

        public void Update()
        {
            using (var db = new DataBase())
            {
                var tempUser = new Users();
                foreach (var user in db.Users)
                {
                    if (user.Name == ThisUser.Name)
                    {
                        tempUser = user;
                        break;
                    }
                }

                foreach (var card in db.Cards)
                {
                    if (card.User == tempUser)
                    {
                        TransPageBalanceLab.Content = card.Balance;
                    }
                }
            }
        }
    }   
}
