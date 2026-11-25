using Assets.Scripts.UI.ButtonsOperations;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameMenu = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Disable();
    }

    public void Disable()
    {
        if (SwitchMenu(_gameMenu))
            return;
        else
        {
            _gameMenu.SetActive(true);

            FindAnyObjectByType<CameraController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public static bool SwitchMenu(GameObject menu)
    {
        if (menu == null)
            return false;

        bool state = (bool)menu?.activeInHierarchy;

        if (state)
        {
            menu.SetActive(false);
            FindAnyObjectByType<CameraController>().enabled = true;

			Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
		}

		return state;
	}
}
