using System;

namespace WebStoreProject.Services
{
    public class CheckTime : ICheckTime
    {
        public bool DateValidation(DateTime register)
        {
            int regYear = register.Year;
            int currentYear = DateTime.Now.Year;
            return currentYear - regYear > 15;
        }

        public string GetMessage()
        {
            var timeNow = DateTime.Now;
            if (timeNow.Hour >= 13 && timeNow.Hour <= 20)
            {
                return "Good Evening";

            }
            else if (timeNow.Hour >= 6 && timeNow.Hour <= 12)
            {
                return "Good Morning";

            }
            else 
            {
                return "Good Night";

            }
           
        }
    }
}
