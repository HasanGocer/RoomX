using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject doorCharacterInPos;
    [SerializeField] GameObject doorCharacterOutPos;
    [SerializeField] GameObject doorHandInPos;
    [SerializeField] GameObject doorHandOutPos;
    [SerializeField] float doorHandInCountdown;
    [SerializeField] float doorHandOutCountdown;

    public GameObject GetDoorCharacterInPos() { return doorCharacterInPos; }
    public GameObject GetDoorHandInPos() { return doorHandInPos; }
    public GameObject GetDoorCharacterOutPos() { return doorCharacterOutPos; }
    public GameObject GetDoorHandOutPos() { return doorHandOutPos; }
    public float GetDoorHandInCountdown() { return doorHandInCountdown; }
    public float GetDoorHandOutCountdown() { return doorHandOutCountdown; }
}
