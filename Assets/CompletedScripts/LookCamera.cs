using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject character;

    void Update()
    {
        transform.position = character.transform.position + new Vector3(0, 1, 0);
        transform.LookAt(mainCamera.transform.position);
    }
}
