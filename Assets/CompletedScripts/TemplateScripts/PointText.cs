using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DamageNumbersPro;

public class PointText : MonoSingleton<PointText>
{
    [Header("Text_Field")]
    [Space(10)]

    [SerializeField] private int OPRedTextCount;
    [SerializeField] private int OPGreenTextCount;
    [SerializeField] private float textMoveTime;

    public void CallRedText(GameObject Pos, int count)
    {
        StartCoroutine(CallPointRedText(Pos, count));
    }

    public void CallGreenText(GameObject Pos, int count)
    {
        StartCoroutine(CallPointGreenText(Pos, count));
    }

    private IEnumerator CallPointRedText(GameObject Pos, int count)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(OPRedTextCount);

        obj.GetComponent<DamageNumberMesh>().leftText = "% " + count;
        obj.transform.position = Pos.transform.position;
        yield return new WaitForSeconds(textMoveTime);
        ObjectPool.Instance.AddObject(OPRedTextCount, obj);
    }
    private IEnumerator CallPointGreenText(GameObject Pos, int count)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(OPGreenTextCount);

        obj.GetComponent<DamageNumberMesh>().leftText = "% " + count;
        obj.transform.position = Pos.transform.position;
        yield return new WaitForSeconds(textMoveTime);
        ObjectPool.Instance.AddObject(OPGreenTextCount, obj);
    }
}
