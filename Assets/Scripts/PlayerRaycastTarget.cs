using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerRaycastTarget
	{
		private GameObject target;
		public GameObject Target => target;

		private string tag;

		public PlayerRaycastTarget(GameObject target)
		{
			this.target = target;
			tag = target.tag;
		}
	}
}
