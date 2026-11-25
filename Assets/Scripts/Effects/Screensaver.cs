using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Assets.Scripts.Cyphers;
using Assets.Scripts.Save;

public class Screensaver : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI encryptedText;
    [SerializeField] private TextMeshProUGUI decryptedText;

    [Header("Settings")]
    [SerializeField] private int minWordLength = 4;
    [SerializeField] private int maxWordLength = 8;
    [SerializeField] private float letterDuration = 0.3f;
    [SerializeField] private float beforeSceneDelay = 0.3f;
    [SerializeField] private string sceneToLoad = "Game";

    private void Start()
    {
        if (encryptedText != null) encryptedText.text = "";
        if (decryptedText != null) decryptedText.text = "";

        StartCoroutine(PlayScreensaver());
    }

    private IEnumerator PlayScreensaver()
    {
        Localization localization = GameSaves.Instance.Localization;
        char[] alphabet = AlphabetManager.GetAlphabet(localization);
        string secret = PickWord(localization, minWordLength, maxWordLength);

        string encrypted = "";
        string decrypted = "";

        for (int i = 0; i < secret.Length; i++)
        {
            char randomLetter = alphabet[Random.Range(0, alphabet.Length)];
            encrypted += randomLetter;

            char realLetter = secret[i];
            decrypted += realLetter;

            if (encryptedText != null) encryptedText.text = decrypted;
            if (decryptedText != null) decryptedText.text = encrypted;

            yield return new WaitForSeconds(letterDuration);
        }

        yield return new WaitForSeconds(beforeSceneDelay);

        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
    }

    private string PickWord(Localization localization, int minLength, int maxLength)
    {
        string[] pool = RandomCipherGenerator.Words[localization];

        int lo = Mathf.Max(1, minLength);
        int hi = Mathf.Max(lo, maxLength);

        string result = null;

        for (int i = 0; i < pool.Length * 2; i++)
        {
            string candidate = pool[Random.Range(0, pool.Length)];
            if (candidate.Length >= lo && candidate.Length <= hi)
            {
                result = candidate;
                break;
            }
        }

        if (result == null)
        {
            foreach (string w in pool)
            {
                if (w.Length >= lo && w.Length <= hi)
                {
                    result = w;
                    break;
                }
            }
        }

        if (result == null)
            result = pool[0];

        return result.ToUpper();
    }
}