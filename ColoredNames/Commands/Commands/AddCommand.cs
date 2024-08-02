using ColoredNames.Features;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace ColoredNames.Commands.Commands
{
    public class AddCommand : ICommand
    {
        public string Command { get; } = "add";
        public string[] Aliases { get; } = { string.Empty };
        public string Description { get; } = "Adds the/A user to the database";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("colorednames.add"))
            {
                response = OtherMethods.NoPermission("colorednames.add");
                return true;
            }

            if (arguments.Count == 0 || arguments.Count < 4)
            {
                response = "\n\n<color=red>Invalid usage</color>\n\n" +
                           "<size=26>.cn add smart/manual [userid] [color] [override badge]</size>\n\n" +
                           "smart = uses RA ID's\n" +
                           "manual = steam64ID\n" +
                           "userid = either RA ID or steam64ID (based on smart/manual)\n" +
                           "color = color name\n" +
                           "override badge = true or false value, if the color should override any server roles (such as staff)\n\n" +
                           "Example: .cn add manual 0000000000@steam red true";
                return true;
            }

            string userType = arguments.At(0);
            string userId = arguments.At(1);
            string color = arguments.At(2);
            bool overrideBadge;

            if (!OtherMethods.IsValidColor(color))
            {
                response = "Invalid color. Please use the 'color' command to see allowed colors.";
                return true;
            }

            if (!bool.TryParse(arguments.At(3), out overrideBadge))
            {
                response = "Invalid override badge value. It should be true or false.";
                return true;
            }

            switch (userType.ToLower())
            {
                case "smart":
                    Player player = Player.Get(userId);

                    if (player == null)
                    {
                        response = "Player not found";
                        return true;
                    }

                    if (DatabaseMethods.UserExists(player.UserId))
                    {
                        response = $"User with ID {player.UserId} already exists.";
                        return true;
                    }

                    DatabaseMethods.AddUser(player.UserId, color, overrideBadge);
                    response = $"{player.UserId} added to cache with color {color}";
                    break;

                case "manual":
                    if (DatabaseMethods.UserExists(userId))
                    {
                        response = $"User with ID {userId} already exists.";
                        return true;
                    }

                    DatabaseMethods.AddUser(userId, color, overrideBadge);
                    response = $"{userId} added to cache with color {color}";
                    break;

                default:
                    response = "<color=red>Invalid user type</color>. It should be either smart or manual.";
                    return true;
            }

            return true;
        }
    }
}