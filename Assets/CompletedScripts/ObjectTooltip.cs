using UnityEngine;
using UnityEngine.UI;

public class ObjectTooltip : MonoSingleton<ObjectTooltip>
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Text tooltipText;
    [SerializeField] GameObject cursorImage;
    [SerializeField] string interactiveName;
    [SerializeField] string paintingName;
    [SerializeField] string DoorName;
    void Start()
    {
        tooltipText.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        cursorImage.SetActive(false);
    }
    private void OnEnable()
    {
        cursorImage.SetActive(true);
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MouseFollow();

            if (hit.collider.CompareTag(interactiveName))
            {
                tooltipText.text = "interaktif";
                tooltipText.gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag(paintingName))
            {
                tooltipText.text = "tablo";
                tooltipText.gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag(DoorName))
            {
                tooltipText.text = "kapý";
                tooltipText.gameObject.SetActive(true);
            }
            else
                tooltipText.gameObject.SetActive(false);
        }
        else
            tooltipText.gameObject.SetActive(false);
    }

    private void MouseFollow()
    {
        Vector2 mousePosition = Input.mousePosition;
        cursorImage.transform.position = mousePosition;
    }
}
