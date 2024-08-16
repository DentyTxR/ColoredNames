using ColoredNames.Features;
using ColoredNames.Features.Components;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Linq;
using UnityEngine;

namespace ColoredNames.Commands.Commands
{
    public class ChangeColor : ICommand
    {
        public string Command { get; } = "changecolor";
        public string[] Aliases { get; } = { "color", "cc" };
        public string Description { get; } = "Changes your colored names color";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("colorednames.changecolor") && !sender.CheckPermission("colorednames.access"))
            {
                response = OtherMethods.NoPermission("colorednames.changecolor");
                return true;
            }

            if (arguments.Count == 0)
            {
                response = "\n\n<color=red>Invalid usage</color>\n\n" +
                           "<size=26>.cn changecolor [color]</size>";
                return true;
            }

            Player player = Player.Get(sender);

            string color = arguments.At(0);

            if (!OtherMethods.IsValidColor(color))
            {
                response = "Invalid color. Please use the 'color' command to see allowed colors.";
                return true;
            }

            if (!DatabaseMethods.UserExists(player.UserId))
            {
                response = $"You are not in the database, you cannot change your color for a person that doesnt exist.";
                return true;
            }

            DatabaseMethods.ChangeColor(player.UserId, color);

            UserData user = Plugin.Singleton.DatabaseHandler.cache.Users.First(u => u.UserId == player.UserId);

            if (player.RankName.IsEmpty() || player.RankName == " " || user.OverrideBadge)
            {
                if (player.GameObject.TryGetComponent(out RainbowBadgeComponent component))
                    UnityEngine.Object.Destroy(component);

                player.RankColor = color;
                player.RankName = " ";
            }

            response = $"updated your color to {color}";
            return true;
        }
    }
}