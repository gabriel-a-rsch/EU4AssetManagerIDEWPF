using System.Security.AccessControl;

namespace EU4DataValidator
{
    public enum EU4DataFileType
    {
        CountryTags,
        Countries,
        Provinces,
        States,
        Regions,
        Religions,
        Cultures,
        Ideas,
        Technologies,
        Events,
        Decisions,
        Missions,
        Policies,
        Scripts,
        NONE
    }
    public class DataValidator
    { // This class is responsible for validating EU4 data files, loading them, and parsing them into a usable format. There should theoretically only be one instance of this class, as it is a singleton.
        // However, for testing purposes it should be allowed to have multiple instances for unit testing and debugging.
        public Guid id = Guid.NewGuid(); // This is just a unique identifier for the instance of the DataValidator, useful for debugging and logging purposes.
        public List<string> loadedRawText = new List<string>(); // this is mostly useful for debugging purposes, to see what the raw text looks like before parsing
        public List<string> validTags = new List<string>(); // this would work better as a HashSet<string> for faster lookups
        public List<string> validCultures = new List<string>(); // same as above, HashSet<string> would be better
        public List<string> loadedPaths = new List<string>(); // this is useful for debugging purposes, to see what files have been loaded
        public Dictionary<string,string> resourcePairsPaths = new Dictionary<string, string>(); // this is useful for debugging purposes, to see what resource pairs have been loaded and from which paths

        private Tuple<string, string> getResourcePairFromLine(string line) {
            string[] tokens = line.Split('=',2);
            string left = tokens[0].Trim();
            string right = tokens.Length > 1 ? tokens[1].Trim() : string.Empty;
            right = right.Replace("\"",""); // Remove quotes from both parts
            return new Tuple<string, string>(left, right);
        }
        private bool loadCountryTags(string content)
        {
            List<Tuple<string, string>> tupleResourceList = new List<Tuple<string, string>>();
            content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(line =>
                {
                    if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
                    {
                        // Skip comments and empty lines
                        return;
                    }
                    var resourcePair = getResourcePairFromLine(line);
                    tupleResourceList.Add(resourcePair);
                });
            resourcePairsPaths = tupleResourceList.ToDictionary(
                pair => pair.Item1, // Key is the left part of the pair
                pair => pair.Item2  // Value is the right part of the pair
            );
            return true;
        }
        private bool parseEU4DataFile(string content, string path)
        {
            // This is a placeholder for the actual parsing logic.
            // You would implement the logic to parse the EU4 data file here.
            // For now, let's assume it always returns true for simplicity.

            // This would likey be implemented using a proper parser for the EU4 data format, and using a giant jump table based on file type/formatting.
            // A really dumb way to do this would be to just parse line by line. Skip any multi-line statements, and just look for key-value pairs.
            // If statements are really ugly but fine, I should fiugre a way to swtich on a string.contains()
            EU4DataFileType fileType = EU4DataFileType.NONE; // This would be determined based on the file path or content
            bool loadStatus = false; // This would be set based on whether the parsing was successful or not
            loadedPaths.Add(path);
            loadedRawText.Add(content);
            if (path.Contains("common\\country_tags"))
                fileType = EU4DataFileType.CountryTags;

            switch (fileType)
            {
                case EU4DataFileType.CountryTags:
                    loadStatus = loadCountryTags(content);
                    break;
            }
            if (fileType == EU4DataFileType.NONE)
            {
                // If we don't know the file type, we can't parse it.
                return false;
            }
            return loadStatus;
        }
        private bool loadEU4DataFromStr(string content, string path)
        {
            loadedRawText.Append(content);
            bool parseSuccess = false;
            parseSuccess = parseEU4DataFile(content, path);
            return parseSuccess;
        }
        public bool loadEU4DataFromPath(string filePath)
        {
            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }
            string fileContents = System.IO.File.ReadAllText(filePath);
            bool resultOfStrLoad = loadEU4DataFromStr(fileContents, filePath);
            return resultOfStrLoad;
        }
    }
}
