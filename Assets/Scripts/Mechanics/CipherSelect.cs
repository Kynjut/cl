using System;
using UnityEngine;
public class CipherSelect : MonoBehaviour
{
    public void SetConfig(GameConfig config)
    {
        if (!config.isHiddenSourceScreen)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!config.isReverseCipherEnabled)
            transform.GetChild(0).gameObject.SetActive(false);

        if (!config.isLadderCipherEnabled)
            transform.GetChild(1).gameObject.SetActive(false);

        if (!config.isSkitalaCipherEnabled)
            transform.GetChild(2).gameObject.SetActive(false);

        if (!config.isCaesarsCipherEnabled)
            transform.GetChild(3).gameObject.SetActive(false);

        if (!config.isCaesarsWithKeyCipherEnabled)
            transform.GetChild(4).gameObject.SetActive(false);

        if (!config.isVigenereCipherEnabled)
            transform.GetChild(5).gameObject.SetActive(false);
    }

    public void ChangeScreen(CipherType type)
    {
        GameObject screen = GameObject.FindWithTag("Screen");

        if (!IsOriginal(screen, type))
        {
            ScreensManager.MakeMockScreen(screen, type);
        }
        else
        {
            ScreensManager.ReturnOriginalCipher(screen);
        }
    }

    private bool IsOriginal(GameObject screen, CipherType type)
    {
        return screen.GetComponent<CipherContainer>().Cipher.Type == type;
    }
}
