using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SeenSystem : MonoSingleton<SeenSystem>
{
    [SerializeField] private float reloadTime;
    Vector3 eyePosition;
    bool isLive;

    public void StartSeen(string targetName, GameObject MainTarget, UnityAction FinishFunc)
    {
        StartCoroutine(SeenRaycast(targetName, MainTarget, FinishFunc));
    }

    public void SetIsLive(bool tempLive)
    {
        isLive = tempLive;
    }
    public bool GetIslive() { return isLive; }

    IEnumerator SeenRaycast(string targetName, GameObject MainTarget, UnityAction FinishFunc)
    {
        SetCameraPosition(MainTarget);

        while (isLive)
        {
            yield return null;

            for (float angle = -150f; angle <= 150f; angle += 5f)
            {
                Vector3 direction = transform.position;
                Quaternion rotation = transform.rotation;
                Vector3 eulerAngles = rotation.eulerAngles;

                float xRad = SeenManager.Instance.GetSeenDistance() * 4 * Mathf.Sin(Mathf.Deg2Rad * eulerAngles.y);
                float yRad = SeenManager.Instance.GetSeenDistance() * 4 * Mathf.Cos(Mathf.Deg2Rad * eulerAngles.y);
                direction += new Vector3(SeenManager.Instance.GetSeenDistance() * Mathf.Sin(angle) + xRad, 0, SeenManager.Instance.GetSeenDistance() * Mathf.Cos(angle) + yRad);

                if (Physics.Raycast(eyePosition, direction, out RaycastHit hitInfo, SeenManager.Instance.GetSeenDistance() * 4))
                {
                    Debug.DrawLine(eyePosition, hitInfo.point, Color.red, 1);
                    if (hitInfo.transform.gameObject.CompareTag(targetName))
                    {
                        FinishFunc();
                        yield return new WaitForSeconds(reloadTime);
                    }
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    void SetCameraPosition(GameObject target)
    {
        Vector3 eyePosition = target.transform.position;
    }
}
