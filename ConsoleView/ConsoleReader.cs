using System;
using System.Text.RegularExpressions;

namespace ConsoleView
{
    public class ConsoleReader
    {
        private readonly string _prompt;
        private readonly string _default;
        private readonly Regex _allowable;

        public ConsoleReader(string prompt, string def = null, Regex allowable = null)
        {
            _prompt = prompt;
            _default = def ?? string.Empty;
            if (def != null)
            {
                _default = def;
                _prompt += " [" + _default + "]";
            }
            _prompt += ": ";
            _allowable = allowable;
        }

        public static string GetString(string prompt, string def = null, Regex allowable = null)
        {
            ConsoleReader reader = new ConsoleReader(prompt, def, allowable);
            return reader.GetString();
        }

        public string GetString()
        {
            string answer;
            do
            {
                Console.Write(_prompt);
                answer = Console.ReadLine();
                if (string.IsNullOrEmpty(answer))
                    answer = _default;

            } while (IsAcceptable(answer) == false);

            return answer;
        }

        private bool IsAcceptable(string answer)
        {
            if (_allowable == null) return true;

            MatchCollection match = _allowable.Matches(answer);
            return match.Count == 1;
        }
    }
}
