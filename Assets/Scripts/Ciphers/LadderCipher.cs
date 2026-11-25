using System;

public class LadderCipher : Cipher
{
    public override CipherType Type => CipherType.LadderCipher;

    public override int ExpiriencePoints => 2;

    public LadderCipher(string message) : base(message) { }

    public override string Encode(string message)
    {
        if (IsMock)
            return message;

        string firstPart = "";
        string secondPart = "";

        for (int i = 0; i < message.Length; i++)
        {
            if (i % 2 == 0)
                firstPart += message[i];
            else
                secondPart += message[i];
        }    

        return firstPart + secondPart;
    }

    public override string Decode(string message)
    {
        if (IsMock)
            return message;

        string res = "";
        string firstPart;
        string secondPart;

        if (message.Length % 2 == 0)
        {
            firstPart = message.Substring(0, message.Length/2);
            secondPart = message.Substring(message.Length/2);
        }
        else
        {
            firstPart = message.Substring(0, message.Length / 2 + 1);
            secondPart = message.Substring(message.Length / 2 + 1);
        }

        for (int i = 0; i < firstPart.Length; i++)
        {
            res += firstPart[i];
            if (i >= secondPart.Length)
                break;
            res += secondPart[i];
        }

        return res;
    }
}
