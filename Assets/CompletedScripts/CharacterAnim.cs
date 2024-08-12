using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations.Rigging;

public class CharacterAnim : MonoSingleton<CharacterAnim>
{
    private enum Way
    {
        left,
        right
    }


    [SerializeField] Animator characterAnim;
    string idleName = "IsIdle", walkName = "IsWalk", turnRightName = "IsTurnRight", turnLeftName = "IsTurnLeft", pickUpName = "IsPickUp", doorInName = "IsDoorIn", doorOutName = "IsDoorOut";
    [SerializeField] GameObject pickUpTarget;
    [SerializeField] GameObject pickUpTargetPos;
    [SerializeField] float HandIKSpeed;

    public IEnumerator TurnTargetIEnum(GameObject obj, Vector3 finishPos, float speedFactor, UnityAction FinishFunc)
    {
        StopAllCoroutines();

        float lerpCount = 0;
        GameObject tempObject = new GameObject();
        GameObject finishPosGO = new GameObject();

        SetAngle(ref finishPosGO, ref tempObject, obj, finishPos);
        Way turnDirection = ChangeWay(tempObject.transform, finishPosGO.transform);

        if (turnDirection == Way.left)
            TurnLeftAnim();
        else
            TurnRightAnim();

        Quaternion startRotation = Quaternion.Euler(0, obj.transform.rotation.eulerAngles.y, 0);
        Quaternion targetRotation = Quaternion.Euler(0, tempObject.transform.rotation.eulerAngles.y, 0);

        float angleDifference = Quaternion.Angle(startRotation, targetRotation);

        float animationDuration = angleDifference / (80 * speedFactor);

        characterAnim.speed = 1 / animationDuration;

        while (lerpCount <= 1)
        {
            lerpCount += Time.deltaTime / animationDuration;
            obj.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, lerpCount);
            yield return null;
        }

        characterAnim.speed = 1;

        finishPosGO.SetActive(false);
        tempObject.SetActive(false);

        WalkAnim();
        FinishFunc();
    }

    public void PickUpTargetMove(GameObject target)
    {
        bool tempBool = true;

        MoveMechanics.Instance.MoveStabile(pickUpTarget, target.transform.position, HandIKSpeed, ref tempBool);
    }
    public void ResetPickUpTarget()
    {
        pickUpTarget.transform.position = pickUpTargetPos.transform.position;
        pickUpTarget.transform.rotation = pickUpTargetPos.transform.rotation;
    }


    private Way ChangeWay(Transform objectA, Transform objectB)
    {
        Vector3 directionToB = objectB.position - objectA.position;
        Vector3 crossProduct = Vector3.Cross(transform.forward, directionToB);

        if (crossProduct.y > 0)
            return Way.left;
        else
            return Way.right;
    }
    private void SetAngle(ref GameObject finishPosGO, ref GameObject tempObject, GameObject obj, Vector3 finishPos)
    {
        finishPosGO.transform.position = finishPos;
        tempObject.transform.position = obj.transform.position;
        tempObject.transform.LookAt(finishPosGO.transform);
        tempObject.transform.position = new Vector3(tempObject.transform.position.x, obj.transform.position.y, tempObject.transform.position.z);
    }

    public void IdleAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(idleName, true);
    }
    public void WalkAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(walkName, true);
    }
    public void PickUpAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(pickUpName, true);
        characterAnim.SetBool(idleName, true);
    }
    public void DoorInAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(doorInName, true);
        characterAnim.SetBool(idleName, true);
    }
    public void DoorOutAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(doorOutName, true);
        characterAnim.SetBool(idleName, true);
    }
    private void TurnLeftAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(turnLeftName, true);
    }
    private void TurnRightAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(turnRightName, true);
    }

    private void AllAnimOff()
    {
        characterAnim.SetBool(idleName, false);
        characterAnim.SetBool(walkName, false);
        characterAnim.SetBool(pickUpName, false);
        characterAnim.SetBool(turnRightName, false);
        characterAnim.SetBool(turnLeftName, false);
        characterAnim.SetBool(doorInName, false);
        characterAnim.SetBool(doorOutName, false);
    }
}
