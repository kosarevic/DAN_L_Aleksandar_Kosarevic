using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    class UserViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Song> UserSongs { get; set; }

        private Song song;

        public Song Song
        {
            get { return song; }
            set
            {
                if (song != value)
                {
                    song = value;
                    OnPropertyChanged("Song");
                }
            }
        }

        public UserViewModel(User user)
        {
            Song = new Song();
            FillList(user);
        }

        /// <summary>
        /// Method fills the list dedicated to the coresponding window.
        /// </summary>
        public void FillList(User user)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand("select * from tblSong s where s.UserID = @UserID;", conn);
                query.Parameters.AddWithValue("@UserId", user.Id);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (UserSongs == null)
                    UserSongs = new ObservableCollection<Song>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Song s = new Song
                    {
                        Id = int.Parse(row[0].ToString()),
                        Title = row[1].ToString(),
                        Author = row[2].ToString(),
                        Length = TimeSpan.Parse(row[3].ToString())
                    };
                    UserSongs.Add(s);
                }
            }
        }

        public void DeleteSong()
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            con.Open();
            var cmd = new SqlCommand("delete from tblSong where SongID = @SongID;", con);
            cmd.Parameters.AddWithValue("@SongID", song.Id);
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            UserSongs.Remove(song);
            var messageBoxResult = System.Windows.MessageBox.Show("Delete Successfull", "Notification");
        }

        public void CreateSong()
        {
            //Order is inserted into database.
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"insert into tblSong values (@Title, @Author, @Length, @UserID);", conn);
                cmd.Parameters.AddWithValue("@Title", song.Title);
                cmd.Parameters.AddWithValue("@Author", song.Author);
                cmd.Parameters.AddWithValue("@Length", song.Length);
                cmd.Parameters.AddWithValue("@UserId", LoginScreen.CurrentUser.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Song successfully created.", "Notification");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
