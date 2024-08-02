using Exiled.API.Features;
using System.Linq;

namespace ColoredNames.Features
{
    public class DatabaseMethods
    {
        public static void AddUser(string userId, string color, bool overrideBadge)
        {
            if (Plugin.Singleton.DatabaseHandler.cache == null)
            {
                Log.Debug("Cache is not initialized.");
                return;
            }

            if (Plugin.Singleton.DatabaseHandler.cache.Users.Any(u => u.UserId == userId))
                return;

            Plugin.Singleton.DatabaseHandler.cache.Users.Add(new UserData(userId, color, overrideBadge));
            Log.Debug($"{userId} was saved to cache with color {color}, override badge: {overrideBadge}");

            Plugin.Singleton.DatabaseHandler.SaveData();
            Log.Debug("Cached data has overridden data.yml due to added user (it just added a user, if you made direct changes to data.yml you should run the cache command so its up to date)");
        }

        public static void RemoveUser(string userId)
        {
            var userToRemove = Plugin.Singleton.DatabaseHandler.cache.Users.FirstOrDefault(u => u.UserId == userId);
            if (userToRemove != null)
            {
                Plugin.Singleton.DatabaseHandler.cache.Users.Remove(userToRemove);
                Log.Debug($"{userId} was removed from data.yml");

                Plugin.Singleton.DatabaseHandler.SaveData();
                Log.Debug("Cached data has overridden data.yml due to removed user (it just removed the removed user, if you made direct changes to data.yml you should run the cache command so its up to date)");
            }
        }

        public static bool UserExists(string userId)
        {
            return Plugin.Singleton.DatabaseHandler.cache.Users.Any(user => user.UserId == userId);
        }

        public static void ChangeColor(string userid, string color)
        {
            //no need to check if color is valid, this method is only used in changecolor command which has its own check
            if (!UserExists(userid))
            {
                Log.Debug($"User {userid} does not exist in the database, cannot change color");
                return;
            }

            UserData user = Plugin.Singleton.DatabaseHandler.cache.Users.First(u => u.UserId == userid);
            user.Color = color;

            Log.Debug($"Color for user {userid} changed to {color}");

            Plugin.Singleton.DatabaseHandler.SaveData();
        }
        public static void ReloadCache()
        {
            Plugin.Singleton.DatabaseHandler.cache.Users.Clear();
            Plugin.Singleton.DatabaseHandler.LoadData();

            Log.Debug("Cache has been re-cached");
        }

        public static UserData GetUserFromCache(string userId)
        {
            return Plugin.Singleton.DatabaseHandler.cache.Users.FirstOrDefault(user => user.UserId == userId);
        }

        public static UserDataCollection GetData() => Plugin.Singleton.DatabaseHandler.cache;
    }
}
