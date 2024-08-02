using ColoredNames.Features;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;
using MEC;
using System.Linq;

namespace ColoredNames
{
    public class EventHandler
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            if (DatabaseMethods.UserExists(ev.Player.UserId) && ev.Player.CheckPermission("colorednames.access"))
            {
                Timing.CallDelayed(0.5f, () =>
                {
                    UserData cachedUser = Plugin.Singleton.DatabaseHandler.cache.Users.First(u => u.UserId == ev.Player.UserId);

                    if (ev.Player.RankName.IsEmpty() || cachedUser.OverrideBadge)
                    {
                        ev.Player.RankColor = cachedUser.Color;
                        ev.Player.RankName = " ";

                        Log.Debug($"Successfully gave {ev.Player.Nickname} a colored name.");
                    }
                    else if (!ev.Player.RankName.IsEmpty() && !cachedUser.OverrideBadge)
                        Log.Debug($"{ev.Player.UserId} already has a rank name and does not have OverrideBadge set to true, Colored name will not be given");
                });
            }
        }
    }
}