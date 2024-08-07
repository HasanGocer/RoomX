using UnityEngine;
using UnityEngine.UI;

public class InformationUISystem : MonoBehaviour
{
    public GameObject itemPrefab; // �temlerin prefab'�
    public RectTransform contentPanel; // ScrollView'�n i�indeki Content paneli
    public int itemsPerRow = 2; // Her sat�rdaki item say�s�
    public float itemSpacing = 10f; // Item'lar aras�ndaki bo�luk
    public Vector2 padding = new Vector2(10f, 10f); // Content panelin �st ve sol kenar bo�luklar�

    void Start()
    {
        // Burada, bir �rnek olarak 10 item ekliyoruz.
        int itemCount = 10;
        PopulateGrid(itemCount);
    }

    void PopulateGrid(int itemCount)
    {
        // Mevcut item'lar� temizle
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Content paneli i�in RectTransform
        RectTransform contentRect = contentPanel.GetComponent<RectTransform>();

        // Grid'lerde item'lar�n boyutlar� ve say�s�n�n hesaplanmas�
        float itemWidth = itemPrefab.GetComponent<RectTransform>().rect.width;
        float itemHeight = itemPrefab.GetComponent<RectTransform>().rect.height;

        int rows = Mathf.CeilToInt((float)itemCount / itemsPerRow);

        // Content panelinin y�ksekli�ini hesapla
        float contentHeight = (itemHeight + itemSpacing) * rows - itemSpacing + padding.y * 2;

        // Content panelinin boyutlar�n� ayarla
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

        for (int i = 0; i < itemCount; i++)
        {
            GameObject item = Instantiate(itemPrefab, contentPanel);
            RectTransform itemRect = item.GetComponent<RectTransform>();

            int row = i / itemsPerRow;
            int column = i % itemsPerRow;

            // Sol �stten dizmeye ba�la
            itemRect.anchoredPosition = new Vector2(
                padding.x + column * (itemWidth + itemSpacing),
                -padding.y - row * (itemHeight + itemSpacing)
            );

            // Pivot noktas�n� ayarla
            itemRect.pivot = new Vector2(0, 1);
        }
    }
}
