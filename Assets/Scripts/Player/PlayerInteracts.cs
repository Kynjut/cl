using Assets.Scripts;
using UnityEngine;

public class PlayerInteracts : MonoBehaviour
{
    private Raycast _raycast;

    private void Start()
    {
        _raycast = GetComponent<Raycast>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;

		PlayerRaycastTarget prt = _raycast.GetPlayerTarget();

        if (prt == null)
            return;

        if (prt.Target.tag == "CipherSelect")
        {
            CipherType type = prt.Target.GetComponent<CipherSelectData>().Type;
            FindAnyObjectByType<CipherSelect>().ChangeScreen(type);
        }
    }
}
