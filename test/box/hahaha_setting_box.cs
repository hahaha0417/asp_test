namespace asp_base
{
    public class hahaha_setting_box
    {
        // -------------------------------------------------
        // system
        // -------------------------------------------------
        public hahaha_setting_system System = new hahaha_setting_system();
        // -------------------------------------------------
        // global
        // -------------------------------------------------
        public hahaha_setting_global Global = new hahaha_setting_global();
        // -------------------------------------------------
        //
        // -------------------------------------------------
        public hahaha_setting_box()
        {
        }

        public int Load_All()
        {
            bool result = true;
            // -------------------------------------------------
            // system
            // -------------------------------------------------
            result &= Load_System() == 0;
            // -------------------------------------------------
            // global
            // -------------------------------------------------
            result &= Load_Global() == 0;
            // -------------------------------------------------
            // 
            // -------------------------------------------------

            return result ? 0 : -1;

        }

        public int Save_All()
        {
            // -------------------------------------------------
            // system
            // -------------------------------------------------
            Save_System();
            // -------------------------------------------------
            // global
            // -------------------------------------------------
            Save_Global();
            // -------------------------------------------------
            // 
            // -------------------------------------------------

            return 0;

        }

        public int Load_System()
        {
            int result = hahaha.Json_.Load($"{System.Dir_Environment}/{System.Name_Setting}/{System.Name_Class}/{System.Name_File}", ref System);

            return result;

        }

        public int Save_System()
        {
            hahaha.Json_.Save($"{System.Dir_Environment}/{System.Name_Setting}/{System.Name_Class}/{System.Name_File}", System);

            return 0;

        }

        public int Load_Global()
        {
            int result = hahaha.Json_.Load($"{System.Dir_Environment}/{System.Name_Setting}/{Global.Name_Class}/{Global.Name_File}", ref Global);

            return result;

        }

        public int Save_Global()
        {
            hahaha.Json_.Save($"{System.Dir_Environment}/{System.Name_Setting}/{Global.Name_Class}/{Global.Name_File}", Global);

            return 0;

        }
    }
}