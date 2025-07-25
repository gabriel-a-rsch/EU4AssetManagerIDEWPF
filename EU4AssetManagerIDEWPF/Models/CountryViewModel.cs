using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4AssetManagerIDEWPF.Models
{
    internal class CountryViewModel
    {
        int id;
        public String TAG { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public String CapitalID { get; set; }
        public String CapitalName { get; set; }

        public List<String> AcceptedCultures { get; set; } //NOTE: Would it make sense to do a soft validation of Culture string contents? ike, check if it matches a known culture name from the base game or the mod?
        public String PrimaryCulture { get; set; }
        public String Religion { get; set; }

    }
}
