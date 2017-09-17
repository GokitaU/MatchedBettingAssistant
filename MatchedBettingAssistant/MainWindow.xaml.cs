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
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.ViewModel.Account;

namespace MatchedBettingAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var account = new Wallet() {Name = "My Wallet", StartingBalance = 10};

            var accountViewModel = new AccountViewModel(account);

            this.DataContext = accountViewModel;
        }
    }
}
