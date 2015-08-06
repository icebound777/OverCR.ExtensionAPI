namespace OverCR.ExtensionSystem.API.Game.Player
{
    public static class Profile
    {
        public static string Name
        {
            get { return G.Sys.ProfileManager_.CurrentProfile_.Name_; }
        }

        public static string ActiveVehicleName
        {
            get { return G.Sys.ProfileManager_.CurrentProfile_.CarName_; }
        }

        public static string PrimaryColor
        {
            get { return G.Sys.ProfileManager_.CurrentProfile_.Primary_.ToHexStringRGB(); }
        }

        public static string SecondaryColor
        {
            get { return G.Sys.ProfileManager_.CurrentProfile_.Secondary_.ToHexStringRGB(); }
        }

        public static string GlowColor
        {
            get { return G.Sys.ProfileManager_.CurrentProfile_.Glow_.ToHexStringRGB(); }
        }

        public static string SparkleColor
        {
            get { return G.Sys.ProfileManager_.CurrentProfile_.Sparkle_.ToHexStringRGB(); }
        }

        public static float SteeringSensitivity
        {
            get { return G.Sys.ProfileManager_.CurrentProfile_.TurningSensitivity_; }
            set { G.Sys.ProfileManager_.CurrentProfile_.TurningSensitivity_ = value; }
        }

        public static float FlightSensitivity
        { 
            get { return G.Sys.ProfileManager_.CurrentProfile_.FlightSensitivity_; }
            set { G.Sys.ProfileManager_.CurrentProfile_.FlightSensitivity_ = value; }
        }
    }
}
