using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoSingleton<PaintingManager>
{
    [System.Serializable]
    public class PaintingsClass
    {
        public List<GameObject> paintingObjects = new List<GameObject>();
        public List<bool> paintingBools = new List<bool>();
    }

    [SerializeField] PaintingsClass paintingsClass;

    private void Start()
    {
        StartPaintings();
    }

    private void StartPaintings()
    {
        if (PlayerPrefs.HasKey("firstPaintings")) PaintingsClassPlacementRead();
        else
        {
            PlayerPrefs.SetInt("firstPaintings", 1);
            PaintingsClassPlacementWrite();
        }
    }

    public bool CheckPanitngs(GameObject painting)
    {
        bool tempBool = false;

        for (int i = 0; i < paintingsClass.paintingObjects.Count; i++)
            if (paintingsClass.paintingObjects[i] == painting && paintingsClass.paintingBools[i]) tempBool = true;
        return tempBool;
    }

    public void PaintingAdd(GameObject painting)
    {
        for (int i = 0; i < paintingsClass.paintingObjects.Count; i++)
            if (paintingsClass.paintingObjects[i] == painting) paintingsClass.paintingBools[i] = true;
        PaintingsClassPlacementWrite();
    }

    private void PaintingsClassPlacementWrite()
    {
        string jsonData = JsonUtility.ToJson(paintingsClass);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/paintingsClassData.json", jsonData);
    }

    private void PaintingsClassPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/paintingsClassData.json");
        paintingsClass = JsonUtility.FromJson<PaintingsClass>(jsonRead);
    }
}
