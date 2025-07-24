using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Commons
{
    public class LocalizableEntity
    {
        public string NameAr {  get; set; }
        public string NameEn { get; set; }

        public string GetLocalize()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return NameAr;
            }
            else
            {
                return NameEn;
            }
        }


    }
}
