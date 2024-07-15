using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //managerde bulunacak

    public enum GameStat
    {
        intro = 0,
        start = 1,
        wait = 2,
        finish = 3
    }


    [Header("Game_Main_Field")]
    [Space(10)]

    [SerializeField] GameStat gameStat;
    public int addedMoney;
    [SerializeField] int level;
    [SerializeField] int money;
    [SerializeField] int vibration;
    [SerializeField] int sound;

    public void Awake()
    {
        PlayerPrefsPlacement();
        ItemData.Instance.AwakeID();
    }

    private void PlayerPrefsPlacement()
    {

        StartPlayerPrefs("money", ref money, 100);
        StartPlayerPrefs("level", ref level, level);
        StartPlayerPrefs("vibration", ref vibration, vibration);
        StartPlayerPrefs("sound", ref sound, sound);

        MoneySystem.Instance.MoneyTextRevork(0);

        if (PlayerPrefs.HasKey("first"))
            resumeGame();
        else
            InitializeGame();
    }

    #region Factor PlayerPrefs
    public void FactorPlacementWrite(ItemData.Field factor)
    {
        string jsonData = JsonUtility.ToJson(factor);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/FactorData.json", jsonData);
    }

    public ItemData.Field FactorPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/FactorData.json");
        ItemData.Field factor = new ItemData.Field();
        factor = JsonUtility.FromJson<ItemData.Field>(jsonRead);
        return factor;
    }
    #endregion

    #region startGame
    void InitializeGame()
    {
        PlayerPrefs.SetInt("first", 1);
        FactorPlacementWrite(ItemData.Instance.factor);
    }

    void resumeGame()
    {
        ItemData.Instance.factor = FactorPlacementRead();
    }
    #endregion

    #region Game Manager Field Func

    public void StartPlayerPrefs(string playerPrefsStringName, ref int playerPrefsRefField, int playerPrefsStartCount = 0)
    {
        if (PlayerPrefs.HasKey(playerPrefsStringName))
            playerPrefsRefField = PlayerPrefs.GetInt(playerPrefsStringName);
        else
            PlayerPrefs.SetInt(playerPrefsStringName, playerPrefsStartCount);
    }

    #endregion

    #region Game Manager Get/Set
    public void SetMoney(int tempMoney)
    {
        PlayerPrefs.SetInt("money", money + tempMoney);
        Buttons.Instance.SetMoneyText();
    }
    public int GetMoney()
    {
        return money;
    }

    public void SetSound(int tempSound)
    {
        PlayerPrefs.SetInt("sound", tempSound);
    }
    public int GetSound()
    {
        return sound;
    }
    public void SetLevel(int tempLevel)
    {
        level++;
        PlayerPrefs.SetInt("level", level + tempLevel);
        Buttons.Instance.SetLevelText();
    }
    public int GetLevel()
    {
        return level;
    }

    public void SetVibration(int tempVib)
    {
        PlayerPrefs.SetInt("vibration", tempVib);
    }
    public int GetVibration()
    {
        return vibration;
    }

    public void SetGameStat(GameStat tempGameStat)
    {
        gameStat = tempGameStat;
    }

    public GameStat GetGameStat()
    {
        return gameStat;
    }
    #endregion
}