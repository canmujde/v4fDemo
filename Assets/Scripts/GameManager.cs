using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameState state;

    public static GameManager instance;

    public GameState State { get => state; set => state = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetState(0);
        //Debug.Log("Game Initialized. State is: " + State.ToString());
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void SetState(int index)
    {
        State = (GameState)index;
    }

    public void StartGame()
    {
        if (CheckIfHasHeart())
        {
            SetState(1);
            UIManager.instance.OnPlayingState();
            PlayerStats.instance.DecreaseHeart();
            Invoke(nameof(ActivateInput), 0.5f);
            
        }
        else
        {
            //(reklam izleyip can doldur) ekranını aç!;
            UIManager.instance.OpenAdPanel();


        }
    }

    private void ActivateInput()
    {
        InputManager.instance.CanPlay = true;
    }

    bool CheckIfHasHeart()
    {
        if (PlayerStats.instance.Heart > 0) return true;

        else return false;
    }

    public void GameOver()
    {
        State = GameState.GameOver;
        UIManager.instance.OnGameOverState();
        PlayerStats.instance.SetHighScore();
    }
}

public enum GameState { Prepare, Playing, GameOver }