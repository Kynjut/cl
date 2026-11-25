using System.Collections;
using UnityEngine;

public class C_DecodeScreen : DecodeScreen
{
    private new InputArrowsReader _inputReader => (InputArrowsReader)base._inputReader;

    public override void SetCipher(Cipher cipher)
    {
        base.SetCipher(cipher);

        InitText();
    }

    protected override bool IsWin()
    {
        if (_cipher.IsMock)
            return false;

        return _cipher is CaesarsСipher c && _inputReader.Shift == c.Shift;
    }

    protected override void InitText()
    {
        base.InitText();

        _inputReader.SetAfterUpdateEvent(() =>
        {
            UpdateUserInputText();
            UpdateEncodedText();

            CheckWin();
        });

        UpdateUserInputText();
        UpdateEncodedText();
    }

    protected IEnumerator AfterUpdateHandler()
    {
        UpdateUserInputText();
        UpdateEncodedText();

        yield return new WaitForSeconds(500);

        if (IsWin())
        {
            Win();
        }
    }

    protected override void UpdateUserInputText()
    {
        _userInputText.text = _inputReader.Shift.ToString();
    }

    private void UpdateEncodedText()
    {
        _encodedText.text = new CaesarsСipher(_cipher.EncodedText, Alphabet.RU, _inputReader.Shift).Decode(_cipher.EncodedText);
    }
}
