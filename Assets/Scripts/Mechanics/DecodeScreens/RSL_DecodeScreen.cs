using Assets.Scripts;

public class RSL_DecodeScreen : DecodeScreen
{
    private new InputKeysReader _inputReader => (InputKeysReader)base._inputReader;

    public override void SetCipher(Cipher cipher)
    {
        base.SetCipher(cipher);
        _inputReader.SetTextLimit(cipher.Message.Length);

        InitText();
    }

    protected override bool IsWin()
    {
        if (_cipher.IsMock && _cipher.Type == CipherType.SkitalaCipher)
            return false;

        return _inputReader.Text.ToUpper() == transform.parent.GetComponent<CipherContainer>().Cipher.Message;
    }

    protected override void InitText()
    {
        base.InitText();

        _inputReader.SetAfterUpdateEvent(() =>
        {
            UpdateUserInputText();

            CheckWin();
        });

        UpdateUserInputText();
    }

    protected override void UpdateUserInputText()
    {
        _userInputText.text = TextConverter.ToUnderlinedFormat(_inputReader.Text.ToUpper(), mask);
    }
}
