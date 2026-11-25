using UnityEngine;

public class CipherSelectData : MonoBehaviour
{
    public CipherType Type => type;
    [SerializeField] private CipherType type;
}
