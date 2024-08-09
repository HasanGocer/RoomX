using UnityEngine;

public class SimplePull : MonoBehaviour
{
    public Transform character; // Karakterinin Transform komponentini buraya atamal�s�n
    public float rotationSpeed = 2f; // D�nme h�z�

    private void Update()
    {
        // Karakterin pozisyonunu hedef olarak al�yoruz
        Vector3 direction = character.position - transform.position;

        // Y ve X eksenindeki rotasyonu engelliyoruz, sadece Z ekseni etraf�nda d�necek
        direction.x = 0;
        direction.y = 0;

        // Hedef rotasyonu hesapl�yoruz (sadece Z ekseninde)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Kap�y� yava��a karaktere do�ru d�nd�r�yoruz
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
