using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSystem : MonoSingleton<BarSystem>
{
    //sa�a sola giden bir bar sistemi

    [SerializeField] private Image barImage;
    [SerializeField] bool isFinish = true;
    private bool goRight = true;
    private int barMoneyFactor;
    [SerializeField] private GameObject startPos, finishPos;
    private float amount = 0;

    // B�l�m oranlar�n� ve onlara kar��l�k gelen �arpanlar� i�eren bir liste tan�mlayal�m
    private List<int> sectionFactors = new List<int>() { 1, 2, 3, 2, 1 };

    public void BarStopButton(int count)
    {
        isFinish = false;
        BarFactorPlacement(amount);
        MoneySystem.Instance.MoneyTextRevork(count * barMoneyFactor);
    }

    public IEnumerator BarImageFillAmountIenum()
    {
        isFinish = true;
        while (isFinish)
        {
            amount += Time.deltaTime;
            Vector2 startPosition = goRight ? startPos.transform.position : finishPos.transform.position;
            Vector2 endPosition = goRight ? finishPos.transform.position : startPos.transform.position;

            barImage.transform.position = Vector2.Lerp(startPosition, endPosition, amount);

            if (amount >= 1)
            {
                amount = 0;
                goRight = !goRight;
            }

            yield return null;
        }
    }

    private void BarFactorPlacement(float barAmount)
    {
        float sectionSize = 1.0f / sectionFactors.Count;
        int sectionIndex = Mathf.FloorToInt(barAmount / sectionSize);
        barMoneyFactor = sectionFactors[sectionIndex];
    }
}
