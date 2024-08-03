using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public enum FlipMode
{
    RightToLeft,
    LeftToRight
}

[ExecuteInEditMode]
public class Book : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField]
    RectTransform BookPanel;
    public GameObject background;
    public GameObject[] bookPages;
    public bool interactable = true;
    public bool enableShadowEffect = true;

    public int currentPage = 0;
    public int TotalPageCount
    {
        get { return bookPages.Length; }
    }
    public Vector3 EndBottomLeft
    {
        get { return ebl; }
    }
    public Vector3 EndBottomRight
    {
        get { return ebr; }
    }
    public float Height
    {
        get
        {
            return BookPanel.rect.height;
        }
    }
    public Image ClippingPlane;
    public Image NextPageClip;
    public Image Shadow;
    public Image ShadowLTR;
    public Image Left;
    public Image LeftNext;
    public Image Right;
    public Image RightNext;
    public UnityEvent OnFlip;

    float radius1, radius2;
    Vector3 sb, st, c, ebr, ebl, f;
    bool pageDragging = false;
    FlipMode mode;

    void Start()
    {
        if (!canvas) canvas = GetComponentInParent<Canvas>();
        if (!canvas) Debug.LogError("Book should be a child to canvas");

        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        UpdateSprites();
        CalcCurlCriticalPoints();

        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        NextPageClip.rectTransform.sizeDelta = new Vector2(pageWidth, pageHeight + pageHeight * 2);
        ClippingPlane.rectTransform.sizeDelta = new Vector2(pageWidth * 2 + pageHeight, pageHeight + pageHeight * 2);

        float hyp = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
        float shadowPageHeight = pageWidth / 2 + hyp;

        Shadow.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        Shadow.rectTransform.pivot = new Vector2(1, (pageWidth / 2) / shadowPageHeight);

        ShadowLTR.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        ShadowLTR.rectTransform.pivot = new Vector2(0, (pageWidth / 2) / shadowPageHeight);
    }

    private void CalcCurlCriticalPoints()
    {
        sb = new Vector3(0, -BookPanel.rect.height / 2);
        ebr = new Vector3(BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        ebl = new Vector3(-BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        st = new Vector3(0, BookPanel.rect.height / 2);
        radius1 = Vector2.Distance(sb, ebr);
        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        radius2 = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
    }

    public Vector3 transformPoint(Vector3 mouseScreenPos)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Vector3 mouseWorldPos = canvas.worldCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, canvas.planeDistance));
            Vector2 localPos = BookPanel.InverseTransformPoint(mouseWorldPos);

            return localPos;
        }
        else if (canvas.renderMode == RenderMode.WorldSpace)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 globalEBR = transform.TransformPoint(ebr);
            Vector3 globalEBL = transform.TransformPoint(ebl);
            Vector3 globalSt = transform.TransformPoint(st);
            Plane p = new Plane(globalEBR, globalEBL, globalSt);
            float distance;
            p.Raycast(ray, out distance);
            Vector2 localPos = BookPanel.InverseTransformPoint(ray.GetPoint(distance));
            return localPos;
        }
        else
        {
            Vector2 localPos = BookPanel.InverseTransformPoint(mouseScreenPos);
            return localPos;
        }
    }

    void Update()
    {
        if (pageDragging && interactable)
        {
            UpdateBook();
        }
    }

    public void UpdateBook()
    {
        f = Vector3.Lerp(f, transformPoint(Input.mousePosition), Time.deltaTime * 10);
        if (mode == FlipMode.RightToLeft)
            UpdateBookRTLToPoint(f);
        else
            UpdateBookLTRToPoint(f);
    }

    public void UpdateBookLTRToPoint(Vector3 followLocation)
    {
        mode = FlipMode.LeftToRight;
        f = followLocation;
        ShadowLTR.transform.SetParent(ClippingPlane.transform, true);
        ShadowLTR.transform.localPosition = new Vector3(0, 0, 0);
        ShadowLTR.transform.localEulerAngles = new Vector3(0, 0, 0);
        Left.transform.SetParent(ClippingPlane.transform, true);

        Right.transform.SetParent(BookPanel.transform, true);
        Right.transform.localEulerAngles = Vector3.zero;
        LeftNext.transform.SetParent(BookPanel.transform, true);

        c = Calc_C_Position(followLocation);
        Vector3 t1;
        float clipAngle = CalcClipAngle(c, ebl, out t1);
        clipAngle = (clipAngle + 180) % 180;

        ClippingPlane.transform.localEulerAngles = new Vector3(0, 0, clipAngle - 90);
        ClippingPlane.transform.position = BookPanel.TransformPoint(t1);

        Left.transform.position = BookPanel.TransformPoint(c);
        float C_T1_dy = t1.y - c.y;
        float C_T1_dx = t1.x - c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        Left.transform.localEulerAngles = new Vector3(0, 0, C_T1_Angle - 90 - clipAngle);

        NextPageClip.transform.localEulerAngles = new Vector3(0, 0, clipAngle - 90);
        NextPageClip.transform.position = BookPanel.TransformPoint(t1);
        LeftNext.transform.SetParent(NextPageClip.transform, true);
        Right.transform.SetParent(ClippingPlane.transform, true);
        Right.transform.SetAsFirstSibling();

        ShadowLTR.rectTransform.SetParent(Left.rectTransform, true);
    }

    public void UpdateBookRTLToPoint(Vector3 followLocation)
    {
        mode = FlipMode.RightToLeft;
        f = followLocation;
        Shadow.transform.SetParent(ClippingPlane.transform, true);
        Shadow.transform.localPosition = Vector3.zero;
        Shadow.transform.localEulerAngles = Vector3.zero;
        Right.transform.SetParent(ClippingPlane.transform, true);

        Left.transform.SetParent(BookPanel.transform, true);
        Left.transform.localEulerAngles = Vector3.zero;
        RightNext.transform.SetParent(BookPanel.transform, true);
        c = Calc_C_Position(followLocation);
        Vector3 t1;
        float clipAngle = CalcClipAngle(c, ebr, out t1);
        if (clipAngle > -90) clipAngle += 180;

        ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);
        ClippingPlane.transform.localEulerAngles = new Vector3(0, 0, clipAngle + 90);
        ClippingPlane.transform.position = BookPanel.TransformPoint(t1);

        Right.transform.position = BookPanel.TransformPoint(c);
        float C_T1_dy = t1.y - c.y;
        float C_T1_dx = t1.x - c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        Right.transform.localEulerAngles = new Vector3(0, 0, C_T1_Angle - (clipAngle + 90));

        NextPageClip.transform.localEulerAngles = new Vector3(0, 0, clipAngle + 90);
        NextPageClip.transform.position = BookPanel.TransformPoint(t1);
        RightNext.transform.SetParent(NextPageClip.transform, true);
        Left.transform.SetParent(ClippingPlane.transform, true);
        Left.transform.SetAsFirstSibling();

        Shadow.rectTransform.SetParent(Right.rectTransform, true);
    }

    private float CalcClipAngle(Vector3 c, Vector3 bookCorner, out Vector3 t1)
    {
        float C_B_dy = bookCorner.y - c.y;
        float C_B_dx = bookCorner.x - c.x;
        float C_B_Angle = Mathf.Atan2(C_B_dy, C_B_dx);

        t1 = new Vector3(c.x + 2 * radius1 * Mathf.Cos(C_B_Angle), c.y + 2 * radius1 * Mathf.Sin(C_B_Angle), 0);

        float T1_B_dy = bookCorner.y - t1.y;
        float T1_B_dx = bookCorner.x - t1.x;

        float T1_B_angle = Mathf.Atan2(T1_B_dy, T1_B_dx);
        float C_T1_B_Angle = T1_B_angle - C_B_Angle;

        if (Mathf.Cos(C_T1_B_Angle) > 0)
        {
            return Mathf.Rad2Deg * C_B_Angle;
        }
        else
        {
            return Mathf.Rad2Deg * T1_B_angle;
        }
    }

    private Vector3 Calc_C_Position(Vector3 followLocation)
    {
        float F_SB_dy = sb.y - followLocation.y;
        float F_SB_dx = followLocation.x - sb.x;
        float F_SB_Angle = Mathf.Atan2(F_SB_dy, F_SB_dx);
        Vector3 r1 = new Vector3(sb.x + radius1 * Mathf.Cos(F_SB_Angle), sb.y - radius1 * Mathf.Sin(F_SB_Angle), 0);

        float F_ST_dy = followLocation.y - st.y;
        float F_ST_dx = followLocation.x - st.x;
        float F_ST_Angle = Mathf.Atan2(F_ST_dy, F_ST_dx);
        Vector3 r2 = new Vector3(st.x + radius1 * Mathf.Cos(F_ST_Angle), st.y + radius1 * Mathf.Sin(F_ST_Angle), 0);

        float R1_F_dy = followLocation.y - r1.y;
        float R1_F_dx = followLocation.x - r1.x;

        float R2_F_dy = followLocation.y - r2.y;
        float R2_F_dx = followLocation.x - r2.x;

        if ((R1_F_dx * R1_F_dx + R1_F_dy * R1_F_dy) < (R2_F_dx * R2_F_dx + R2_F_dy * R2_F_dy))
        {
            return r1;
        }
        else
        {
            return r2;
        }
    }

    public void DragRightPageToLeft()
    {
        if (currentPage >= bookPages.Length) return;
        if (pageDragging) return;
        pageDragging = true;
        f = ebr;
        ClippingPlane.rectTransform.pivot = new Vector2(0, 0.35f);

        Left.gameObject.SetActive(true);
        Left.overrideSprite = background.GetComponent<Image>().sprite;

        if (currentPage < bookPages.Length - 1)
        {
            LeftNext.overrideSprite = bookPages[currentPage].GetComponent<Image>().sprite;
            Right.overrideSprite = bookPages[currentPage + 1].GetComponent<Image>().sprite;
            RightNext.overrideSprite = (currentPage < bookPages.Length - 2) ? bookPages[currentPage + 2].GetComponent<Image>().sprite : background.GetComponent<Image>().sprite;
        }
        else
        {
            LeftNext.overrideSprite = bookPages[currentPage].GetComponent<Image>().sprite;
            Right.overrideSprite = background.GetComponent<Image>().sprite;
            RightNext.overrideSprite = background.GetComponent<Image>().sprite;
        }
        mode = FlipMode.RightToLeft;
        OnFlip.Invoke();
    }

    public void DragLeftPageToRight()
    {
        if (currentPage <= 0) return;
        if (pageDragging) return;
        pageDragging = true;
        f = ebl;
        ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);

        Right.gameObject.SetActive(true);
        Right.overrideSprite = background.GetComponent<Image>().sprite;
        Left.overrideSprite = (currentPage >= 2) ? bookPages[currentPage - 2].GetComponent<Image>().sprite : background.GetComponent<Image>().sprite;

        if (currentPage >= 1)
        {
            RightNext.overrideSprite = bookPages[currentPage - 1].GetComponent<Image>().sprite;
            LeftNext.overrideSprite = bookPages[currentPage].GetComponent<Image>().sprite;
        }
        else
        {
            LeftNext.overrideSprite = bookPages[currentPage].GetComponent<Image>().sprite;
            RightNext.overrideSprite = background.GetComponent<Image>().sprite;
        }
        mode = FlipMode.LeftToRight;
        OnFlip.Invoke();
    }

    public void OnMouseRelease()
    {
        if (!pageDragging) return;
        pageDragging = false;

        if (mode == FlipMode.RightToLeft)
        {
            if (c.x > 0)
                TweenBack();
            else
                TweenForward();
        }
        else
        {
            if (c.x < 0)
                TweenBack();
            else
                TweenForward();
        }
    }

    public void UpdateSprites()
    {
        if (currentPage > 0 && currentPage < bookPages.Length)
        {
            Left.sprite = bookPages[currentPage - 1].GetComponent<Image>().sprite;
            Right.sprite = bookPages[currentPage].GetComponent<Image>().sprite;
        }
        else if (currentPage == 0)
        {
            Left.sprite = background.GetComponent<Image>().sprite;
            Right.sprite = bookPages[currentPage].GetComponent<Image>().sprite;
        }
    }

    public void TweenForward()
    {
        currentPage += (mode == FlipMode.RightToLeft) ? 2 : -2;
        if (currentPage > bookPages.Length) currentPage = bookPages.Length;
        if (currentPage < 0) currentPage = 0;

        UpdateSprites();
        ClippingPlane.transform.localEulerAngles = Vector3.zero;
        ClippingPlane.transform.localPosition = Vector3.zero;
        Shadow.rectTransform.SetParent(transform, true);
        ShadowLTR.rectTransform.SetParent(transform, true);
        Left.transform.SetParent(BookPanel.transform, true);
        LeftNext.transform.SetParent(BookPanel.transform, true);
        Right.transform.SetParent(BookPanel.transform, true);
        RightNext.transform.SetParent(BookPanel.transform, true);
    }

    public void TweenBack()
    {
        currentPage += (mode == FlipMode.RightToLeft) ? -2 : 2;
        if (currentPage > bookPages.Length) currentPage = bookPages.Length;
        if (currentPage < 0) currentPage = 0;

        UpdateSprites();
        ClippingPlane.transform.localEulerAngles = Vector3.zero;
        ClippingPlane.transform.localPosition = Vector3.zero;
        Shadow.rectTransform.SetParent(transform, true);
        ShadowLTR.rectTransform.SetParent(transform, true);
        Left.transform.SetParent(BookPanel.transform, true);
        LeftNext.transform.SetParent(BookPanel.transform, true);
        Right.transform.SetParent(BookPanel.transform, true);
        RightNext.transform.SetParent(BookPanel.transform, true);
    }

    public void LoadPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex > bookPages.Length) return;
        currentPage = pageIndex;
        UpdateSprites();
    }

    public IEnumerator FlipRightPage()
    {
        DragRightPageToLeft();
        yield return new WaitForSeconds(0.15f);
        OnMouseRelease();
    }

    public IEnumerator FlipLeftPage()
    {
        DragLeftPageToRight();
        yield return new WaitForSeconds(0.15f);
        OnMouseRelease();
    }

    public void EnableShadowEffect(bool enable)
    {
        enableShadowEffect = enable;
        Shadow.gameObject.SetActive(enableShadowEffect);
        ShadowLTR.gameObject.SetActive(enableShadowEffect);
    }
}
