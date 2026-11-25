using Assets.Scripts;
using Assets.Scripts.Ciphers.Additional;
using TMPro;
using UnityEngine;

public class CWK_DecodeScreen : DecodeScreen
{
    [SerializeField] private TextMeshProUGUI _keyText;
    private new InputKeysReader _inputReader => (InputKeysReader)base._inputReader;

    public override void SetCipher(Cipher cipher)
    {
        base.SetCipher(cipher);
        _inputReader.SetTextLimit(RandomCipherGenerator.MaxWordLength);

        InitText();
    }

    protected override bool IsWin()
    {
        if (_cipher.IsMock)
            return false;

        string inputSeq = _inputReader.Text.ToUpper();
        string decodedMessage = new CaesarsWithKeyСipher(
                        _cipher.EncodedText,
                        Alphabet.RU,
                        _userInputText.text.ToUpper()
                    ).DecodedText;

        return _cipher is CaesarsWithKeyСipher c && inputSeq == c.Key || decodedMessage == _cipher.Message;
    }

    protected override void InitText()
    {
        base.InitText();

        _inputReader.SetAfterUpdateEvent(() =>
        {
            UpdateKeyText();
            UpdateUserInputText();
            UpdateEncodedText();

            CheckWin();
        });

        UpdateKeyText();
        UpdateUserInputText();
        UpdateEncodedText();
    }

    protected override void UpdateUserInputText()
    {
        _userInputText.text = _inputReader.Text.ToUpper();
    }

    private void UpdateEncodedText()
    {
        if (_userInputText.text.Length != 0)
            _encodedText.text =
                TextConverter.ToHighlightCorrectnessFormat(
                    new CaesarsWithKeyСipher(
                        _cipher.EncodedText,
                        Alphabet.RU,
                        _userInputText.text.ToUpper()
                    ).DecodedText,
                _cipher.Message,
                _cipher.IsMock);
        else
            _encodedText.text = TextConverter.ToHighlightCorrectnessFormat(_cipher.EncodedText, _cipher.Message, _cipher.IsMock);
    }

    private void UpdateKeyText()
    {
        string key = _inputReader.Text.ToUpper();

        if (key.Length != 0)
            _keyText.text = Helper.GenerateFullKey(key, RandomCipherGenerator.MaxWordLength);
        else 
            _keyText.text = "";
    }
}
