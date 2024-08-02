using ColoredNames.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using System.Linq;

namespace ColoredNames
{
    public class EventHandler
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            if (DatabaseMethods.UserExists(ev.Player.UserId))
            {
                Timing.CallDelayed(0.5f, () =>
                {
                    UserData cachedUser = Plugin.Singleton.DatabaseHandler.cache.Users.First(u => u.UserId == ev.Player.UserId);

                    if (ev.Player.RankName.IsEmpty() || cachedUser.OverrideBadge)
                    {
                        ev.Player.RankColor = cachedUser.Color;
                        ev.Player.RankName = " ";
                    }
                });
            }
        }
    }
}
