using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Text[] highScores;
    [SerializeField] private Text[] currentScores;
    [SerializeField] private Text hearts;

    [SerializeField] private GameObject adPanel;

    [SerializeField] private Slider bar;

    [SerializeField] private Page[] pages;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnPrepareState();
    }

    private void Update()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        bar.value = PlayerStats.instance.CurrentScore;
    }

    public void OnPrepareState()
    {
        pages[0].Open();
        pages[1].Close();
        pages[2].Close();
    }

    public void OnPlayingState()
    {
        pages[0].Close();
        pages[1].Open();
        pages[2].Close();
    }

    

    public void OnGameOverState()
    {
        pages[0].Close();
        pages[1].Close();
        pages[2].Open();
    }

    

    public void UpdateScores()
    {
        UpdateBestScore();
        UpdateCurrentScore();
    }
    public void UpdateAll()
    {
        UpdateHearts();
        UpdateBestScore();
        UpdateCurrentScore();
    }
    public void UpdateHearts()
    {
        hearts.text = PlayerStats.instance.Heart.ToString();
    }
    public void UpdateBestScore()
    {
        foreach (Text highScore in highScores)
        {
            highScore.text = "BEST\n" + PlayerStats.instance.HighScore;
        }
    }
    public void UpdateCurrentScore()
    {
        foreach (Text currentScore in currentScores)
        {
            currentScore.text = PlayerStats.instance.CurrentScore.ToString();
        }
    }
    public void OpenAdPanel()
    {
        adPanel.SetActive(true);
    }
    public void CloseAdPanel()
    {
        adPanel.SetActive(false);
    }
}
