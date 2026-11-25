using UnityEngine;

namespace Assets.Scripts.Mechanics.Decoders
{
    public class Decoder : MonoBehaviour
    {
		[SerializeField] protected GameObject _successScreen;

        public virtual void Decode() { }
    }
}
