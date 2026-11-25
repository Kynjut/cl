using System;
using System.Linq;

public class ReverseCipher : Cipher
{
    public override CipherType Type => CipherType.ReverseCipher;

    public override int ExpiriencePoints => 1;

    public ReverseCipher(string message) : base(message) { }

    public override string Decode(string message)
    {
        if (IsMock)
            return message;

        return Reverse(message);
    }
    public override string Encode(string message)
    {
        if (IsMock)
            return message;

        return Reverse(message);
    }
    public string Reverse(string message) => new string(message.Reverse().ToArray());
}
