using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnim : MonoSingleton<CharacterAnim>
{
    private enum Way
    {
        left,
        right
    }


    [SerializeField] Animator characterAnim;
    string idleName = "IsIdle", walkName = "IsWalk", turnRightName = "IsTurnRight", turnLeftName = "IsTurnLeft", PickUpName = "IsPickUp", GateOpenName = "IsGateOpen", GateCloseName = "IsGateClose";

    public IEnumerator TurnTargetIEnum(GameObject obj, Vector3 finishPos, float speedFactor, UnityAction FinishFunc)
    {
        StopAllCoroutines();

        float lerpCount = 0;
        GameObject tempObject = new GameObject();
        GameObject finishPosGO = new GameObject();

        SetAngle(ref finishPosGO, ref tempObject, obj, finishPos);
        if (ChangeWay(tempObject.transform, finishPosGO.transform) == Way.left) TurnLeftAnim();
        else TurnRightAnim();

        Quaternion startRotation = Quaternion.Euler(0, obj.transform.rotation.eulerAngles.y, 0);
        Quaternion targetRotation = Quaternion.Euler(0, tempObject.transform.rotation.eulerAngles.y, 0);

        while (lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, lerpCount);
            yield return null;
        }

        finishPosGO.SetActive(false);
        tempObject.SetActive(false);

        FinishFunc();
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
        characterAnim.SetBool(PickUpName, true);
        characterAnim.SetBool(idleName, true);
    }
    public void GateOpenAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(GateOpenName, true);
        characterAnim.SetBool(idleName, true);
    }
    public void GateCloseAnim()
    {
        AllAnimOff();
        characterAnim.SetBool(GateCloseName, true);
        characterAnim.SetBool(idleName, true);
    }

     private void TurnLeftAnim()
     {
         AllAnimOff();
         characterAnim.SetBool(turnLeftName, true);
        characterAnim.SetBool(walkName, true);
     }
     private void TurnRightAnim()
     {
         AllAnimOff();
         characterAnim.SetBool(turnRightName, true);
        characterAnim.SetBool(walkName, true);
     }

    private void AllAnimOff()
    {
        characterAnim.SetBool(idleName, false);
        characterAnim.SetBool(walkName, false);
        characterAnim.SetBool(PickUpName, false);
        characterAnim.SetBool(turnRightName, false);
        characterAnim.SetBool(turnLeftName, false);
        characterAnim.SetBool(GateCloseName, false);
        characterAnim.SetBool(GateOpenName, false);
    }
}
