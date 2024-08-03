using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] float moveSpeed = 10.0f; // Kameran�n hareket h�z�
    Canvas worldSpaceCanvas; // World Space �zellikli Canvas
    private RectTransform canvasRectTransform;

    void Start()
    {
        // Canvas'�n RectTransform bile�enini al
        canvasRectTransform = worldSpaceCanvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        // Sol fare tu�una bas�l�p bas�lmad���n� kontrol ediyoruz
        if (Input.GetMouseButton(0))
        {
            // Fare hareketlerini al
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Kameran�n yeni pozisyonunu hesapla
            Vector3 newPosition = transform.position + new Vector3(-mouseX, -mouseY, 0) * moveSpeed * Time.deltaTime;

            // Kameran�n yeni pozisyonunu s�n�rlara g�re ayarla
            newPosition = ClampPositionToCanvasBounds(newPosition);

            // Kameran�n yeni pozisyonunu uygula
            transform.position = newPosition;
        }
    }

    public void SetWorldSpaceCanvas(Canvas tempWorldSpaceCanvas)
    {
        worldSpaceCanvas = tempWorldSpaceCanvas;
    }

    // Kameran�n pozisyonunu Canvas'�n s�n�rlar� i�inde tutmak i�in k�s�tlama fonksiyonu
    private Vector3 ClampPositionToCanvasBounds(Vector3 position)
    {
        // Canvas'�n d�nyadaki pozisyonunu ve boyutunu al
        Vector3 canvasPosition = canvasRectTransform.position;
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        // S�n�rlar� hesapla (bu �rnekte ortada sabit bir pozisyon varsay�l�yor)
        float minX = canvasPosition.x - canvasSize.x / 2f;
        float maxX = canvasPosition.x + canvasSize.x / 2f;
        float minY = canvasPosition.y - canvasSize.y / 2f;
        float maxY = canvasPosition.y + canvasSize.y / 2f;

        // Pozisyonu s�n�rlara g�re k�s�tla
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        return position;
    }
}
