using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("MenuTransforms")]
    public Transform MainMenuTransform;
    public Transform LevelMenuTransform;
    public Transform LevelCompleateTransform;
    public Transform PauseMenuTransform;
    [Space]
    [Header("Canvases")]
    public GameObject MenuCanvas;
    public GameObject GameCanvas;
    [Space]
    [SerializeField]
    private LevelButtonsUI _levelButtonsUI;
    [SerializeField]
    private LevelCompleateUI _levelCompleateUI; 
    private void Start()
    {
        OpenMainMenu();
    }
    private void OnEnable()
    {
        _levelButtonsUI.OnRequestLoadLevel += _levelButtonsUI_OnRequestLoadLevel;
        LevelController.Instance.OnLevelCleared += LevelController_OnLevelCleared;
    }
    private void OnDisable()
    {
        _levelButtonsUI.OnRequestLoadLevel -= _levelButtonsUI_OnRequestLoadLevel;
        LevelController.Instance.OnLevelCleared -= LevelController_OnLevelCleared;
    }
    private void LevelController_OnLevelCleared(float time)
    {
        GameCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
        OpenVictoryScreen();
        _levelCompleateUI.LevelCompleated(time);
    }
    public void OpenLevelMenu()
    {
        LevelMenuTransform.gameObject.SetActive(true);
        MainMenuTransform.gameObject.SetActive(false);
        LevelCompleateTransform.gameObject.SetActive(false);
        PauseMenuTransform.gameObject.SetActive(false);
    }
    public void OpenMainMenu()
    {
        MainMenuTransform.gameObject.SetActive(true);
        LevelMenuTransform.gameObject.SetActive(false);
        LevelCompleateTransform.gameObject.SetActive(false);
        PauseMenuTransform.gameObject.SetActive(false);
    }
    public void OpenVictoryScreen()
    {
        LevelCompleateTransform.gameObject.SetActive(true);
        MainMenuTransform.gameObject.SetActive(false);
        LevelMenuTransform.gameObject.SetActive(false);
        PauseMenuTransform.gameObject.SetActive(false);
    }
    public void OpenPauseMenu()
    {
        LevelController.Instance.PauseGame();

        GameCanvas.SetActive(false);
        MenuCanvas.SetActive(true);

        LevelCompleateTransform.gameObject.SetActive(false);
        MainMenuTransform.gameObject.SetActive(false);
        LevelMenuTransform.gameObject.SetActive(false);
        PauseMenuTransform.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        LevelController.Instance.ResumeGame();
        GameCanvas.SetActive(true);
        MenuCanvas.SetActive(false);

        LevelCompleateTransform.gameObject.SetActive(false);
        MainMenuTransform.gameObject.SetActive(false);
        LevelMenuTransform.gameObject.SetActive(false);
        PauseMenuTransform.gameObject.SetActive(false);
    }
    public void LoadNextLevel()
    {
        MenuCanvas.SetActive(false);
        GameCanvas.SetActive(true);

        if (!LevelController.Instance.LoadNextLevel())
        {
            MenuCanvas.SetActive(true);
            GameCanvas.SetActive(false);
            OpenLevelMenu();
        }
    }
    private void _levelButtonsUI_OnRequestLoadLevel(int levelNum)
    {
        MenuCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        if (!LevelController.Instance.LoadLevel(levelNum))
        {
            MenuCanvas.SetActive(true);
            GameCanvas.SetActive(false);
            OpenLevelMenu();
        }
    }
    public void CloseApp()
    {
        Application.Quit();
    }
}
