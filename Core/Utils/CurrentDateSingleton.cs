using System;

namespace vega.Core.Utils
{
    public class CurrentDateSingleton
    {
        private static CurrentDateSingleton _instance;
        protected DateTime currentDate;
   
        CurrentDateSingleton() 
        { 
         
        }

        public static CurrentDateSingleton setDate(DateTime currentDate)
        {
                if(_instance == null) {
                    _instance = new CurrentDateSingleton();
                    _instance.currentDate = currentDate;
                }

                return _instance;
        }

        public DateTime getCurrentDate()
        {
            return _instance.currentDate;
        }
    }


}