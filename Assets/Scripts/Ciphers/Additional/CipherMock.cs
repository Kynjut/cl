using System.Linq;

namespace Assets.Scripts.Ciphers.Additional
{
    public class CipherMock
    {
        public static Cipher GetMock(Cipher cipher, CipherType toType)
        {
            Cipher newCipher = null;

            switch (toType)
            {
                case CipherType.ReverseCipher:
                    newCipher = new ReverseCipher(cipher.EncodedText);
                    break;
                case CipherType.LadderCipher:
                    newCipher = new LadderCipher(cipher.EncodedText);
                    break;
                case CipherType.SkitalaCipher:
                    newCipher = new SkitalaCipher(cipher.EncodedText, Alphabet.RU, 0);
                    break;
                case CipherType.CaesarCipher:
                    newCipher = new CaesarsСipher(cipher.EncodedText, Alphabet.RU, 0);
                    break;
                case CipherType.CaesarWithKeyCipher:
                    newCipher = new CaesarsWithKeyСipher(cipher.EncodedText, Alphabet.RU, "");
                    break;
                case CipherType.VigenereCipher:
                    newCipher = new VigenereCipher(
                        cipher.EncodedText,
                        Alphabet.RU,
                        "");
                    break;
            }

            newCipher.Mock();

            return newCipher;
        }
    }
}
