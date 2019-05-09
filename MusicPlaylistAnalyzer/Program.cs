using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPlaylistAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            //Check if inputs are valid
            //
            if (args.Length != 2)
            {

                Console.WriteLine("Incorrect number of file paths given. Program should be run:\nMusicPlaylistAnalyzer <music_playlist_file_path> <report_file_path>");
                return;
            }

            string filePath = args[0];
            string outputFile = args[1];

            foreach (string input in args)
            {
                bool doc1 = IsValid(input);
                if (doc1 is false)
                {
                    return;
                }
                bool doc2 = Readable(input);
                if (doc2 is false)
                {
                    return;
                }
            }

            bool doc3 = Writable(outputFile);
            if (doc3 is false)
            {
                return;
            }


            //
            //Read from .txt file and save to list of instances of class Songs
            //
            List<Songs> songList = new List<Songs>();

            StreamReader reader = new StreamReader(filePath);
            try
            {
                reader.ReadLine();
                while (reader.Peek() != -1)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\t');
                    //var values = Array.ConvertAll(line.Split('\t'), int.Parse).ToList();
                    if (values.Length != 8)
                    {
                        Console.WriteLine("Incorrect number of values. There are {0} values and there should be 8.", values.Length);
                        return;
                    }
                    Songs a = new Songs { Name = values[0], Artist = values[1], Album = values[2], Genre = values[3], Size = int.Parse(values[4]), Time = int.Parse(values[5]), Year = int.Parse(values[6]), Plays = int.Parse(values[7])};
                    songList.Add(a);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }


            //
            //Answer Questions
            //

            string content = "";


            //Question 1
            var numPlays200 = from a in songList
                              where a.Plays >= 200
                              select new { z = a.Name, y = a.Artist, x = a.Album, w = a.Genre, v = a.Size, u = a.Time, t = a.Year, s = a.Plays};

            int num1 = numPlays200.Count();
            int numa = 0;

            content = content + "Songs that received 200 or more plays:\n";

            foreach (var item in numPlays200)
            {
                content = content + "Name: " + item.z + ", Artist: " + item.y + ", Album:" + item.x + ", Genre:" + item.w + ", Size:" + item.v + ", Time:" + item.u + ", Year:" + item.t + ", Plays:" + item.s;
                if (numa < num1 - 1)
                {
                    content = content + "\n";
                }
                else
                {
                    content = content + "\n\n";
                }
                numa++;
            }


            //Question 2
            var numAlternative = from b in songList
                                 where b.Genre == "Alternative"
                                 select b.Name;

            int num2 = numAlternative.Count();

            content = content + "Number of Alternative songs: " + num2 + "\n\n";


            //Question 3

            var numHiphop = from c in songList
                                 where c.Genre == "Hip-Hop/Rap"
                                 select c.Name;

            int num3 = numHiphop.Count();

            content = content + "Number of Hip-Hop/Rap songs: " + num3 + "\n\n";


            //Question 4
            var songFishbowl = from d in songList
                               where d.Album == "Welcome to the Fishbowl"
                               select new { z = d.Name, y = d.Artist, x = d.Album, w = d.Genre, v = d.Size, u = d.Time, t = d.Year, s = d.Plays };

            int num4 = songFishbowl.Count();
            int numb = 0;

            content = content + "Songs from the album Welcome to the Fishbowl:\n";

            foreach (var item in songFishbowl)
            {
                content = content + "Name: " + item.z + ", Artist: " + item.y + ", Album:" + item.x + ", Genre:" + item.w + ", Size:" + item.v + ", Time:" + item.u + ", Year:" + item.t + ", Plays:" + item.s;
                if (numb < num4 - 1)
                {
                    content = content + "\n";
                }
                else
                {
                    content = content + "\n\n";
                }
                numb++;
            }


            //Question 5
            var songBefore1970 = from e in songList
                               where e.Year < 1970
                               select new { z = e.Name, y = e.Artist, x = e.Album, w = e.Genre, v = e.Size, u = e.Time, t = e.Year, s = e.Plays };
                               
            int num5 = songBefore1970.Count();
            int numc = 0;

            content = content + "Songs from before 1970:\n";

            foreach (var item in songBefore1970)
            {
                content = content + "Name: " + item.z + ", Artist: " + item.y + ", Album:" + item.x + ", Genre:" + item.w + ", Size:" + item.v + ", Time:" + item.u + ", Year:" + item.t + ", Plays:" + item.s;
                if (numc < num5 - 1)
                {
                    content = content + "\n";
                }
                else
                {
                    content = content + "\n\n";
                }
                numc++;
            }


            //Question 6
            var longSong = from f in songList
                           where f.Name.Length > 85
                           select f.Name;

            int num6 = longSong.Count();
            int numd = 0;

            content = content + "Song names longer than 85 characters: ";

            foreach (var item in longSong)
            {
                content = content + item;
                if (numd < num6 - 1)
                {
                    content = content + "\n";
                }
                else
                {
                    content = content + "\n\n";
                }
                numd++;
            }


            //Question 7
            var longTime = from g in songList
                           orderby g.Time descending
                           select new { z = g.Name, y = g.Artist, x = g.Album, w = g.Genre, v = g.Size, u = g.Time, t = g.Year, s = g.Plays };

            int num7 = 1;
            int nume = 0;

            content = content + "Longest song: ";

            foreach (var item in longTime)
            {
                if (nume<num7)
                {
                    content = content + "Name: " + item.z + ", Artist: " + item.y + ", Album:" + item.x + ", Genre:" + item.w + ", Size:" + item.v + ", Time:" + item.u + ", Year:" + item.t + ", Plays:" + item.s;
                    nume++;
                }
                else
                {
                    break;
                }
            }


            //
            //Write content to output file
            //
            StreamWriter writer = new StreamWriter(outputFile);
            try
            {
                writer.WriteLine(content);
                Console.WriteLine("{0} was successfully saved.", outputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }

        }

        static bool IsValid(string doc)
        {
            if (File.Exists(doc) is false)
            {
                Console.WriteLine("File {0} does not exist", doc);
                return false;
            }
            return true;
        }

        static bool Readable(string doc)
        {
            using (FileStream fs = new FileStream(doc, FileMode.Open))
            {
                if (!fs.CanRead)
                {
                    Console.WriteLine("File {0} cannot be opened.", doc);
                    return false;
                }
                return true;
            }
        }

        static bool Writable(string doc)
        {
            using (FileStream fs = new FileStream(doc, FileMode.Open))
            {
                if (!fs.CanWrite)
                {
                    Console.WriteLine("File {0} cannot be written to.", doc);
                    return false;
                }
                return true;
            }
        }
    }

    public class Songs
    {
        public string Name;
        public string Artist;
        public string Album;
        public string Genre;
        public int Size;
        public int Time;
        public int Year;
        public int Plays;
    }
}
