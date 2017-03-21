using System;

namespace Forum.Web.Helpers
{
    public static class CustomConvert
    {
        public static int ToInt32(object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        public static int ToInt32(string value)
        {
            return ToInt32((object)value);
        }
    }
}