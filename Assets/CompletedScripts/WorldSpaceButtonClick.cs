using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldSpaceButtonClick : MonoSingleton<WorldSpaceButtonClick>
{
    [SerializeField] Camera mainCamera;
    [SerializeField] PhysicsRaycaster physicsRaycaster;

    private void OnEnable()
    {
        physicsRaycaster.enabled = true;
    }

    private void OnDisable()
    {
        physicsRaycaster.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Button button = hit.transform.GetComponent<Button>();
                if (button != null)
                    button.onClick.Invoke();
            }
        }
    }
}
