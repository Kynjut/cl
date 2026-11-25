using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;

public class DecodeScreen : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _encodedText;
    [SerializeField] protected TextMeshProUGUI _userInputText;

    public Cipher Cipher => _cipher;

    protected InputReader _inputReader;

    protected Cipher _cipher;
    protected string mask;

    protected void Start()
    {
        InitReader();
    }

    public void CheckWin()
    {
        if (IsWin())
            Win();
    }

    public virtual void SetCipher(Cipher cipher)
    {
        if (!_inputReader)
        {
            InitReader();
        }

        if (cipher is CaesarsСipher caesar)
            _cipher = caesar;
        else if (cipher is CaesarsWithKeyСipher caesarWithKey)
            _cipher = caesarWithKey;
        else if (cipher is VigenereCipher vigenere)
            _cipher = vigenere;
        else
            _cipher = cipher;

        print(_cipher.Message);

        LogCipherKey();
    }

    private void LogCipherKey()
    {
        switch (_cipher)
        {
            case VigenereCipher vc:
                print(vc.Key);
                break;
            case CaesarsWithKeyСipher cc:
                print(cc.Key);
                break;
        }
    }

    protected virtual void InitReader() 
    {
        _inputReader = GetComponentInChildren<InputReader>();
    }

    protected virtual bool IsWin() { throw new NotImplementedException(); }

    protected void Win()
    {
        GetComponent<DecodeScreenActions>().Win(500);
    }

    protected virtual void InitText()
    {
        _encodedText.text = TextConverter.ToUnderlinedFormat(_cipher.EncodedText);
        mask = _cipher.Message;
    }

    protected virtual void UpdateUserInputText() { }
}

