using System;
using Exiled.API.Features;
using ColoredNames.Features;
using PlayerHandler = Exiled.Events.Handlers.Player;

namespace ColoredNames
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton;
        private EventHandler EventHandler;
        public DatabaseHandler DatabaseHandler;

        public override string Name { get; } = "ColoredNames";
        public override string Author { get; } = "Denty";
        public override string Prefix { get; } = "colored_names";
        public override Version Version { get; } = new Version(1, 2, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 0, 0);

        public override void OnEnabled()
        {
            Singleton = this;
            DatabaseHandler = new DatabaseHandler();
            EventHandler = new EventHandler();

            PlayerHandler.Verified += EventHandler.OnVerified;

            base.OnEnabled();
        }


        public override void OnDisabled()
        {
            DatabaseHandler = null;
            EventHandler = null;
            Singleton = null;

            PlayerHandler.Verified -= EventHandler.OnVerified;

            base.OnDisabled();
        }
    }
}