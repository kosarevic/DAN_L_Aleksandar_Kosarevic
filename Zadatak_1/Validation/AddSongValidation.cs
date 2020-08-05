using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Validation
{
    static class AddSongValidation
    {

        public static bool Validate(Song song, int h, int m, int s)
        {
            if (song.Title == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Invalid song title, please try again.", "Notification");
                return false;
            }
            else if (song.Author == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Invalid song author, please try again.", "Notification");
                return false;
            }
            else if (h < 0 || h > 23)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Invalid duration in hours, please try again.", "Notification");
                return false;
            }
            else if (m < 0 || m > 59)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Invalid duration in minutes, please try again.", "Notification");
                return false;
            }
            else if (s < 0 || s > 59)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Invalid duration in hours, please try again.", "Notification");
                return false;
            }
            else if (h == 0 && m == 0 && s == 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Invalid song duration, please try again.", "Notification");
                return false;
            }


            return true;
        }
    }
}
