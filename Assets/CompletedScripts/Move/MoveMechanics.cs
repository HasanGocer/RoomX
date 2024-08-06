using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class MoveMechanics : MonoSingleton<MoveMechanics>
{
    public void MoveLerp(GameObject obj, Vector3 finishPos, int speedFactor, ref bool isMove, UnityAction FinishFunc)
    {
        StartCoroutine(MoveLerpIEnum(obj, finishPos, speedFactor, isMove, FinishFunc));
    }
    public void MoveLerp(GameObject obj, Vector3 finishPos, int speedFactor, ref bool isMove)
    {
        StartCoroutine(MoveLerpIEnum(obj, finishPos, speedFactor, isMove));
    }
    public void MoveLerpQuaternion(GameObject obj, Quaternion finishPos, int speedFactor, ref bool isMove)
    {
        StartCoroutine(MoveLerpIEnumQuaternion(obj, finishPos, speedFactor, isMove));
    }
    public void MoveLerpLocalQuaternion(GameObject obj, Quaternion finishPos, int speedFactor, ref bool isMove)
    {
        StartCoroutine(MoveLerpIEnumLocalQuaternion(obj, finishPos, speedFactor, isMove));
    }
    public void MoveStabile(GameObject obj, Vector3 finishPos, int speedFactor, ref bool isMove, UnityAction FinishFunc)
    {
        StartCoroutine(MoveStabileIEnum(obj, finishPos, speedFactor, isMove, FinishFunc));
    }
    public void MoveStabile(GameObject obj, Vector3 finishPos, int speedFactor, ref bool isMove)
    {
        StartCoroutine(MoveStabileIEnum(obj, finishPos, speedFactor, isMove));
    }
    public void MoveUIBar(Image �mage, ref float barFilAmount, int speedFactor, bool isMove, UnityAction FinishFunc)
    {
        StartCoroutine(MoveUIBarIenum(�mage, barFilAmount, speedFactor, isMove, FinishFunc));
    }
    public void MoveUIBar(Image �mage, ref float barFilAmount, int speedFactor, bool isMove)
    {
        StartCoroutine(MoveUIBarIenum(�mage, barFilAmount, speedFactor, isMove));
    }


    private IEnumerator MoveUIBarIenum(Image �mage, float barFilAmount, int speedFactor, bool isMove, UnityAction FinishFunc)
    {
        float lerpCount = 0;
        float startCount = �mage.fillAmount;

        while (isMove && lerpCount < 1)
        {
            lerpCount = Time.deltaTime * speedFactor;
            �mage.fillAmount = Mathf.Lerp(startCount, barFilAmount, lerpCount);
            yield return null;
        }

        FinishFunc();
    }
    private IEnumerator MoveUIBarIenum(Image �mage, float barFilAmount, int speedFactor, bool isMove)
    {
        float lerpCount = 0;
        float startCount = �mage.fillAmount;

        while (isMove && lerpCount < 1)
        {
            lerpCount = Time.deltaTime * speedFactor;
            �mage.fillAmount = Mathf.Lerp(startCount, barFilAmount, lerpCount);
            yield return null;
        }
    }

    private IEnumerator MoveStabileIEnum(GameObject obj, Vector3 finishPos, int speedFactor, bool isMove, UnityAction FinishFunc)
    {
        float lerpCount = 0;
        Vector3 startPos = obj.transform.position;

        while (isMove && lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.position = Vector3.Lerp(startPos, finishPos, lerpCount);
            yield return null;
        }

        FinishFunc();
    }
    private IEnumerator MoveStabileIEnum(GameObject obj, Vector3 finishPos, int speedFactor, bool isMove)
    {
        float lerpCount = 0;
        Vector3 startPos = obj.transform.position;

        while (isMove && lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.position = Vector3.Lerp(startPos, finishPos, lerpCount);
            yield return null;
        }
    }

    private IEnumerator MoveLerpIEnum(GameObject obj, Vector3 finishPos, int speedFactor, bool isMove)
    {
        float lerpCount = 0;

        while (isMove && lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.position = Vector3.Lerp(obj.transform.position, finishPos, lerpCount);
            yield return null;
        }
    }
    private IEnumerator MoveLerpIEnumQuaternion(GameObject obj, Quaternion finishPos, int speedFactor, bool isMove)
    {
        float lerpCount = 0;

        while (isMove && lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, finishPos, lerpCount);
            yield return null;
        }
    }
    private IEnumerator MoveLerpIEnumLocalQuaternion(GameObject obj, Quaternion finishPos, int speedFactor, bool isMove)
    {
        float lerpCount = 0;

        while (isMove && lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, finishPos, lerpCount);
            yield return null;
        }
    }
    private IEnumerator MoveLerpIEnum(GameObject obj, Vector3 finishPos, int speedFactor, bool isMove, UnityAction FinishFunc)
    {
        float lerpCount = 0;

        while (isMove && lerpCount < 1)
        {
            lerpCount += Time.deltaTime * speedFactor;
            obj.transform.position = Vector3.Lerp(obj.transform.position, finishPos, lerpCount);
            yield return null;
        }

        FinishFunc();
    }

}
