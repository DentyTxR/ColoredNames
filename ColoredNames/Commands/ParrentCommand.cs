using CommandSystem;
using System;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using ColoredNames.Commands.Commands;

namespace ColoredNames.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]

    //Some of this code comes from Michal78900 (I dont remember which repo, but i wanna give credit since I got it from him)
    public class ParrentCommand : ParentCommand
    {
        public ParrentCommand() => LoadGeneratedCommands();

        public override string Command => "colorednames";

        public override string[] Aliases { get; } = { "cn", "colorednames" };

        public override string Description => "Help command for colorednames";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new AddSelfCommand());
            RegisterCommand(new AddCommand());
            RegisterCommand(new ChangeColor());
            RegisterCommand(new RemoveCommand());
            RegisterCommand(new CacheCommand());
            RegisterCommand(new ShowDataCommand());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "\nBelow is all available commands you can access\n\n";

            foreach (var command in AllCommands)
            {
                if (((CommandSender)sender).CheckPermission($"colorednames.{command.Command}"))
                {
                    response += $"Command Name: {command.Command}\nAliases: {string.Join(", ", command.Aliases)}\nCommand Description: {command.Description}\n\n";
                }
            }
            return true;
        }
    }
}