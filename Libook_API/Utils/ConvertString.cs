using System.Text.RegularExpressions;

namespace Libook_API.Utils
{
    public class ConvertString
    {
        public static List<Guid> ExtractBookIds(string value)
        {
            var result = new List<Guid>();
            var regex = new Regex(@"BookID:\s*([a-fA-F0-9\-]+)", RegexOptions.Compiled);
            var matches = regex.Matches(value);

            foreach (Match match in matches)
            {
                if (Guid.TryParse(match.Groups[1].Value, out var bookId) && !result.Contains(bookId) && result.Count < 8) // Ensure no duplicates and max 8
                {
                    result.Add(bookId);
                }
            }

            return result;
        }
    }
}
