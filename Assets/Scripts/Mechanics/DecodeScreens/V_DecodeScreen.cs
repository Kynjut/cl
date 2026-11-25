using Assets.Scripts;
using Assets.Scripts.Ciphers;
using Assets.Scripts.Ciphers.Additional;
using TMPro;
using UnityEngine;

public class V_DecodeScreen : DecodeScreen
{
    [SerializeField] private V_Visualizer viz;
    [SerializeField] private TextMeshProUGUI _keyText;

    private new InputKeysReader _inputReader => (InputKeysReader)base._inputReader;

    private new void Start()
    {
        base.Start();
    }

    public override void SetCipher(Cipher cipher)
    {
        base.SetCipher(cipher);
        _inputReader.SetTextLimit(RandomCipherGenerator.MaxWordLength);

        if (cipher.EncodedText.Length > 0)
            viz.Init(cipher.EncodedText[0], Localization.RU, V_Visualizer.Mode.Decode);

        InitText();
    }

    protected override bool IsWin()
    {
        if (_cipher.IsMock || !(_cipher is VigenereCipher vc))
            return false;

        string inputSeq = _inputReader.Text.ToUpper();
        string decodedMessage = new VigenereCipher(
            vc.EncodedText,
            Alphabet.RU,
            inputSeq
        ).DecodedText;

        return decodedMessage == _cipher.Message;
    }

    protected override void InitText()
    {
        base.InitText();

        _inputReader.SetAfterUpdateEvent(() =>
        {
            UpdateKeyText();
            UpdateUserInputText();
            UpdateEncodedText();
            UpdateVizualizer();

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

    private void UpdateVizualizer()
    {
        int len = _inputReader.Text.Length;
        if (len == 0 || _cipher.EncodedText.Length < len)
            return;

        viz.SetRowChar(_cipher.EncodedText[len - 1]);
        viz.SetColChar(_inputReader.Text[^1]);
    }

    private void UpdateEncodedText()
    {
        string key = _inputReader.Text.ToUpper();

        if (key.Length != 0)
            _encodedText.text =
                TextConverter.ToHighlightCorrectnessFormat(
                    new VigenereCipher(
                        _cipher.EncodedText,
                        Alphabet.RU,
                        key
                    ).DecodedText,
                    _cipher.Message,
                    _cipher.IsMock);
        else
            _encodedText.text =
                TextConverter.ToHighlightCorrectnessFormat(
                    _cipher.EncodedText,
                    _cipher.Message,
                    _cipher.IsMock);
    }

    private void UpdateKeyText()
    {
        string key = _inputReader.Text.ToUpper();

        _keyText.text = key.Length != 0
            ? Helper.GenerateFullKey(key, _cipher.EncodedText.Length)
            : "";
    }
}