using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4DataValidator
{
    internal class ParsingToken
    {
        Guid id = Guid.NewGuid();
        public int lineNumber;
        public required string content;
        public bool isMultiLine => content.Contains("{") || content.Contains("}");
        //TODO: Add the scope property, it's based on what the path of the file is. For example, if the file is in common/countries, the scope is "common/countries". This is used to determine the context of the token.
    }
    internal class LexerParserEU4Language
    {
        public Dictionary<ParsingToken, ParsingToken> symbolValuePairs = new Dictionary<ParsingToken, ParsingToken>(); // token pairs that have the same line number are evaluated together. maybe i should use intermediary numbers for multi lines since multi lines are technically one compound statement
        public Dictionary<string, string> evaluatedValuePairs = new Dictionary<string, string>(); // this is used to store the evaluated values of the symbol-value pairs, for example, if a value is a reference to another symbol, it will be resolved here
        private Tuple<ParsingToken, ParsingToken> getResourcePairFromOneLiner(ParsingToken token)
        {
            string[] contentTokens = token.content.Split('=', 2);
            string left = contentTokens[0].Trim();
            string right = contentTokens.Length > 1 ? contentTokens[1].Trim() : string.Empty;
            right = right.Replace("\"", ""); // Remove quotes from both parts
            ParsingToken leftToken = new ParsingToken
            {
                lineNumber = token.lineNumber,
                content = left
            };
            ParsingToken rightToken = new ParsingToken
            {
                lineNumber = token.lineNumber,
                content = right
            };
            return Tuple.Create(leftToken, rightToken);
        }
        private void parseSingleLine(ParsingToken token) 
        {
            Tuple<ParsingToken,ParsingToken> resourcePair = getResourcePairFromOneLiner(token);
            symbolValuePairs.Add(resourcePair.Item1, resourcePair.Item2);
        }
        private void parseMultiLine(ParsingToken token)
        { 
            throw new NotImplementedException("Multi-line parsing is not implemented yet. This is a placeholder for future implementation.");
        }
        public void parseDocumentFromStr(string content) // There is theoretically a universal syntax for paradox stuff. This would take a long time to implement fully but I don't need to
        {
            string[] lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<ParsingToken> tokens = new List<ParsingToken>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
                {
                    // Skip comments and empty lines
                    continue;
                }
                ParsingToken token = new ParsingToken
                {
                    lineNumber = i + 1, // Line numbers are 1-based
                    content = line
                };
                tokens.Add(token);
            }
            // now we can consolidate the tokens that aren't singe-line definitions without losing the line numbers
            List<ParsingToken> oneLiners = tokens.Where(token => !token.content.Contains("{") && !token.content.Contains("}")).ToList(); // this is actually some really cool LINQ syntax
            // now we need to differentiate betweeen one liners and multi-line definitions
            List<ParsingToken> multiLineStatements = tokens.Except(oneLiners).ToList();
            // now we need to separate the multi line statements into their own sections
            List<ParsingToken> complextStatements = new List<ParsingToken>(); // list of concattenated multi-line statements tha make up a single definition
            // populate complextStatements with the consolidated multi-line statements
            {
                ParsingToken currentStatement = new ParsingToken() { content = "" };
                foreach (ParsingToken line in multiLineStatements)
                {
                    if (line.content.StartsWith("#") || string.IsNullOrWhiteSpace(line.content))
                    {
                        // Skip comments and empty lines
                        continue;
                    }
                    if (line.content.Contains("{"))
                    {
                        // This is the start of a multi-line statement
                        currentStatement.lineNumber = line.lineNumber; // Keep the line number of the first line of the statement
                        currentStatement.content = line.content.Trim();
                    }
                    else if (line.content.Contains("}"))
                    {
                        // This is the end of a multi-line statement
                        if (complextStatements.Count > 0)
                        {
                            currentStatement.content += "\n" + line.content.Trim();
                            complextStatements.Add(currentStatement);
                            currentStatement = new ParsingToken() { content = "" }; // Reset for the next statement
                        }
                    }
                    else
                    {
                        // This is a continuation of a multi-line statement
                        currentStatement.content += "\n" + line.content.Trim();
                    }
                }
            }
            // TODO: Now we need to parse the one-liners and multi-line statements separately
            // They have to be handled sequentially. Now we have line numbers and content for each token, we can parse them into a dictionary of symbol-value pairs.
            Stack<ParsingToken> lineOrderedStack = new Stack<ParsingToken>(tokens);
            foreach (ParsingToken token in oneLiners)
            {
                lineOrderedStack.Push(token);
            }
            foreach (ParsingToken token in complextStatements)
            {
                lineOrderedStack.Push(token);
            }
            lineOrderedStack = new Stack<ParsingToken>(lineOrderedStack.OrderBy(t => t.lineNumber)); // Sort the stack by line number
            foreach(ParsingToken token in lineOrderedStack)
            {
                if (token.isMultiLine)
                    parseMultiLine(token);
                else
                    parseSingleLine(token);
            }
        }
    }
}
