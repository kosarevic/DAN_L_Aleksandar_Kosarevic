using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    public class Song
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public TimeSpan Length { get; set; }

        public Song()
        {
        }

        public Song(int id, string title, string author, TimeSpan length)
        {
            Id = id;
            Title = title;
            Author = author;
            Length = length;
        }
    }
}
