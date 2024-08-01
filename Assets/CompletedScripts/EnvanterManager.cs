using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvanterManager : MonoSingleton<EnvanterManager>
{
    [System.Serializable]
    public class Envanter
    {
        public List<bool> itemChecked = new List<bool>();
        public List<string> itemName = new List<string>();
        public List<int> itemID = new List<int>();
        public List<Sprite> itemImage = new List<Sprite>();
    }
    [System.Serializable]
    public class EnvanterUI
    {
        public List<Image> itemImage = new List<Image>();
        public List<bool> itemChecked = new List<bool>();
    }

    public Envanter envanter;
    public EnvanterUI envanterUI;
    [SerializeField] Sprite blackSprite;

    private void Start()
    {
        StartEnvanter();
        StartEnvanterUI();
    }

    private void StartEnvanter()
    {
        if (PlayerPrefs.HasKey("first")) EnvanterPlacementRead();
        else
        {
            PlayerPrefs.SetInt("first", 1);
            EnvanterPlacementWrite();
        }
    }

    private void StartEnvanterUI()
    {
        for (int i = 0; i < envanterUI.itemImage.Count; i++)
        {
            envanterUI.itemImage[i].sprite = blackSprite;
            envanterUI.itemChecked[i] = false;
        }

        int imageCount = 0;
        for (int i = 0; i < envanter.itemChecked.Count; i++)
            if (envanter.itemChecked[i])
            {
                envanterUI.itemImage[imageCount].sprite = envanter.itemImage[i];
                envanterUI.itemChecked[imageCount] = true;
            }
    }

    public void ItemAdd(InteractiveID interactiveID)
    {
        envanter.itemChecked[interactiveID.GetInteractiveID()] = true;
        StartEnvanterUI();
        EnvanterPlacementWrite();
    }
    public void ItemUsed(int itemID)
    {
        foreach (int item in envanter.itemID)
            if (itemID == item)
                envanter.itemChecked[item] = false;
        StartEnvanterUI();
        EnvanterPlacementWrite();
    }

    private void EnvanterPlacementWrite()
    {
        string jsonData = JsonUtility.ToJson(envanter);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/EnvanterData.json", jsonData);
    }

    private void EnvanterPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/EnvanterData.json");
        envanter = JsonUtility.FromJson<Envanter>(jsonRead);
    }
}
