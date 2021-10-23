using UnityEngine;

public class Game_Controller : MonoBehaviour

{
    public static Game_Controller GameControlerStatic;
    public float scoreMultiplayer;
    public bool _alive;

    [SerializeField] private UI_Controller _UIController;
    [SerializeField] private GameObject _PausePanel;
    private float _score;

    public void GameOver()
    {        
        _alive = false;
        ScoreRecorder((int)_score);
        Time.timeScale = 0;
    }
    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            _PausePanel.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            _PausePanel.SetActive(false);
        }
    }

    private void Start()
    {
        Initializations();
    }

    private void Update()
    {
        ScoreCount();
        Timer();
    }
    private void Initializations()
    {
        GameControlerStatic = this;
        _alive = true;
    }

    private void ScoreCount()
    {
        if (_alive == true)
        {
            _score += Time.deltaTime * scoreMultiplayer;
            _UIController.ScoreHUDUpdate((int)_score);
        }
    }

    private void Timer()
    {
        _UIController.TimerCount();
    }    

    private void ScoreRecorder(int currentScore)
    {

    }
}
