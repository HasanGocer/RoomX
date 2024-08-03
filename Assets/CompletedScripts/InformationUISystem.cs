using UnityEngine;
using UnityEngine.UI;

public class InformationUISystem : MonoBehaviour
{
    public GameObject itemPrefab; // Ýtemlerin prefab'ý
    public RectTransform contentPanel; // ScrollView'ýn içindeki Content paneli
    public int itemsPerRow = 2; // Her satýrdaki item sayýsý
    public float itemSpacing = 10f; // Item'lar arasýndaki boþluk

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

        // Content panelinin boyutlarýný ayarla
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, (itemHeight + itemSpacing) * rows - itemSpacing);

        for (int i = 0; i < itemCount; i++)
        {
            GameObject item = Instantiate(itemPrefab, contentPanel);
            RectTransform itemRect = item.GetComponent<RectTransform>();

            int row = i / itemsPerRow;
            int column = i % itemsPerRow;

            itemRect.anchoredPosition = new Vector2(
                column * (itemWidth + itemSpacing),
                -row * (itemHeight + itemSpacing)
            );
        }
    }
}
