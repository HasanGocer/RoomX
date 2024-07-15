using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblingSystem : MonoSingleton<GamblingSystem>
{

    public float auctionTime;

    [System.Serializable]
    public class Determination
    {
        public int up, stay, down;
    }

    //baþlangýç oraný ve kumara devam ettiði sürece devam etme kalma veya kaybetme oranýný belirleyin.
    [SerializeField] Determination determination;
    [SerializeField] Determination upperDetermination;
    int GambleCount;
    int choose = 0;

    public void GambleStart()
    {
        int GambleCount = 1;

        Gamble();
    }

    public void SetChoseCount(int tempchose)
    {
        if (tempchose == 1) choose = 1;
        else choose = 2;
    }

    IEnumerator Gamble()
    {
        int limit = Random.Range(0, (determination.up + determination.down + determination.stay));

        yield return new WaitForSeconds(auctionTime);
        if (limit <= determination.down)
        {
            GambleCount = 0;
            CounterFinish();
        }
        else if (limit <= (determination.stay + determination.down))
        {
            CounterFinish();
        }
        else
        {
            GambleCount++;
            choose = 0;
            ChangeChoose();
            StartCoroutine(GambleChose());
        }
    }

    IEnumerator GambleChose()
    {
        yield return null;

        while (true)
        {
            if (choose == 1)
            {
                Gamble();
                break;
            }
            if (choose == 2)
            {
                CounterFinish();
                break;
            }
            yield return null;
        }

    }
    void ChangeChoose()
    {
        determination.up += upperDetermination.up;
        determination.down += upperDetermination.down;
        determination.stay += upperDetermination.stay;
    }

    void CounterFinish()
    {
        GameManager.Instance.SetMoney(GameManager.Instance.addedMoney * GambleCount);
        Buttons.Instance.GetMoneyText();
        GameManager.Instance.addedMoney = 0;
    }
}
