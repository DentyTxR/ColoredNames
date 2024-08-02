using ColoredNames.Features;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace ColoredNames.Commands.Commands
{
    public class ShowDataCommand : ICommand
    {
        public string Command { get; } = "showdata";
        public string[] Aliases { get; } = { "sd" };
        public string Description { get; } = "Shows all data from data.yml in a clean format";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("colorednames.showdata") && !sender.CheckPermission("colorednames.access"))
            {
                response = OtherMethods.NoPermission("colorednames.showdata");
                return true;
            }

            response = $@"\nData in Data.yml\nLocation: {Paths.Configs}\ColoredNames\data.yml" + "\n\n";

            foreach (var data in DatabaseMethods.GetData().Users)
            {
                response += $"<size=19>ID: {data.UserId}</size>\n" +
                    $"<size=19>Color: {data.Color}</size>\n" +
                    $"<size=19>Override Badge: {data.OverrideBadge}</size>";
            }

            return true;
        }
    }
}