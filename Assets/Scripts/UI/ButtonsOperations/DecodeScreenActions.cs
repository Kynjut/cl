using System.Collections;
using UnityEngine;

public class DecodeScreenActions : MonoBehaviour
{
    [SerializeField] GameObject SuccessScreen;

    private ScreensManager screensManager;

    private void Start()
    {
        screensManager = FindAnyObjectByType<ScreensManager>();
    }

    public void Win(int delayInMilliseconds)
    {
        StartCoroutine(WinDelayed(delayInMilliseconds));
    }

    public void Debug()
    {
        print("hello");
    }

    IEnumerator WinDelayed(int delayInMilliseconds)
    {
        yield return new WaitForSeconds(delayInMilliseconds / 1000.0f);

        SuccessScreen.SetActive(true);
        
        yield return new WaitForSeconds(delayInMilliseconds / 1000.0f);

        screensManager.Next();
    }
}
