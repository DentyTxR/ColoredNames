using System.Linq;

namespace ColoredNames.Features
{
    public class DatabaseMethods
    {
        public static void AddUser(string userId, string color, bool overrideBadge)
        {
            if (Plugin.Singleton.DatabaseHandler.cache.Users.Any(u => u.UserId == userId))
                return;

            Plugin.Singleton.DatabaseHandler.cache.Users.Add(new UserData(userId, color, overrideBadge));

            Plugin.Singleton.DatabaseHandler.SaveData();
        }

        public static void RemoveUser(string userId)
        {
            var userToRemove = Plugin.Singleton.DatabaseHandler.cache.Users.FirstOrDefault(u => u.UserId == userId);
            if (userToRemove != null)
            {
                Plugin.Singleton.DatabaseHandler.cache.Users.Remove(userToRemove);

                Plugin.Singleton.DatabaseHandler.SaveData();
            }
        }

        public static bool UserExists(string userId)
        {
            return Plugin.Singleton.DatabaseHandler.cache.Users.Any(user => user.UserId == userId);
        }

        public static void ReloadCache()
        {
            Plugin.Singleton.DatabaseHandler.cache.Users.Clear();
            Plugin.Singleton.DatabaseHandler.LoadData();
        }

        public static UserData GetUserFromCache(string userId)
        {
            return Plugin.Singleton.DatabaseHandler.cache.Users.FirstOrDefault(user => user.UserId == userId);
        }
    }
}
