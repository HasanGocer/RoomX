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
        // Sol t�klama i�lemi ger�ekle�ti�inde
        if (Input.GetMouseButtonDown(0))
        {
            // T�klama pozisyonundan bir ���n (ray) g�nder
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // E�er ���n bir objeye �arparsa
            if (Physics.Raycast(ray, out hit))
            {
                // �arp�lan objenin bir UI butonu olup olmad���n� kontrol et
                Button button = hit.transform.GetComponent<Button>();
                if (button != null)
                {
                    Debug.Log("bast�");
                    // Butonun onClick eventini tetikle
                    button.onClick.Invoke();
                }
            }
        }
    }
}
