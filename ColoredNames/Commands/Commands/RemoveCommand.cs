using ColoredNames.Features;
using CommandSystem;
using System;
using Exiled.Permissions.Extensions;
using Exiled.API.Features;

namespace ColoredNames.Commands.Commands
{
    public class RemoveCommand : ICommand
    {
        public string Command { get; } = "remove";
        public string[] Aliases { get; } = { string.Empty };
        public string Description { get; } = "Removes a user from the database";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("colorednames.remove"))
            {
                response = OtherMethods.NoPermission("colorednames.remove");
                return true;
            }

            if (arguments.Count == 0)
            {
                response = "\n\n<color=red>Invalid usage</color>\n\n" +
                           "<size=26>.cn remove steam64ID</size>";
                return true;
            }

            if (!DatabaseMethods.UserExists(arguments.At(0)))
            {
                response = $"User does not exist in the database";
                return true;
            }

            DatabaseMethods.RemoveUser(arguments.At(0));
            response = $"User was removed from the database";
            return true;
        }
    }
}