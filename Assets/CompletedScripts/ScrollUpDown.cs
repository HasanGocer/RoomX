using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUpDown : MonoSingleton<ScrollUpDown>
{
    [Range(0.80f, .99f)]
    [SerializeField] float scrollValue = .90f;
    [Range(0.01f, 0.1f)]
    [SerializeField] float scrollSensitivity = 0.01f;

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            scrollValue -= scrollInput * scrollSensitivity;
            if (scrollValue < .80f) scrollValue = .80f;
            else if (scrollValue > .99f) scrollValue = .99f;
        }
    }

    public float GetScrollValue()
    {
        return scrollValue;
    }
    public void SetScrollSensitivity(float tempScrollSensitivity)
    {
        if (tempScrollSensitivity > 0.5f && tempScrollSensitivity < 1)
            scrollSensitivity = tempScrollSensitivity;
    }
    public float GetScrollSensitivity()
    {
        return scrollValue;
    }
}
