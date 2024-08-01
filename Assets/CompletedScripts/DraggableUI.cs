using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] int interactiveID;
    private Vector3 originalPosition;

    // S�r�kleme i�lemi ba�lad���nda �a�r�l�r
    public void OnBeginDrag(PointerEventData eventData)
    {
        // S�r�kleme izni kontrol�
        originalPosition = transform.position;
        if (EnvanterManager.Instance.envanter.itemChecked[interactiveID])
            return;

    }


    // S�r�kleme s�ras�nda �a�r�l�r
    public void OnDrag(PointerEventData eventData)
    {
        // Nesneyi farenin konumuna g�re s�r�kleme
        transform.position = Input.mousePosition;
    }

    // S�r�kleme bitti�inde �a�r�l�r
    public void OnEndDrag(PointerEventData eventData)
    {
        // Nesneyi b�rakma i�lemi
        if (IsValidDrop(eventData))
        {
            // Ge�erli bir yere b�rak�lm��sa burada kalabilir
        }
        else
        {
            // Ge�ersiz bir yere b�rak�lm��sa orijinal konumuna geri d�ner
            transform.position = originalPosition;
        }
    }

    // Nesneyi b�rakma yerini kontrol etmek i�in kullan�labilir
    private bool IsValidDrop(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // E�er hit olan nesnenin tag'i belirlenen tag ile uyu�uyorsa
            if (hit.collider.CompareTag("Wall"))
            {
                return true; // Ge�erli b�rakma yeri
            }
        }

        return false; // Ge�ersiz b�rakma yeri
    }
}
