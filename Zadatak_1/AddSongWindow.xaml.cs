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
using Zadatak_1.Validation;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for AddSongWindow.xaml
    /// </summary>
    public partial class AddSongWindow : Window
    {
        UserViewModel uvm = new UserViewModel(LoginScreen.CurrentUser);

        public AddSongWindow()
        {
            InitializeComponent();
            DataContext = uvm;
            Hours.Text = "0";
            Minutes.Text = "0";
            Seconds.Text = "0";
        }

        private void Create_Song(object sender, RoutedEventArgs e)
        {
            if (Hours.Text.All(char.IsDigit) && Minutes.Text.All(char.IsDigit) && Seconds.Text.All(char.IsDigit))
            {
                if (AddSongValidation.Validate(uvm.Song, int.Parse(Hours.Text), int.Parse(Minutes.Text), int.Parse(Seconds.Text)))
                {
                    uvm.Song.Length = new TimeSpan(int.Parse(Hours.Text),int.Parse(Minutes.Text),int.Parse(Seconds.Text));
                    uvm.CreateSong();
                    UserWindow window = new UserWindow(LoginScreen.CurrentUser);
                    window.Show();
                    Close();
                }
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ivalid input in song duration, please try again.", "Notification");
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow(LoginScreen.CurrentUser);
            window.Show();
            Close();
        }
    }
}
