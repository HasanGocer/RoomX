using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoSingleton<MoneySystem>
{
    private int _billion = 1000000000, _million = 1000000, _thousand = 1000;

    public void MoneyTextRevork(int plus)
    {
        GameManager.Instance.SetMoney(plus);

        if (GameManager.Instance.GetMoney() >= _billion)
        {
            float money = GameManager.Instance.GetMoney() / _billion;
            Buttons.Instance.GetMoneyText().text = money.ToString() + " B";
        }
        else if (GameManager.Instance.GetMoney() >= _million)
        {
            float money = GameManager.Instance.GetMoney() / _million;
            Buttons.Instance.GetMoneyText().text = money.ToString() + " M";
        }
        else if (GameManager.Instance.GetMoney() >= _thousand)
        {
            float money = GameManager.Instance.GetMoney() / _thousand;
            Buttons.Instance.GetMoneyText().text = money.ToString() + " K";
        }
        else
        {
            Buttons.Instance.GetMoneyText().text = GameManager.Instance.GetMoney().ToString();
        }
    }

    public string NumberTextRevork(int number)
    {
        string numberString;

        if (number >= _billion)
        {
            int money = number / _billion;
            float Floatmoney = (float)money / 10;
            numberString = Floatmoney.ToString() + " B";
        }
        else if (number >= _million)
        {
            int money = number / _million;
            float Floatmoney = (float)money / 10;
            numberString = Floatmoney.ToString() + " M";
        }
        else if (number >= _thousand)
        {
            int money = number / _thousand;
            float Floatmoney = (float)money / 10;
            numberString = Floatmoney.ToString() + " K";
        }
        else
        {
            numberString = number.ToString();
        }
        return numberString;
    }
}
