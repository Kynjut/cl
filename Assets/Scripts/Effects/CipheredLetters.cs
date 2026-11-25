using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Assets.Scripts.Cyphers;

public class CipheredLetters : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private float duration = 2f;
    [SerializeField] private float interval = 0.1f;
    private TextMeshProUGUI tmpro;

    private string _originalText;
    private Coroutine _routine;
    private char[] _alphabet;
    private bool _isDecrypted;

    private void Start()
    {
        _alphabet = AlphabetManager.GetAlphabet(Alphabet.RU);
        tmpro = tmpro ?? GetComponentInChildren<TextMeshProUGUI>();
        _originalText = tmpro.text;
        Encrypt();
    }

    private void Encrypt()
    {
        char[] chars = _originalText.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
            if (char.IsLetter(chars[i]))
                chars[i] = _alphabet[Random.Range(0, _alphabet.Length)];

        tmpro.text = new string(chars);
        _isDecrypted = false;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (_isDecrypted) return;

        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        char[] current = tmpro.text.ToCharArray();
        char[] target = _originalText.ToCharArray();
        float elapsed = 0f;

        while (elapsed < duration)
        {
            int randomIndex = Random.Range(0, current.Length);
            if (char.IsLetter(current[randomIndex]))
            {
                char random = _alphabet[Random.Range(0, _alphabet.Length)];
                current[randomIndex] = char.IsUpper(target[randomIndex]) ? char.ToUpper(random) : char.ToLower(random);
            }

            tmpro.text = new string(current);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        int index = 0;
        while (true)
        {
            if (index < current.Length && char.IsLetter(target[index]))
                current[index] = target[index];

            tmpro.text = new string(current);

            bool done = true;
            for (int i = 0; i < current.Length; i++)
                if (char.IsLetter(current[i]) && current[i] != target[i])
                {
                    done = false;
                    break;
                }

            if (done) break;

            yield return new WaitForSeconds(interval);
            index = (index + 1) % current.Length;
        }

        _isDecrypted = true;
        _routine = null;
    }
}