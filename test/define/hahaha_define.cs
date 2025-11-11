using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Polly;


namespace asp_base
{
    public static partial class ha
    {
        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------
        public static WebApplication? App = null;

        public static ILogger? Log_Program = null;
        public static hahaha_setting_box? Setting = null;

        public static hahaha_config? Config_ = null;
    }

    // ---------------------------------------------------------------
    //
    // ---------------------------------------------------------------

    public static partial class app
    {
        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------
        public static string Name_Application = "base";
        public static string Name_Title = "°òÂ¦¬[ºc";
    }

    // ---------------------------------------------------------------
    //
    // ---------------------------------------------------------------

    public static partial class setting
    {
        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------

    }

    // ---------------------------------------------------------------
    //
    // ---------------------------------------------------------------

    public static partial class layout
    {
        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------

    }



    public static partial class hahaha
    {
        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------
        public static WebApplication? App_ = null;


        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------
        public static hahahalib.hahaha_log Log_ = new hahahalib.hahaha_log();
        public static hahahalib.hahaha_json Json_ = new hahahalib.hahaha_json();


        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------
        public static hahaha_setting_box? Setting_Box_ = null;
        public static hahaha_config? Config_ = null;
        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------
        public static ILogger? Log_Program_ = null;
        // ---------------------------------------------------------------
        //
        // ---------------------------------------------------------------
        public static readonly IMemoryCache Cache_Memory_ = new MemoryCache(new MemoryCacheOptions());

        public static Policy circuitBreaker = null;


        // ---------------------------------------------------------------
        // ¥D­n
        // ---------------------------------------------------------------
        public static int Initial_Environment()
        {
            Setting_Box_ = new hahaha_setting_box();

            if (Setting_Box_.Load_All() != 0)
            {
                Setting_Box_.Save_All();
            }

            ha.Setting = Setting_Box_;

            Log_.Create($"{ha.Setting.System.Dir_Environment}/{ha.Setting.System.Name_Config}/nlog.config");

            Log_Program_ = Log_.Create_Log<Program>();
            ha.Log_Program = Log_Program_;

            

            return 0;

        }


        


        public static int Initial()
        {
            Config_ = new hahaha_config();
            Config_.Initial();

            ha.Config_ = Config_;
            return 0;
        }

        public static int Initial_UI()
        {
            return 0;
        }

        public static int Close()
        {
            hahaha.Log_.Close();

            return 0;
        }

        

    }    

}