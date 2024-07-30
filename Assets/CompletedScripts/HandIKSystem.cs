using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIKSystem : MonoBehaviour
{
    void Start()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        CharCon.Instance.IK();
    }

}
