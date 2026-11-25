public abstract class Cipher
{
    public virtual CipherType Type => 0;

    public string Message;

    public bool IsMock { get; private set; } = false;

    public virtual int ExpiriencePoints => 0;

    public string EncodedText { get => Encode(Message); }
    public string DecodedText { get => Decode(Message); }

    public Cipher(string message)
    {
        Message = message.ToUpper();
    }

    public abstract string Encode(string message);
    public abstract string Decode(string message);

    public void Mock()
    {
        IsMock = true;
    }
}
