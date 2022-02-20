using System;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace Banks_system
{
    public partial class RegisterWindow : Window
    {
        private string Temp1 { get; set; }
        private string Temp2 { get; set; }
        private bool Reg { get; set; }
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private async Task Insert()
        {
            using (var db = new DataBase())
            {
                var UserTemp = new Users();
                db.Users.Add(new Users() {Name = RegLoginTextBox.Text });
                await db.SaveChangesAsync();

                foreach (var findusers in db.Users)
                {
                    if (findusers.Name == RegLoginTextBox.Text)
                    {
                        UserTemp = findusers;
                        break;
                    }
                }

                var BankTemp = new Bank();
                foreach (var findbanks in db.Banks)
                {
                    if (findbanks.Name == (string)RegBankComboBox.SelectedItem)
                    {
                        BankTemp = findbanks;
                        break;
                    }
                }

                db.Cards.Add(new Card()
                {
                    Bank = BankTemp,
                    Num = RegCardTextBox.Text,
                    CVV = int.Parse(RegCVVTextBox.Text),
                    Balance = new Random().Next(20, 20000),
                    User = UserTemp
                });
                await db.SaveChangesAsync();
            }
        }

        private void Init()
        {
            using (var db = new DataBase())
            {
                 foreach (var find in db.Banks)
                {
                    RegBankComboBox.Items.Add(find.Name);
                }
                
            }
        }

        private async void RegRegBut_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DataBase())
            {
                foreach (var checkusers in db.Users)
                {
                    Temp1 = checkusers.Name;
                }

                foreach (var checkcards in db.Cards)
                {
                    Temp2 = checkcards.Num;
                }
              
                if (RegLoginTextBox.Text != "" || RegCardTextBox.Text != "")
                {
                    try
                    {
                        if (Temp1 != RegLoginTextBox.Text || Temp1 == RegLoginTextBox.Text && Temp2 != RegCardTextBox.Text)
                        {
                            Match match = Regex.Match(RegCardTextBox.Text, @"([0-9][0-9][0-9][0-9]) ([0-9][0-9][0-9][0-9]) ([0-9][0-9][0-9][0-9]) ([0-9][0-9][0-9][0-9])");
                            if (match.Success)
                            {
                                Reg = true;
                                await Insert();
                            }
                            else
                                MessageBox.Show("Некорректно введены даные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            MessageBox.Show("Такая карта уже существует!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Некорректно введены даные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                    MessageBox.Show("Даные небыли введены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            if (Reg)
            {
                Reg = false;
                RegisterWindow1.Close();
            }
            
                
        }
    }
}
