using DG.Tweening.Plugins.Options;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CameraDirectionChange : MonoBehaviour
{
    public enum Direction
    {
        northWest = 1,
        southWest = 2,
        northEast = 3,
        southEast = 4,
    }

    public GameObject north;
    public Renderer northRenderer;
    public GameObject south;
    public Renderer southRenderer;
    public GameObject east;
    public Renderer eastRenderer;
    public GameObject west;
    public Renderer westRenderer;

    [SerializeField] GameObject FindAngle;



    Direction directionCount = Direction.northWest;

    public void Update()
    {
        UpdateDirection();

        Color currentColor = northRenderer.material.color;
        currentColor.a = 0;
        northRenderer.material.color = currentColor;
    }

    private void UpdateDirection()
    {
        SetRotation();
        SelectRotation();
    }

    private void OpenAll()
    {
        south.SetActive(true);
        east.SetActive(true);
        north.SetActive(true);
        west.SetActive(true);
    }

    private void SelectRotation()
    {
        float angle = FindAngle.transform.rotation.eulerAngles.y;

        if (angle < 30 || angle > 330)
        {
            OpenAll();
            SetMaterail(southRenderer, eastRenderer, northRenderer, westRenderer, 30);
        }
        else if (angle < 60)
        {
            SetDirection(south, east, north, west);
        }
        else if (angle < 120)
        {
            OpenAll();
            SetMaterail(southRenderer, westRenderer, northRenderer, eastRenderer, 120);
        }
        else if (angle < 150)
        {
            SetDirection(south, west, north, east);
        }
        else if (angle < 210)
        {
            OpenAll();
            SetMaterail(northRenderer, westRenderer, southRenderer, eastRenderer, 210);
        }
        else if (angle < 240)
        {
            SetDirection(north, west, south, east);
        }
        else if (angle < 300)
        {
            OpenAll();
            SetMaterail(northRenderer, eastRenderer, southRenderer, westRenderer, 300);
        }
        else if (angle < 330)
        {
            SetDirection(north, east, south, west);
        }
    }

    private void SetMaterail(Renderer ren1, Renderer ren2, Renderer ren3, Renderer ren4, int minangle)
    {

        Color currentColor = ren1.material.color;
        currentColor.a = Mathf.Clamp01(minangle - SetAngle(FindAngle.transform.rotation.eulerAngles.y) / 60f);
        ren1.material.color = currentColor;

        currentColor = ren2.material.color;
        currentColor.a = Mathf.Clamp01(60 - minangle - SetAngle(FindAngle.transform.rotation.eulerAngles.y) / 60);
        ren2.material.color = currentColor;

        currentColor = ren3.material.color;
        currentColor.a = 1f;
        ren3.material.color = currentColor;

        currentColor = ren4.material.color;
        currentColor.a = 1f;
        ren4.material.color = currentColor;
    }
    void SetDirection(GameObject dir1, GameObject dir2, GameObject dir3, GameObject dir4)
    {
        dir1.SetActive(true);
        dir2.SetActive(true);
        dir3.SetActive(false);
        dir4.SetActive(false);
    }
    private void SetRotation()
    {
        if (FindAngle.transform.rotation.y < 0)
        {
            Vector3 tempRot = FindAngle.transform.rotation.eulerAngles;
            tempRot.y += 360;
            FindAngle.transform.rotation = Quaternion.Euler(tempRot);
        }
        else if (FindAngle.transform.rotation.y > 360)
        {
            Vector3 tempRot = FindAngle.transform.rotation.eulerAngles;
            tempRot.y -= 360;
            FindAngle.transform.rotation = Quaternion.Euler(tempRot);
        }
    }
    float SetAngle(float temp)
    {
        if (temp < 0) return temp += 360;
        else if (temp > 360) return temp -= 360;
        else return temp;
    }

}