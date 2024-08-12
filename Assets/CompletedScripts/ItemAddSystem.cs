using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddSystem : MonoSingleton<ItemAddSystem>
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                string objectName = hit.transform.name;

                if (objectName == "Item")
                {
                    EnvanterManager.Instance.ItemAdd(hit.transform.gameObject.GetComponent<InteractiveID>());
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
