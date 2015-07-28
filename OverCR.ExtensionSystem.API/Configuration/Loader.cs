namespace OverCR.ExtensionSystem.API.Configuration
{
    public static class Loader
    {
        public static Settings RetrieveSettings(object caller)
        {
            if(Settings.Exist(caller))
                return new Settings(caller);

            return null;
        }
    }
}
