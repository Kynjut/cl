using UnityEngine;

public class ConfigButtonOperations : MonoBehaviour
{
    public void FinishConfig()
    {
        var config = FindAnyObjectByType<GameConfig>();

        if (!config.IsValid)
            return;

        config.gameObject.SetActive(false);

        var mgr = FindAnyObjectByType<ScreensManager>();
        mgr.SetConfig(config);
        mgr.Next();

        FindAnyObjectByType<CameraController>().enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
}
