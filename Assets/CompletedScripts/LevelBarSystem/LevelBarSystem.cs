using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarSystem : MonoSingleton<LevelBarSystem>
{
    //Level artýþýnýn oraný için levelConstant girmeniz ve levelin zorlu logaritmik artmasý için maxXP'yi düzgün ayarlamalýsýnýz.
    [SerializeField] private float levelConstant;
    [SerializeField] private int maxXP;

    [SerializeField] private Image barImage;
    [SerializeField] private Text levelText, nextLevelText;

    int XP;


    public void LevelStart()
    {
        GameManager gameManager = GameManager.Instance;
        for (int i = 1; i < gameManager.GetLevel(); i++)
        {
            maxXP = (int)(maxXP * levelConstant);
        }
        barImage.fillAmount = (float)XP / (float)maxXP;
        levelText.text = gameManager.GetLevel().ToString();
        nextLevelText.text = (gameManager.GetLevel() + 1).ToString();
    }

    public IEnumerator BarLerp(int XPPlus)
    {
        GameManager gameManager = GameManager.Instance;
        float count = 0;
        XP += XPPlus;
        float limit = (float)XP / (float)maxXP;
        yield return null;
        while (true)
        {
            count += Time.deltaTime;
            barImage.fillAmount = Mathf.Lerp(barImage.fillAmount, limit, count);
            yield return new WaitForSeconds(Time.deltaTime);
            if (limit >= barImage.fillAmount)
            {
                LevelUpgradeCheck();
                gameManager.StartPlayerPrefs("XP", ref XP, XP);
                break;
            }
        }
    }

    public void LevelUpgradeCheck()
    {
        GameManager gameManager = GameManager.Instance;
        if (XP >= maxXP)
        {
            maxXP = (int)(maxXP * levelConstant);
            XP = 0;
            gameManager.SetLevel(1);
            Buttons.Instance.SetLevelText();
            nextLevelText.text = (gameManager.GetLevel() + 1).ToString();

        }
    }
}
