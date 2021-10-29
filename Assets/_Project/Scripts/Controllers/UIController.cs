using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private string _TextScore;
    [SerializeField] private string _TextTimer;
    [SerializeField] private TextMeshProUGUI _HUDScore;
    [SerializeField] private TextMeshProUGUI _HUDTimer;
    [SerializeField] private TextMeshProUGUI _HUDHighScore;
    [SerializeField] private GameObject _GameOverPanel;
    [SerializeField] private GameObject[] _PausePanels = new GameObject[3];
    [SerializeField] private LeaderboardController _lead;

    public void Initializations()
    {
        Debug.Log("UIController");

        GameController.instance.OnGameOver += CallGameOver;
        GameController.instance.OnGamePause += CallPausePanels;
        GameController.instance.OnGameUnpause += DisablePausePanels;        
    }
    public void ScoreHUDUpdate(int score)
    {        
        _TextScore = score.ToString();
        _HUDScore.text = _TextScore;
        _HUDHighScore.text = _lead.leaderboardScores[0].ToString();        

        if(_lead.leaderboardScores[0] < score)
        {
            _HUDHighScore.text = score.ToString();
            return;
        }
    }

    public void TimerCount()
    {
        int minutes = (int)(Time.timeSinceLevelLoad / 60);
        int segunds = (int)(Time.timeSinceLevelLoad % 60);

        if (GameController.instance.playerAlive == true)
        {
            _TextTimer = TimeSpan.FromMinutes(minutes).ToString("mm") + ":" + TimeSpan.FromSeconds(segunds).ToString("ss");
            _HUDTimer.text = _TextTimer;
        }
    }

    public void CloseAplication()
    {
        Application.Quit();
    }    

    private void Start()
    {
        Initializations();
    }

    private void CallGameOver()
    {
        _GameOverPanel.SetActive(true);
    }

    private void CallPausePanels()
    {
        _PausePanels[0].SetActive(true);
        _PausePanels[1].SetActive(true);
        _PausePanels[2].SetActive(false);
    }

    private void DisablePausePanels()
    {
        _PausePanels[0].SetActive(false);
        _PausePanels[1].SetActive(false);
        _PausePanels[2].SetActive(true);
    }
    private void OnDisable()
    {
        GameController.instance.OnGameOver -= CallGameOver;
        GameController.instance.OnGamePause -= CallPausePanels;
        GameController.instance.OnGameUnpause -= DisablePausePanels;
    }
}