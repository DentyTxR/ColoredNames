using Exiled.API.Features;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System;
using MEC;

namespace ColoredNames.Features
{
    public class DatabaseHandler
    {
        private static readonly string DataPath = Path.Combine(Paths.Configs, "ColoredNames");
        private const string DataFile = "data.yml";
        public UserDataCollection cache = new UserDataCollection();

        static DatabaseHandler()
        {
            Directory.CreateDirectory(DataPath);
        }

        public DatabaseHandler()
        {
            cache = new UserDataCollection();
            EnsureDataFileExists();
            LoadData();
        }

        public void EnsureDataFileExists()
        {
            string fullPath = Path.Combine(DataPath, DataFile);
            if (!File.Exists(fullPath))
            {
                Log.Info("data.yml does not exist, creating...");

                try
                {
                    File.WriteAllText(fullPath, "# Below is an example if you are manually writing (its also the plugin dev)");

                    Timing.CallDelayed(1f, () =>
                    {
                        DatabaseMethods.AddUser("76561198972907216@steam", "red", true);
                        SaveData();
                    });

                    Log.Info("data.yml created");
                }
                catch (Exception ex)
                {
                    Log.Error($"data.yml was not created\n{ex}");
                }
            }
        }

        public void SaveData()
        {
            try
            {
                string yaml = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build()
                    .Serialize(cache);

                File.WriteAllText(Path.Combine(DataPath, DataFile), yaml);
                Log.Debug($"Successfully saved data to {DataFile}.");

                foreach (Player player in Player.List.Where(p => cache.Users.Any(u => u.UserId == p.UserId)))
                {
                    UserData cachedUser = cache.Users.First(u => u.UserId == player.UserId);
                    Log.Debug($"Processing player: {player.UserId} with cached color: {cachedUser.Color}");

                    if (player.RankName.IsEmpty() || cachedUser.OverrideBadge)
                    {
                        player.RankColor = cachedUser.Color;
                        player.RankName = " ";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"Error while saving data:\n {ex.Message}");
            }
        }

        public void LoadData()
        {
            try
            {
                string fullPath = Path.Combine(DataPath, DataFile);
                if (File.Exists(fullPath))
                {
                    string yaml = File.ReadAllText(fullPath);
                    Log.Debug($"Loading data from {DataFile}.");

                    cache = new DeserializerBuilder()
                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                        .Build()
                        .Deserialize<UserDataCollection>(yaml);

                    Log.Debug("Data loaded successfully.");
                }
                else
                {
                    Log.Debug($"Data file {DataFile} does not exist. No data loaded.");
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"Error loading data:\n {ex.Message}");
            }
        }

        public UserDataCollection GetData() => cache;
    }
}