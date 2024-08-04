using UnityEngine;
using UnityEngine.UI;

public class InformationUISystem : MonoBehaviour
{
    public GameObject itemPrefab; // Ýtemlerin prefab'ý
    public RectTransform contentPanel; // ScrollView'ýn içindeki Content paneli
    public int itemsPerRow = 2; // Her satýrdaki item sayýsý
    public float itemSpacing = 10f; // Item'lar arasýndaki boþluk
    public Vector2 padding = new Vector2(10f, 10f); // Content panelin üst ve sol kenar boþluklarý

    void Start()
    {
        // Burada, bir örnek olarak 10 item ekliyoruz.
        int itemCount = 10;
        PopulateGrid(itemCount);
    }

    void PopulateGrid(int itemCount)
    {
        // Mevcut item'larý temizle
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Content paneli için RectTransform
        RectTransform contentRect = contentPanel.GetComponent<RectTransform>();

        // Grid'lerde item'larýn boyutlarý ve sayýsýnýn hesaplanmasý
        float itemWidth = itemPrefab.GetComponent<RectTransform>().rect.width;
        float itemHeight = itemPrefab.GetComponent<RectTransform>().rect.height;

        int rows = Mathf.CeilToInt((float)itemCount / itemsPerRow);

        // Content panelinin yüksekliðini hesapla
        float contentHeight = (itemHeight + itemSpacing) * rows - itemSpacing + padding.y * 2;

        // Content panelinin boyutlarýný ayarla
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

        for (int i = 0; i < itemCount; i++)
        {
            GameObject item = Instantiate(itemPrefab, contentPanel);
            RectTransform itemRect = item.GetComponent<RectTransform>();

            int row = i / itemsPerRow;
            int column = i % itemsPerRow;

            // Sol üstten dizmeye baþla
            itemRect.anchoredPosition = new Vector2(
                padding.x + column * (itemWidth + itemSpacing),
                -padding.y - row * (itemHeight + itemSpacing)
            );

            // Pivot noktasýný ayarla
            itemRect.pivot = new Vector2(0, 1);
        }
    }
}
