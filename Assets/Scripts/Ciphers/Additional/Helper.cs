namespace Assets.Scripts.Ciphers.Additional
{
    internal class Helper
    {
        public static string GenerateFullKey(string key, string message)
        {
            if(key.Length == 0 || message.Length == 0)
            {
                return "";
            }

            string fullKey = "";

            for (int i = 0; i < message.Length; i++)
                fullKey += key[i % key.Length];

            return fullKey;
        }

        public static string GenerateFullKey(string key, int len)
        {
            if (key.Length == 0 || len == 0)
            {
                return "";
            }

            string fullKey = "";

            for (int i = 0; i < len; i++)
                fullKey += key[i % key.Length];

            return fullKey;
        }

        public static int GetCharIndexInAlphabet(char ch, char firstChInAlphabet)
        {
            var charIndex = ch - (int)firstChInAlphabet;

            if ((int)ch == 1025)
                charIndex = 6;
            if ((int)ch > 1045)
                charIndex++;

            return charIndex;
        }
    }
}
