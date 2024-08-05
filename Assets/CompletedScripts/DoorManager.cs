using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoSingleton<DoorManager>
{
    [SerializeField] AnimationClip doorClip;


    public float GetDoorClipTime() { return doorClip.length; }
}
