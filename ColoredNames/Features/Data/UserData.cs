namespace ColoredNames.Features
{
    public class UserData
    {
        public UserData() { }

        public UserData(string userId, string color, bool overrideBadge)
        {
            UserId = userId;
            Color = color;
            OverrideBadge = overrideBadge;
        }

        public string UserId { get; set; }
        public string Color { get; set; }
        public bool OverrideBadge { get; set; }
    }

}