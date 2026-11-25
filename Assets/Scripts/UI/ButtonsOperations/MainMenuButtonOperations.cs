using UnityEngine;

namespace Assets.Scripts.UI.ButtonsOperations
{
	public class MainMenuButtonOperations : MonoBehaviour
	{
		public void Play()
		{
			FindAnyObjectByType<CleverSceneLoader>().LoadScene("game");
		}

		public void LoadMenu()
		{
            FindAnyObjectByType<CleverSceneLoader>().LoadScene("menu");
        }

		public void Quit()
		{
			Application.Quit();
		}
	}
}
