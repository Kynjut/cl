using Assets.Scripts.Ciphers.Additional;
using System;
using System.Linq;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    [SerializeField]  private ProgressBar progress;
    [SerializeField] private GameObject screenPrefab;

    private CipherSelect cipherSwitcher;

    private GameConfig config;

    private GameObject previousCreatedScreen;

    private void Start()
    {
        cipherSwitcher = FindAnyObjectByType<CipherSelect>();
    }

    public void SetConfig(GameConfig config)
    {
        this.config = config;

        cipherSwitcher.SetConfig(config);
    }

    public void Next()
    {
        if (previousCreatedScreen)
        {
            Destroy(previousCreatedScreen);

            int score = previousCreatedScreen.GetComponentInChildren<DecodeScreen>().Cipher.ExpiriencePoints;
            progress.AddPoints(score);
        }
        
        Cipher cipher = RandomCipherGenerator.GetRandomCipher(config);
        ConfigureScreen(cipher, config);
    }

    void ConfigureScreen(Cipher cipher, GameConfig config)
    {
        GameObject screen = Instantiate(screenPrefab);
        screen.GetComponent<CipherContainer>().Cipher = cipher;
        screen.transform.position = screenPrefab.transform.position;

        previousCreatedScreen = screen;

        if (config.isHiddenSourceScreen && GetRandomType(config) is var rndType && rndType != cipher.Type)
        {
            MakeMockScreen(screen, rndType);
        }
        else
        {
            SwitchScreen(screen, cipher.Type);
        }

        DecodeScreen decodeScreen = screen.transform.GetComponentInChildren<DecodeScreen>();
        decodeScreen.SetCipher(cipher);
    }

    CipherType GetRandomType(GameConfig config)
    {
        CipherType type = config.PossibleTypes.OrderBy(x => UnityEngine.Random.Range(0, 1f)).First();

        return type;
    }

    public static void MakeMockScreen(GameObject screen, CipherType type)
    {
        CloseAllScreens(screen);
        SwitchScreen(screen, type);

        DecodeScreen decodeScreen = screen.transform.GetComponentInChildren<DecodeScreen>();
        Cipher originalCipher = screen.GetComponent<CipherContainer>().Cipher;
        decodeScreen.SetCipher(CipherMock.GetMock(originalCipher, type));
    }

    public static void ReturnOriginalCipher(GameObject screen)
    {
        CloseAllScreens(screen);

        Cipher originalCipher = screen.GetComponent<CipherContainer>().Cipher;
        SwitchScreen(screen, originalCipher.Type);

        DecodeScreen decodeScreen = screen.transform.GetComponentInChildren<DecodeScreen>();
        decodeScreen.SetCipher(originalCipher);
        decodeScreen.CheckWin();
    }

    public static void SwitchScreen(GameObject screen, CipherType type)
    {
        GetScreen(screen, type).SetActive(true);
    }

    public static GameObject GetScreen(GameObject screen, CipherType type)
    {
        switch (type)
        {
            case CipherType.ReverseCipher:
            case CipherType.SkitalaCipher:
            case CipherType.LadderCipher:
                return screen.transform.GetChild(0).gameObject;
            case CipherType.CaesarWithKeyCipher:
                return screen.transform.GetChild(2).gameObject;
            case CipherType.CaesarCipher:
                return screen.transform.GetChild(1).gameObject;
            case CipherType.VigenereCipher:
                return screen.transform.GetChild(3).gameObject;
            default: 
                throw new ArgumentException();
        }
    }

    public static void CloseAllScreens(GameObject screen)
    {
        for (int i = 0; i < screen.transform.childCount; i++)
        {
            screen.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
