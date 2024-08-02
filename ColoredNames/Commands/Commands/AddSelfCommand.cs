﻿using ColoredNames.Features;
using CommandSystem;
using System;
using Exiled.Permissions.Extensions;
using Exiled.API.Features;

namespace ColoredNames.Commands.Commands
{
    public class AddSelfCommand : ICommand
    {
        public string Command { get; } = "addself";
        public string[] Aliases { get; } = { string.Empty };
        public string Description { get; } = "Adds self to database and gives colored name";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("colorednames.addself"))
            {
                response = OtherMethods.NoPermission("colorednames.addself");
                return true;
            }

            if (arguments.Count == 0)
            {
                response = "\n\n<color=red>Invalid usage</color>\n\n" +
                           "<size=26>.cn addself [color]</size>";
                return true;
            }

            string color = arguments.At(0);

            if (!OtherMethods.IsValidColor(color))
            {
                response = "Invalid color. Please use the 'color' command to see allowed colors.";
                return true;
            }

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Player not found";
                return true;
            }

            if (DatabaseMethods.UserExists(player.UserId))
            {
                response = $"You already exist.";
                return true;
            }

            DatabaseMethods.AddUser(player.UserId, color, false);
            response = $"{player.UserId} added to cache with color {color}";
            return true;
        }
    }
}