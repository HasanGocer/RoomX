using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject doorCharacterPos;
    [SerializeField] GameObject doorHandPos;

    public GameObject GetDoorCharacterPos() { return doorCharacterPos; }
    public GameObject GetDoorHandPos() { return doorHandPos; }
}
