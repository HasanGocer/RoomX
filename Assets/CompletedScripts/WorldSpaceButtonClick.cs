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
        // Sol týklama iþlemi gerçekleþtiðinde
        if (Input.GetMouseButtonDown(0))
        {
            // Týklama pozisyonundan bir ýþýn (ray) gönder
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Eðer ýþýn bir objeye çarparsa
            if (Physics.Raycast(ray, out hit))
            {
                // Çarpýlan objenin bir UI butonu olup olmadýðýný kontrol et
                Button button = hit.transform.GetComponent<Button>();
                if (button != null)
                {
                    Debug.Log("bastý");
                    // Butonun onClick eventini tetikle
                    button.onClick.Invoke();
                }
            }
        }
    }
}
