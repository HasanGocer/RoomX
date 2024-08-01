using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] int interactiveID;
    private Vector3 originalPosition;
    private bool canDrag = false;

    // S�r�kleme i�lemi ba�lad���nda �a�r�l�r
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;

        if (EnvanterManager.Instance.envanterUI.itemChecked[interactiveID]) canDrag = true;
        else canDrag = false;
    }

    // S�r�kleme s�ras�nda �a�r�l�r
    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
            return;

        transform.position = Input.mousePosition;
    }

    // S�r�kleme bitti�inde �a�r�l�r
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag)
            return;

        // Nesneyi b�rakma i�lemi
        if (IsValidDrop(eventData))
        {
            // Ge�erli bir yere b�rak�lm��sa burada kalabilir
        }
        else
            transform.position = originalPosition;

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
