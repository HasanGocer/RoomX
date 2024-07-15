using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak
    [Header("Global_Panel")]
    [Space(10)]

    [SerializeField] private GameObject globalPanel;
    [SerializeField] TMP_Text moneyText, levelText;

    [Header("Start_Panel")]
    [Space(10)]

    [SerializeField] GameObject startPanel;
    [SerializeField] Button startButton;

    [Header("Setting_Panel")]
    [Space(10)]

    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject settingGame;

    [SerializeField] private GameObject soundOn, soundOff;
    [SerializeField] private GameObject vibrationOn, vibrationOff;
    [SerializeField] private Button settingBackButton;
    [SerializeField] private Button soundButton, vibrationButton;

    [Header("Finish_Panel")]
    [Space(10)]

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject barPanel;
    [SerializeField] private Button winPrizeButton, winEmptyButton, failButton;
    [SerializeField] int finishWaitTime;

    [SerializeField] TMP_Text finishGameMoneyText;

    [Header("Loading_Panel")]
    [Space(10)]

    [SerializeField] GameObject loadingPanel;
    [SerializeField] int loadingScreenCountdownTime;
    [SerializeField] int startSceneCount;

    private void Start()
    {
        ButtonPlacement();
        SettingPlacement();
        levelText.text = GameManager.Instance.GetLevel().ToString();
    }

    public IEnumerator LoadingScreen()
    {

        loadingPanel.SetActive(true);
        globalPanel.SetActive(false);
        startPanel.SetActive(false);
        yield return new WaitForSeconds(loadingScreenCountdownTime);
        loadingPanel.SetActive(false);
        globalPanel.SetActive(true);
        startPanel.SetActive(true);
    }

    public void SetMoneyText()
    {
        moneyText.text = GameManager.Instance.GetMoney().ToString();
    }
    public void SetLevelText()
    {
        levelText.text = GameManager.Instance.GetLevel().ToString();

    }
    public void OpenBarPanel()
    {
        barPanel.SetActive(true);
    }
    public void SetFinishGameMoneyText(int tempCount)
    {
        finishGameMoneyText.text = tempCount.ToString();
    }
    public TMP_Text GetMoneyText() { return moneyText; }


    private void ButtonPlacement()
    {
        settingButton.onClick.AddListener(SettingButton);
        settingBackButton.onClick.AddListener(SettingBackButton);
        soundButton.onClick.AddListener(SoundButton);
        vibrationButton.onClick.AddListener(VibrationButton);
        winPrizeButton.onClick.AddListener(() => StartCoroutine(WinPrizeButton()));
        winEmptyButton.onClick.AddListener(() => StartCoroutine(WinButton()));
        failButton.onClick.AddListener(() => StartCoroutine(FailButton()));
        startButton.onClick.AddListener(StartButton);
    }
    private void SettingPlacement()
    {
        SoundSystem soundSystem = SoundSystem.Instance;
        GameManager gameManager = GameManager.Instance;

        if (gameManager.GetSound() == 1)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            soundSystem.MainMusicPlay();
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            soundSystem.MainMusicStop();
        }

        if (gameManager.GetVibration() == 1)
        {
            vibrationOn.SetActive(true);
            vibrationOff.SetActive(false);
        }
        else
        {
            vibrationOn.SetActive(false);
            vibrationOff.SetActive(true);
        }
    }

    private void SettingButton()
    {
        if (GameManager.Instance.GetGameStat() != GameManager.GameStat.finish)
        {
            startPanel.SetActive(false);
            settingGame.SetActive(true);
            settingButton.gameObject.SetActive(false);
            globalPanel.SetActive(false);
        }
    }
    private void SettingBackButton()
    {
        if (GameManager.Instance.GetGameStat() == GameManager.GameStat.intro)
            startPanel.SetActive(true);
        settingGame.SetActive(false);
        settingButton.gameObject.SetActive(true);
        globalPanel.SetActive(true);
    }
    private void SoundButton()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.GetSound() == 1)
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            SoundSystem.Instance.MainMusicStop();
            gameManager.SetSound(0);
        }
        else
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            SoundSystem.Instance.MainMusicPlay();
            gameManager.SetSound(1);
        }
    }
    private void VibrationButton()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.GetVibration() == 1)
        {
            vibrationOff.SetActive(true);
            vibrationOn.SetActive(false);
            soundOn.SetActive(false);
            gameManager.SetVibration(0);
        }
        else
        {
            vibrationOff.SetActive(false);
            vibrationOn.SetActive(true);
            gameManager.SetVibration(1);
        }
    }
    private IEnumerator WinPrizeButton()
    {
        GameManager gameManager = GameManager.Instance;

        winPrizeButton.enabled = false;
        BarSystem.Instance.BarStopButton(gameManager.addedMoney);
        yield return new WaitForSeconds(finishWaitTime);

        gameManager.SetLevel(1);

        SceneManager.LoadScene(startSceneCount);
    }
    private IEnumerator WinButton()
    {
        GameManager gameManager = GameManager.Instance;

        winEmptyButton.enabled = false;
        gameManager.SetLevel(1);
        BarSystem.Instance.BarStopButton(0);
        MoneySystem.Instance.MoneyTextRevork(gameManager.addedMoney);
        yield return new WaitForSeconds(finishWaitTime);

        SceneManager.LoadScene(startSceneCount);
    }
    private IEnumerator FailButton()
    {
        MoneySystem.Instance.MoneyTextRevork(GameManager.Instance.addedMoney);
        yield return new WaitForSeconds(finishWaitTime);

        SceneManager.LoadScene(startSceneCount);
    }
    private void StartButton()
    {
        GameManager.Instance.SetGameStat(GameManager.GameStat.start);
        startPanel.SetActive(false);
    }
}
