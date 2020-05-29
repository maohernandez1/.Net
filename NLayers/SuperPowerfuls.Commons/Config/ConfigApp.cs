namespace SuperPowerfuls.Commons.Config
{
    using System.Configuration;
    using SuperPowerfuls.Commons.Log;
    public class ConfigApp
    {
        public static string GetFlagForVillian()
        {
            string flagVillian = string.Empty;
            try
            {
                ConfigurationManager.RefreshSection("appSettings");
                flagVillian = ConfigurationManager.AppSettings["flagVillian"];
            }
            catch(ConfigurationErrorsException confErrorException)
            {
                ErrorManager.RiseError(confErrorException.Message);
            }
            return flagVillian;
        }
    }
}
