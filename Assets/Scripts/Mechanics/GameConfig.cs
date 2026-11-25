using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public bool IsValid { get; private set; }

    public List<CipherType> PossibleTypes
    {
        get
        {
            List<CipherType> types = new List<CipherType>();

            if (isReverseCipherEnabled)
                types.Add(CipherType.ReverseCipher);

            if (isSkitalaCipherEnabled)
                types.Add(CipherType.SkitalaCipher);

            if (isLadderCipherEnabled)
                types.Add(CipherType.LadderCipher);

            if (isCaesarsWithKeyCipherEnabled)
                types.Add(CipherType.CaesarWithKeyCipher);

            if (isCaesarsCipherEnabled)
                types.Add(CipherType.CaesarCipher);

            if (isVigenereCipherEnabled)
                types.Add(CipherType.VigenereCipher);

            return types;
        }
    }

    public bool
        isReverseCipherEnabled,
        isSkitalaCipherEnabled,
        isLadderCipherEnabled,
        isCaesarsCipherEnabled,
        isCaesarsWithKeyCipherEnabled,
        isVigenereCipherEnabled;

    public bool
        isHiddenSourceScreen;

    [SerializeField] private TextMeshProUGUI errorText;

    private ProgressBar progress;

    private void Start()
    {
        progress = FindAnyObjectByType<ProgressBar>();

        FindAnyObjectByType<CameraController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Switch(int type)
    {
        switch (type)
        {
            case 0:
                isReverseCipherEnabled = !isReverseCipherEnabled;
                break;
            case 1:
                isSkitalaCipherEnabled = !isSkitalaCipherEnabled;
                break;
            case 2:
                isLadderCipherEnabled = !isLadderCipherEnabled;
                break;
            case 3:
                isCaesarsCipherEnabled = !isCaesarsCipherEnabled;
                break;
            case 4:
                isCaesarsWithKeyCipherEnabled = !isCaesarsWithKeyCipherEnabled;
                break;
            case 5:
                isVigenereCipherEnabled = !isVigenereCipherEnabled;
                break;
            case 6:
                isHiddenSourceScreen = !isHiddenSourceScreen;
                break;
            default:
                break;
        }
    }

    public bool IsEnabled(CipherType type)
    {
        switch (type)
        {
            case CipherType.ReverseCipher:
                return isReverseCipherEnabled;
            case CipherType.LadderCipher:
                return isLadderCipherEnabled;
            case CipherType.SkitalaCipher:
                return isSkitalaCipherEnabled;
            case CipherType.CaesarCipher:
                return isCaesarsCipherEnabled;
            case CipherType.CaesarWithKeyCipher:
                return isCaesarsWithKeyCipherEnabled;
            case CipherType.VigenereCipher:
                return isVigenereCipherEnabled;
            default:
                throw new ArgumentException();
        }
    }

    public bool IsAnyCipherEnabled()
    {
        return isReverseCipherEnabled || 
            isLadderCipherEnabled || 
            isSkitalaCipherEnabled || 
            isCaesarsCipherEnabled || 
            isCaesarsWithKeyCipherEnabled || 
            isVigenereCipherEnabled;
    }

    public void Validate()
    {
        UpdateErrorText("");

        bool isValid = true;

        if (isReverseCipherEnabled)
            isValid &= ValidateLevel(CipherType.ReverseCipher, 0);

        if (isLadderCipherEnabled)
            isValid &= ValidateLevel(CipherType.LadderCipher, 1);

        if (isSkitalaCipherEnabled)
            isValid &= ValidateLevel(CipherType.SkitalaCipher, 2);

        if (isCaesarsCipherEnabled)
            isValid &= ValidateLevel(CipherType.CaesarCipher, 3);

        if (isCaesarsWithKeyCipherEnabled)
            isValid &= ValidateLevel(CipherType.CaesarWithKeyCipher, 4);

        if (isVigenereCipherEnabled)
            isValid &= ValidateLevel(CipherType.VigenereCipher, 5);

        if (!IsAnyCipherEnabled())
        {
            UpdateErrorText("Необходимо включить хоть один шифр");
            isValid = false;
        }

        if (isHiddenSourceScreen && PossibleTypes.Count < 2)
        {
            UpdateErrorText("Для игры с подбором шифра нужно выбрать как минимум 2 шифра");
            isValid = false;
        }

        IsValid = isValid;
    }

    public bool ValidateLevel(CipherType type, int neededLevel)
    {
        if (progress.CurrentLevel < neededLevel)
        {
            UpdateErrorText($"Необходим минимум {neededLevel + 1} уровень, чтобы включить этот шифр");
            return false;
        }

        return true;
    }

    public void UpdateErrorText(string text)
    {
        errorText.text = text;
    }
}
