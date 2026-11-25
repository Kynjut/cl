using Assets.Scripts.Save;
using UnityEngine;

namespace Assets.Scripts.UI.ButtonsOperations
{
    public class MenuButtonOperations : MonoBehaviour
    {
        public void Contunue()
        {
            FindAnyObjectByType<CameraController>().enabled = true;
            gameObject.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void SetEducationCipher(int code)
        {
            GameSaves.PresentedCipher = (CipherType)code;
        }

        public void MainMenu()
        {
            FindAnyObjectByType<CleverSceneLoader>().LoadScene("menu");
        }
    }
}
