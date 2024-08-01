using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] int interactiveID;
    private Vector3 originalPosition;

    // Sürükleme iþlemi baþladýðýnda çaðrýlýr
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Sürükleme izni kontrolü
        originalPosition = transform.position;
        if (EnvanterManager.Instance.envanter.itemChecked[interactiveID])
            return;

    }


    // Sürükleme sýrasýnda çaðrýlýr
    public void OnDrag(PointerEventData eventData)
    {
        // Nesneyi farenin konumuna göre sürükleme
        transform.position = Input.mousePosition;
    }

    // Sürükleme bittiðinde çaðrýlýr
    public void OnEndDrag(PointerEventData eventData)
    {
        // Nesneyi býrakma iþlemi
        if (IsValidDrop(eventData))
        {
            // Geçerli bir yere býrakýlmýþsa burada kalabilir
        }
        else
        {
            // Geçersiz bir yere býrakýlmýþsa orijinal konumuna geri döner
            transform.position = originalPosition;
        }
    }

    // Nesneyi býrakma yerini kontrol etmek için kullanýlabilir
    private bool IsValidDrop(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Eðer hit olan nesnenin tag'i belirlenen tag ile uyuþuyorsa
            if (hit.collider.CompareTag("Wall"))
            {
                return true; // Geçerli býrakma yeri
            }
        }

        return false; // Geçersiz býrakma yeri
    }
}
