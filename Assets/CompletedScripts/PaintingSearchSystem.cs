using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSearchSystem : MonoSingleton<PaintingSearchSystem>
{
    [SerializeField] GameObject _camera;
    [SerializeField] RectTransform targetObject;
    [SerializeField] float scaleDistance = 1f;
    [SerializeField] float minScale = 0.5f;
    [SerializeField] float maxScale = 1;

    public float GetScaleDistance() { return scaleDistance; }
    public RectTransform GetTargetObject() { return targetObject; }
    public float GetMinScale() { return minScale; }
    public float GetMaxScale() { return maxScale; }
    public void targetObjectOn() { targetObject.gameObject.SetActive(true); }
    public void targetObjectOff() { targetObject.gameObject.SetActive(false); }

    public void StartPaintingSearch(GameObject target)
    {
        FollowMouseAndScale followMouseAndScale = target.GetComponent<FollowMouseAndScale>();
        bool tempBool = true;

        CharacterAnim.Instance.IdleAnim();
        SetCamera(followMouseAndScale, false, true, false);


        if (PaintingManager.Instance.CheckPanitngs(target))
            PaintingCompetedSystem.Instance.ButtonActive();
        else
        {
            WorldSpaceButtonClick.Instance.enabled = true;
            followMouseAndScale.enabled = true;
        }

        MoveMechanics.Instance.MoveLerpQuaternion(_camera, followMouseAndScale.GetCameraPos().transform.rotation, 1, ref tempBool);
        MoveMechanics.Instance.MoveLerp(_camera, followMouseAndScale.GetCameraPos().transform.position, 1, ref tempBool);

    }

    public void FinishPaintingSearch(GameObject target)
    {
        FollowMouseAndScale followMouseAndScale = target.GetComponent<FollowMouseAndScale>();

        SetCamera(followMouseAndScale, true, false, true);

        WorldSpaceButtonClick.Instance.enabled = false;
        PaintingManager.Instance.PaintingAdd(target);

        followMouseAndScale.enabled = false;
    }

    private void SetCamera(FollowMouseAndScale followMouseAndScale, bool cameraTargetFollow, bool cameraController, bool characterMove)
    {
        CameraTargetFollow.Instance.enabled = cameraTargetFollow;
        CameraController.Instance.enabled = cameraController;
        CharacterMove.Instance.gameObject.SetActive(characterMove);

        CameraController.Instance.SetWorldSpaceCanvas(followMouseAndScale.GetCanvas());
    }
}
