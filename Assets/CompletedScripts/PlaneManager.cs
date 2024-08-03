using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoSingleton<PlaneManager>
{
    [SerializeField] GameObject targetPlane;

    public GameObject GetTargetPlane()
    {
        return targetPlane;
    }

}
