using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace Practika
{
    class CountryConverter :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Country> country = new List<Country>();
            HashSet<ProductCountry> pr = (HashSet<ProductCountry>)value;
            foreach (var item in pr)
            {
                country.Add(item.Country);
            }

            string countyString = "";
            foreach (Country item in country)
            {

                countyString += item.Name;
                countyString = countyString.Remove(countyString.Length - 2);
                if (country.Last() != item)
                {
                    countyString += ", ";
                }
            }

            return countyString;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
