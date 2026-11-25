using Assets.Scripts;
using Assets.Scripts.Ciphers.Additional;

public class CaesarsWithKeyСipher : CaesarsСipher
{
    public override CipherType Type => CipherType.CaesarWithKeyCipher;

    public override int ExpiriencePoints => 5;

    public readonly string Key;

    public CaesarsWithKeyСipher(string message, Alphabet alphabet, string key) : base(message, alphabet) 
    { 
        Key = key.ToUpper();
    }

    public override string Encode(string message)
    {
        if (IsMock)
            return message;

        string res = "";

        if (Key.Length == 0)
            return TextConverter.ToBlankFormat(message);

        string fullKey = Helper.GenerateFullKey(Key, message);

        for (int i = 0; i < message.Length; i++)
            res += EncodeSym(message[i], Helper.GetCharIndexInAlphabet(fullKey[i], AlphabetChars[0]));

        return res;
    }

    public override string Decode(string message)
    {
        if (IsMock)
            return message;

        if (Key.Length == 0)
            return message;

        string res = "";
        string fullKey = Helper.GenerateFullKey(Key, message);

        for (int i = 0; i < message.Length; i++)
           res += EncodeSym(message[i], -Helper.GetCharIndexInAlphabet(fullKey[i], AlphabetChars[0]));

        return res;
    }
}
