using ColoredNames.Features;
using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace ColoredNames.Commands.Commands
{
    public class CacheCommand : ICommand
    {
        public string Command { get; } = "cache";
        public string[] Aliases { get; } = { string.Empty };
        public string Description { get; } = "Re-cache the database, Useful if you manually modifed data.yml";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("colorednames.cache") && !sender.CheckPermission("colorednames.access"))
            {
                response = OtherMethods.NoPermission("colorednames.cache");
                return true;
            }


            DatabaseMethods.ReloadCache();
            response = "Cache was re-cached";
            return true; ;
        }
    }
}
