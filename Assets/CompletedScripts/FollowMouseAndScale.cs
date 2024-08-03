using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FollowMouseAndScale : MonoBehaviour
{
    [SerializeField] GameObject cameraPos;
    [SerializeField] Canvas canvas; // World Space Canvas
    [SerializeField] List<RectTransform> scaleZones; // Yaklaþýnca küçüleceði bölgeler
    [SerializeField] List<Button> buttons; // Butonlar
    [SerializeField] List<GameObject> activeObjects;
    [SerializeField] RectTransform targetObject;

    private Camera mainCamera;
    private HashSet<Button> clickedButtons = new HashSet<Button>();

    private void Start()
    {
        mainCamera = Camera.main;

        // Butonlara týklama olaylarýný ekle
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
        // Fare pozisyonunu dünya uzayýna dönüþtürmek için raycast kullan
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // UI elemanýný dünya uzayýndaki hit noktasýna taþý
            targetObject.position = hit.point;
            targetObject.rotation = Quaternion.LookRotation(targetObject.position - mainCamera.transform.position); // UI elemanýnýn kameraya bakmasý için
        }

        // En yakýn bölgeye olan mesafeyi bul
        float minDistance = float.MaxValue;
        foreach (var zone in scaleZones)
        {
            if (!clickedButtons.Contains(zone.GetComponent<Button>())) // Týklanmýþ butonlarýn olduðu bölgeleri yoksay
            {
                float zoneDistance = Vector3.Distance(targetObject.position, zone.position);
                if (zoneDistance < minDistance)
                {
                    minDistance = zoneDistance;
                }
            }
        }

        // Mesafeye baðlý olarak ölçeði deðiþtirin
        float scaleFactor = Mathf.Clamp01(minDistance / PaintingSearchSystem.Instance.GetScaleDistance()); // Mesafeyi 0 ile 1 arasýnda normalize et
        float scale = Mathf.Lerp(PaintingSearchSystem.Instance.GetMinScale(), PaintingSearchSystem.Instance.GetMaxScale(), scaleFactor); // Minimum ve maksimum ölçek arasýnda geçiþ yap
        targetObject.localScale = new Vector3(scale, scale, scale);
    }

    private void OnButtonClicked(Button clickedButton)
    {
        clickedButtons.Add(clickedButton);
        UpdateUIForButton(clickedButton);

        // Eðer tüm butonlar týklanmýþsa belirli bir fonksiyonu çalýþtýr
        if (clickedButtons.Count == buttons.Count)
        {
            AllButtonsClicked();
        }
    }

    private void UpdateUIForButton(Button clickedButton)
    {
        for (int i = 0; i < buttons.Count; i++)
            if (buttons[i] == clickedButton)
                activeObjects[i].gameObject.SetActive(true);
        // Týklanan butonun bölgesinde gösterilecek farklý image
        // Bu kýsmý ihtiyacýnýza göre özelleþtirin
        RectTransform zone = clickedButton.GetComponent<RectTransform>();
        // Örneðin, bir image deðiþtirme iþlemi yapabilirsiniz
        // Image img = zone.GetComponent<Image>();
        // img.sprite = someOtherSprite;
    }

    private void AllButtonsClicked()
    {
        PaintingSearchSystem.Instance.CameraOff(gameObject);
        foreach (GameObject item in activeObjects)
            item.SetActive(false);
        PaintingManager.Instance.PaintingAdd(gameObject);
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }

    public GameObject GetCameraPos()
    {
        return cameraPos;
    }
}
