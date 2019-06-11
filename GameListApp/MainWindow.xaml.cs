using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;


namespace GameListApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadGamesToList();
        }

        private async void LoadGamesToList()
        {
            var games = await GetGames();

            gamesListBox.ItemsSource = games;

            foreach (var game in games)
            {
                if (game.Videos.Count > 0)
                {
                    gameVideo.Navigate(new Uri(await GetVideoUrl(game.Videos[0])));
                }
            }
        }

        private Task<List<Game>> GetGames()
        {
            return Task.Run(() =>
            {
                string url = "https://api-v3.igdb.com/games/?search=&fields=age_ratings,aggregated_rating,aggregated_rating_count,alternative_names,artworks,bundles,category,collection,cover,created_at,dlcs,expansions,external_games,first_release_date,follows,franchise,franchises,game_engines,game_modes,genres,hypes,involved_companies,keywords,multiplayer_modes,name,parent_game,platforms,player_perspectives,popularity,pulse_count,rating,rating_count,release_dates,screenshots,similar_games,slug,standalone_expansions,status,storyline,summary,tags,themes,time_to_beat,total_rating,total_rating_count,updated_at,url,version_parent,version_title,videos,websites";
                HttpWebRequest gameRequest = (HttpWebRequest)WebRequest.Create(url);
                gameRequest.Accept = "application/json";
                gameRequest.Headers.Add("user-key", "e4c3e5ce51b8b8b1c9c58853c4182aca");
                WebResponse gameResponse = (HttpWebResponse)gameRequest.GetResponse();
                string json = new StreamReader(gameResponse.GetResponseStream()).ReadToEnd();

                List<Game> games = JsonConvert.DeserializeObject<List<Game>>(json);
                return games;
            });
        }

        private Task<string> GetVideoUrl(int videoId)
        {
            return Task.Run(() => 
            {
                string url = "https://api-v3.igdb.com/game_videos/?search=&fields=video_id";
                HttpWebRequest gameRequest = (HttpWebRequest)WebRequest.Create(url);
                gameRequest.Accept = "application/json";
                gameRequest.Headers.Add("user-key", "e4c3e5ce51b8b8b1c9c58853c4182aca");
                WebResponse gameResponse = (HttpWebResponse)gameRequest.GetResponse();
                string json = new StreamReader(gameResponse.GetResponseStream()).ReadToEnd();
                
                List<Video> videos = JsonConvert.DeserializeObject<List<Video>>(json);

                foreach (var video in videos)
                {
                    if (videoId == video.Id)
                    {
                        return "https://www.youtube.com/watch?v=" + video.VideoName;
                    }
                }
                return "";
            });
        }

    }
}
