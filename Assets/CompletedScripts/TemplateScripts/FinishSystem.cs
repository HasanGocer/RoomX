using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    public void FinishCheck()
    {
        if (GameManager.Instance.GetGameStat() == GameManager.GameStat.start)
            FinishTime();
    }
    public void FinishTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;

        gameManager.SetLevel(1);
        buttons.OpenBarPanel();
        buttons.SetFinishGameMoneyText(gameManager.addedMoney);
        gameManager.SetGameStat(GameManager.GameStat.finish);
    }
}
