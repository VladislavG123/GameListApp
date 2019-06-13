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
    public partial class MainWindow : Window
    {
        private List<Game> _games;
        public MainWindow()
        {
            InitializeComponent();

            LoadGamesToList();
        }

        private async void LoadGamesToList()
        {
            var games = await GetGames();

            gamesListBox.ItemsSource = games;
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

                _games = JsonConvert.DeserializeObject<List<Game>>(json);
                return _games;
            });
        }

        private Task<string> GetScreenUrl(int screenId)
        {
            return Task.Run(() =>
            {
                string url = "https://api-v3.igdb.com/screenshots/?search=&fields=image_id,url";
                HttpWebRequest gameRequest = (HttpWebRequest)WebRequest.Create(url);
                gameRequest.Accept = "application/json";
                gameRequest.Headers.Add("user-key", "e4c3e5ce51b8b8b1c9c58853c4182aca");
                WebResponse gameResponse = (HttpWebResponse)gameRequest.GetResponse();
                string json = new StreamReader(gameResponse.GetResponseStream()).ReadToEnd();

                List<Screen> photos = JsonConvert.DeserializeObject<List<Screen>>(json);

                foreach (var photo in photos)
                {
                    if (screenId == photo.Id)
                    {
                        return photo.Url;
                    }
                }
                return "https://uoslab.com/images/tovary/no_image.jpg";
            });
        }





        private async void GamesListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int counter = 0;
            foreach (var game in _games)
            {
                if (counter == gamesListBox.SelectedIndex)
                {
                    gameNameTextBox.Content = game.Name;

                    categoryLabel.Content = game.Category;
                    createdAtLabel.Content = game.CreatedAt;
                    firstReleaseLabel.Content = game.FirstReleaseDate;
                    foreach (var gameMod in game.GameModes)
                    {
                        gameModesLabel.Content += gameMod.ToString();

                    }
                    
                    foreach (var company in game.InvolvedCompanies)
                    {
                       companiesLabel.Content += company.ToString();

                    }

                    foreach (var platform in game.Platforms)
                    {
                        platformsLabel.Content += platform.ToString();

                    }

                    foreach (var perspectivity in game.PlayerPerspectives)
                    {
                        playerPerspectivesLabel.Content += perspectivity.ToString();

                    }

                    popularityLabel.Content = game.Popularity;

                    foreach (var similarGame in game.SimilarGames)
                    {
                        similarGamesLabel.Content += similarGame.ToString();

                    }

                    updatedAtLabel.Content = game.UpdatedAt;
                    ratingLabel.Content = game.Rating;

                    try
                    {
                        if (game.Videos.Count > 0)
                        {
                            image.Source = new Uri(await GetScreenUrl(game.Screenshots[0]));
                        }
                    }
                    catch (Exception)
                    { }
                    return;
                }
                counter++;
            }
        }
    }
}
