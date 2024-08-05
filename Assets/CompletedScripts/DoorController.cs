using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject doorCharacterPos;

    public GameObject GetDoorCharacterPos() { return doorCharacterPos; }

    public void DoorOpen()
    {
        InteractiveManager.Instance.ChainIKOn();
        InteractiveManager.Instance.PerspectiveCameraOff();
    }


}
