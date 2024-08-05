using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FollowMouseAndScale : MonoBehaviour
{
    [SerializeField] GameObject cameraPos;
    [SerializeField] Canvas canvas; // World Space Canvas
    [SerializeField] List<RectTransform> scaleZones; // Yakla��nca k���lece�i b�lgeler
    [SerializeField] List<Button> buttons; // Butonlar
    [SerializeField] List<GameObject> activeObjects;
    [SerializeField] RectTransform targetObject;

    private Camera mainCamera;
    private HashSet<Button> clickedButtons = new HashSet<Button>();

    public Canvas GetCanvas() { return canvas; }
    public GameObject GetCameraPos() { return cameraPos; }

    private void Start()
    {
        mainCamera = Camera.main;

        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    private void OnEnable()
    {
        targetObject.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        targetObject.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // UI eleman�n� d�nya uzay�ndaki hit noktas�na ta��
            targetObject.position = hit.point;
            targetObject.rotation = Quaternion.LookRotation(targetObject.position - mainCamera.transform.position); // UI eleman�n�n kameraya bakmas� i�in
        }

        // En yak�n b�lgeye olan mesafeyi bul
        float minDistance = float.MaxValue;
        foreach (var zone in scaleZones)
        {
            if (!clickedButtons.Contains(zone.GetComponent<Button>())) // T�klanm�� butonlar�n oldu�u b�lgeleri yoksay
            {
                float zoneDistance = Vector3.Distance(targetObject.position, zone.position);
                if (zoneDistance < minDistance)
                {
                    minDistance = zoneDistance;
                }
            }
        }

        // Mesafeye ba�l� olarak �l�e�i de�i�tirin
        float scaleFactor = Mathf.Clamp01(minDistance / PaintingSearchSystem.Instance.GetScaleDistance()); // Mesafeyi 0 ile 1 aras�nda normalize et
        float scale = Mathf.Lerp(PaintingSearchSystem.Instance.GetMinScale(), PaintingSearchSystem.Instance.GetMaxScale(), scaleFactor); // Minimum ve maksimum �l�ek aras�nda ge�i� yap
        targetObject.localScale = new Vector3(scale, scale, scale);
    }

    private void OnButtonClicked(Button clickedButton)
    {
        clickedButtons.Add(clickedButton);
        UpdateUIForButton(clickedButton);

        if (clickedButtons.Count == buttons.Count)
            AllButtonsClicked();
    }

    private void UpdateUIForButton(Button clickedButton)
    {
        for (int i = 0; i < buttons.Count; i++)
            if (buttons[i] == clickedButton)
                activeObjects[i].gameObject.SetActive(true);
    }

    private void AllButtonsClicked()
    {
        PaintingSearchSystem.Instance.FinishPaintingSearch(gameObject);
        PaintingManager.Instance.PaintingAdd(gameObject);
        foreach (GameObject item in activeObjects)
            item.SetActive(false);
    }


}
