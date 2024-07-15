using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public float temp;
        public int tempInt;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field fieldPrice;

    public void AwakeID()
    {

        SetField(ref field.temp, ref standart.temp, ref factor.temp, ref maxFactor.temp, ref constant.temp, ref fieldPrice.temp);
        SetField(ref field.tempInt, ref standart.tempInt, ref factor.tempInt, ref maxFactor.tempInt, ref constant.tempInt, ref fieldPrice.tempInt);
        StartCoroutine(Buttons.Instance.LoadingScreen());
    }

    void SetField(ref float tempField, ref float tempStandart, ref float tempFactor, ref float tempMaxFactor, ref float tempConstant, ref float tempFieldPrice)
    {
        tempField = tempStandart + (tempFactor * tempConstant);
        tempFieldPrice = tempFieldPrice * tempFactor;

        if (tempFactor > tempMaxFactor)
        {
            tempFactor = tempMaxFactor;
            tempField = tempStandart + (tempFactor * tempConstant);
            tempFieldPrice = tempFieldPrice / (tempFactor - 1);
            tempFieldPrice = tempFieldPrice * tempFactor;
        }
    }
    void SetField(ref int tempField, ref int tempStandart, ref int tempFactor, ref int tempMaxFactor, ref int tempConstant, ref int tempFieldPrice)
    {
        tempField = tempStandart + (tempFactor * tempConstant);
        tempFieldPrice = tempFieldPrice * tempFactor;

        if (tempFactor > tempMaxFactor)
        {
            tempFactor = tempMaxFactor;
            tempField = tempStandart + (tempFactor * tempConstant);
            tempFieldPrice = tempFieldPrice / (tempFactor - 1);
            tempFieldPrice = tempFieldPrice * tempFactor;
        }
    }

    public void UpdateField(ref float tempField, ref float tempStandart, ref float tempFactor, ref float tempMaxFactor, ref float tempConstant, ref float tempFieldPrice)
    {
        tempFieldPrice = tempFieldPrice / tempFactor;

        tempField++;
        tempField = tempStandart + (tempFactor * tempConstant);
        tempFieldPrice = tempFieldPrice * tempFactor;

        if (tempFactor > tempMaxFactor)
        {
            tempFactor = tempMaxFactor;
            tempField = tempStandart + (tempFactor * tempConstant);
            tempFieldPrice = tempFieldPrice / (tempFactor - 1);
            tempFieldPrice = tempFieldPrice * tempFactor;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void UpdateField(ref int tempField, ref int tempStandart, ref int tempFactor, ref int tempMaxFactor, ref int tempConstant, ref int tempFieldPrice)
    {
        tempFieldPrice = tempFieldPrice / tempFactor;

        tempField++;
        tempField = tempStandart + (tempFactor * tempConstant);
        tempFieldPrice = tempFieldPrice * tempFactor;

        if (tempFactor > tempMaxFactor)
        {
            tempFactor = tempMaxFactor;
            tempField = tempStandart + (tempFactor * tempConstant);
            tempFieldPrice = tempFieldPrice / (tempFactor - 1);
            tempFieldPrice = tempFieldPrice * tempFactor;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
