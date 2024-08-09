using UnityEngine;

public class SimplePull : MonoBehaviour
{
    public Transform character; // Karakterinin Transform komponentini buraya atamalýsýn
    public float rotationSpeed = 2f; // Dönme hýzý

    private void Update()
    {
        // Karakterin pozisyonunu hedef olarak alýyoruz
        Vector3 direction = character.position - transform.position;

        // Y ve X eksenindeki rotasyonu engelliyoruz, sadece Z ekseni etrafýnda dönecek
        direction.x = 0;
        direction.y = 0;

        // Hedef rotasyonu hesaplýyoruz (sadece Z ekseninde)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Kapýyý yavaþça karaktere doðru döndürüyoruz
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
