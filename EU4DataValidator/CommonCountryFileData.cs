using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4DataValidator
{
    internal class CommonCountryFileData
    {
        Guid id = Guid.NewGuid();
        public string TAG { get; set; } // should be a 3-letter code, but we can use a string for flexibility. can be taken from the filename
        public string Name { get; set; } // can be taken from the filename
        public string graphical_culture { get; set; } // should I maybe provide a wrapper for this to an enum definition? Or at least provide a helper function.
        public (int,int,int) color { get; set; } // RGB color values, should be a tuple of three integers, I should technically do bounds checking here, the range is 0-255 for each value
        public (int,int,int) revolutionary_color { get; set; } // RGB color values, should be a tuple of three integers, I should technically do bounds checking here, the range is 0-255 for each value

        public string[] leader_names { get; set; } // this size should be fixed, it's set at runtime based on the number of lines containing leader names
        public string[] ship_names { get; set; } // this size should be fixed, it's set at runtime based on the number of lines containing ship names
        public List<string> historical_idea_groups { get; set; } // should theoretically always be of fixed size, but we can use a list if needed
        public List<string>[] historical_units { get; set; } // should theoretically always be of fixed size, but we can use a list if needed
        public Dictionary<string, int> monarch_names { get; set; } // Monarch names with their weights(?)
    }
}
