using Assets.Scripts.Cyphers;
using TMPro;
using UnityEngine;

public class InputArrowsReader : InputReader
{
    public int Shift
    {
        get
        {
            int alphabetLength = AlphabetManager.GetAlphabet(alphabet).Length;
            int shift = _offset % alphabetLength;
            return shift < 0 ? shift + alphabetLength : shift;
        }
    }

    private int _offset;

    private Alphabet alphabet;

    public void SetAlphabet(Alphabet alphabet)
    {
        this.alphabet = alphabet;
    }

    protected override void ProcessCurrentPressedKeys()
    {
        if (_mustBeWatched)
        {
            if (FindAnyObjectByType<Raycast>().GetPlayerTarget()?.Target != _watchedObject)
                return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _offset++;
            _afterUpdate?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _offset--;
            _afterUpdate?.Invoke();
        }
    }
}
