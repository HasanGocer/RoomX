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
    string idleName = "IsIdle", walkName = "IsWalk", turnRightName = "IsTurnRight", turnLeftName = "IsTurnLeft";

    public IEnumerator TurnTargetIEnum(GameObject obj, Vector3 finishPos, int speedFactor, UnityAction FinishFunc)
    {
        StopAllCoroutines();

        float lerpCount = 0;
        GameObject tempObject = new GameObject();
        GameObject finishPosGO = new GameObject();

        SetAngle(ref finishPosGO, ref tempObject, obj, finishPos);
        if (ChangeWay(tempObject.transform, finishPosGO.transform) == Way.left) TurnLeftAnim();
        else TurnRightAnim();


        while (lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, tempObject.transform.rotation, lerpCount);
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
        characterAnim.SetBool(turnRightName, false);
        characterAnim.SetBool(turnLeftName, false);
    }
}
