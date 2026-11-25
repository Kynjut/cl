using System.Linq;
using System.Text.RegularExpressions;

namespace Assets.Scripts
{
    public class TextConverter
    {
        public static string ToUnderlinedFormat(string text)
        {
            string res = "";

            foreach (var sym in text)
                res += CharToUnderLineFormat(sym);

            return res;
        }

        public static string ToUnderlinedFormat(string text, string textMask)
        {
            int needLen = ToNoFormat(textMask).Length;

            if (text.Length > needLen)
                text = text.Substring(0, needLen);

            string res = ToUnderlinedFormat(text);

            while (ToNoFormat(res).Length < needLen)
            {
                if (res.Length > 0 && res[^1] == ' ')
                    res += $"<u>?</u>";
                else 
                    res += ' ';
            }

            return res;
        }

        public static string ToBlankFormat(string text)
        {
            return string.Join("", text.Select(x => SpaceToUnderLineFormat()));
        }

        public static string ToNoFormat(string text)
        {
            string res = text;
            res = RemoveUnderlineTags(res);

            return res.Replace(" ", "");
        }

        public static string ToHighlightCorrectnessFormat(string text, string correct, bool isMock)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var sb = new System.Text.StringBuilder(text.Length * 24);

            for (int i = 0; i < text.Length; i++)
            {
                bool hasExpected = correct != null && i < correct.Length;

                char actual = char.ToUpper(text[i]);
                char expected = hasExpected ? char.ToUpper(correct[i]) : '\0';

                bool isMatch = (hasExpected && actual == expected) && !isMock;

                string color = isMatch ? "green" : "red";
                sb
                    .Append("<u>")
                    .Append("<color=")
                    .Append(color)
                    .Append('>')
                    .Append(actual)
                    .Append("</color>")
                    .Append("</u> ");
            }

            return sb.ToString();
        }

        static string RemoveUnderlineTags(string input)
        {
            string pattern = @"<\/?u>";
            return Regex.Replace(input, pattern, "");
        }

        private static string CharToUnderLineFormat(char sym)
        {
            if (sym != ' ')
                return $"<u>{sym}</u> ";
            else
                return " ";
        }

        private static string ToUnderLineFormat(string sym)
        {
            if (sym != " ")
                return $"<u>{sym}</u> ";
            else
                return " ";
        }

        private static string SpaceToUnderLineFormat()
        {
            return $"<u>_</u> ";
        }
    }
}
