using Exiled.API.Features;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

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
                File.WriteAllText(fullPath, "# Below is a example if you are manually writing (its also the plugin dev)");

                //cache.Users.Add(new UserData("76561198972907216@steam", "red", true)); old way
                DatabaseMethods.AddUser("76561198972907216@steam", "red", true);
                SaveData();

            }
        }

        public void SaveData()
        {
            string yaml = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build()
                .Serialize(cache);

            File.WriteAllText(Path.Combine(DataPath, DataFile), yaml);

            foreach (Player player in Player.List.Where(p => cache.Users.Any(u => u.UserId == p.UserId)))
            {
                UserData cachedUser = cache.Users.First(u => u.UserId == player.UserId);

                if (player.RankName.IsEmpty() || cachedUser.OverrideBadge)
                {
                    player.RankColor = cachedUser.Color;
                    player.RankName = " ";
                }
            }
        }

        public void LoadData()
        {
            string fullPath = Path.Combine(DataPath, DataFile);
            if (File.Exists(fullPath))
            {
                string yaml = File.ReadAllText(fullPath);
                
                cache = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build()
                    .Deserialize<UserDataCollection>(yaml);
            }
        }

        public UserDataCollection GetData() => cache;
    }
}