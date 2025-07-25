namespace Testing_EU4_Data_Validator
{
    [TestClass]
    public sealed class BasicFileHandlingTests
    {
        [TestMethod]
        public void TestFileTextMirroringInMemory()
        {
            // Arrange
            var dataValidator = new EU4DataValidator.DataValidator();
            string path = "TestData\\EU4\\common\\country_tags\\00_countries.txt";
            
            // Act
            // Load the EU4 data from the specified path
            dataValidator.loadEU4DataFromPath(path);
            // load the text file separately to compare to sotred text
            string loadedTestText = System.IO.File.ReadAllText(path);
            // Get the loaded text from memory
            string loadedMemoryText = dataValidator.loadedRawText[0];
            // Get the loaded paths from memory
            string loadedMemoryPaths = dataValidator.loadedPaths[0];
            // Note: In a real scenario, you would likely have more than one loaded path and text, so you might want to loop through them or check specific indices.
            // Assert
            // Assert that the loaded text matches the stored text
            Assert.AreEqual(loadedTestText, loadedMemoryText, "The loaded text from the file does not match the stored text in memory.");
            Assert.AreEqual(path, loadedMemoryPaths,"The loaded paths from the file does not match the stored paths in memory.");
        }
        [TestMethod]
        public void TestValidResourcePairs()
        {
            // Arrange
            var dataValidator = new EU4DataValidator.DataValidator();
            string path = "TestData\\EU4\\common\\country_tags\\00_countries.txt";
            // Act
            // Load the EU4 data from the specified path
            dataValidator.loadEU4DataFromPath(path);
            // Get the resource pairs from memory
            var resourcePairs = dataValidator.resourcePairsPaths;
            // Assert
            // Assert that the resource pairs contain expected values
            Assert.IsTrue(resourcePairs.ContainsKey("SWE"), "Resource pairs should contain 'SWE'.");
            Assert.IsTrue(resourcePairs.ContainsKey("ALB"), "Resource pairs should contain 'ALB'.");
            Assert.AreEqual(resourcePairs["SWE"],"countries/Sweden.txt", "The kay 'SWE' should have the value 'countries/Sweden.txt'");
            Assert.AreEqual(resourcePairs["ALB"], "countries/Albania.txt", "The kay 'ALB' should have the value 'countries/Albania.txt'");
        }
    }
}
