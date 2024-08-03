using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] float moveSpeed = 10.0f; // Kameranýn hareket hýzý
    Canvas worldSpaceCanvas; // World Space özellikli Canvas
    private RectTransform canvasRectTransform;

    void Start()
    {
        // Canvas'ýn RectTransform bileþenini al
        canvasRectTransform = worldSpaceCanvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        // Sol fare tuþuna basýlýp basýlmadýðýný kontrol ediyoruz
        if (Input.GetMouseButton(0))
        {
            // Fare hareketlerini al
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Kameranýn yeni pozisyonunu hesapla
            Vector3 newPosition = transform.position + new Vector3(-mouseX, -mouseY, 0) * moveSpeed * Time.deltaTime;

            // Kameranýn yeni pozisyonunu sýnýrlara göre ayarla
            newPosition = ClampPositionToCanvasBounds(newPosition);

            // Kameranýn yeni pozisyonunu uygula
            transform.position = newPosition;
        }
    }

    public void SetWorldSpaceCanvas(Canvas tempWorldSpaceCanvas)
    {
        worldSpaceCanvas = tempWorldSpaceCanvas;
    }

    // Kameranýn pozisyonunu Canvas'ýn sýnýrlarý içinde tutmak için kýsýtlama fonksiyonu
    private Vector3 ClampPositionToCanvasBounds(Vector3 position)
    {
        // Canvas'ýn dünyadaki pozisyonunu ve boyutunu al
        Vector3 canvasPosition = canvasRectTransform.position;
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        // Sýnýrlarý hesapla (bu örnekte ortada sabit bir pozisyon varsayýlýyor)
        float minX = canvasPosition.x - canvasSize.x / 2f;
        float maxX = canvasPosition.x + canvasSize.x / 2f;
        float minY = canvasPosition.y - canvasSize.y / 2f;
        float maxY = canvasPosition.y + canvasSize.y / 2f;

        // Pozisyonu sýnýrlara göre kýsýtla
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        return position;
    }
}
