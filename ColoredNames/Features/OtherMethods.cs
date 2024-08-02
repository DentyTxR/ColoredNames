
using System.Collections.Generic;

namespace ColoredNames.Features
{
    public class OtherMethods
    {
        public static List<string> validColors = new List<string>
        {
            "pink",
            "red",
            "brown",
            "silver",
            "light_green",
            "crimson",
            "cyan",
            "aqua",
            "deep_pink",
            "tomato",
            "yellow",
            "magenta",
            "blue_green",
            "orange",
            "lime",
            "green",
            "emerald",
            "carmine",
            "nickel",
            "mint",
            "army_green",
            "pumpkin"
        };

        public static string NoPermission(string c) => $"<color=white>You do not have permission required for this command,You need</color><color=red> {c}</color>";

        public static bool IsValidColor(string color)
        {
            if (validColors.Contains(color))
                return true;

            return false;
        }
    }
}
