using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject doorCharacterInPos;
    [SerializeField] GameObject doorCharacterOutPos;
    [SerializeField] GameObject doorHandInPos;
    [SerializeField] GameObject doorHandOutPos;

    public GameObject GetDoorCharacterInPos() { return doorCharacterInPos; }
    public GameObject GetDoorHandInPos() { return doorHandInPos; }
    public GameObject GetDoorCharacterOutPos() { return doorCharacterOutPos; }
    public GameObject GetDoorHandOutPos() { return doorHandOutPos; }
}
