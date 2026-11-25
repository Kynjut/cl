using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;
    [SerializeField] private float delayInSeconds, delayStep;
    private float currDelay;

    private Image pressDelayInfoImage;

    private void Start()
    {
        pressDelayInfoImage = GameObject.Find("UICanvas").transform
          .Find("PressDelayInfo").GetComponent<Image>();
    }

    public void TeleportDelayed(GameObject gm)
    {
        currDelay = 0;
        pressDelayInfoImage.fillAmount = 0;
        pressDelayInfoImage.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TeleportDelay(gm));
    }

    private IEnumerator TeleportDelay(GameObject gm)
    {
        currDelay += delayStep;
        pressDelayInfoImage.fillAmount = currDelay / delayInSeconds;

        yield return new WaitForSeconds(delayStep);

        if (!Input.GetKey(KeyCode.E))
        {
            pressDelayInfoImage.gameObject.SetActive(false);
            yield break;
        }

        if (currDelay >= delayInSeconds)
        {
            currDelay = 0;
            pressDelayInfoImage.gameObject.SetActive(false);
            TeleportImmediately(gm);
        }
        else
            StartCoroutine(TeleportDelay(gm));
    }

    public void TeleportImmediately(GameObject gm)
    {
        gm.transform.position = teleportTo.position;
        StartCoroutine(W());
    }

    private IEnumerator W()
    {
        yield return new WaitForFixedUpdate();
    }
}