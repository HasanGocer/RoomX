using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenManager : MonoSingleton<SeenManager>
{
    int SeenDistance;

    public int GetSeenDistance() { return SeenDistance; }
}
