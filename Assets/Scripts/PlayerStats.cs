using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;
    [SerializeField] private int heart;

    [SerializeField] private float currentBar;
    [SerializeField] private float maxBar = 100;
    [SerializeField] private int level;

    public static PlayerStats instance;

    public int HighScore { get => highScore; set => highScore = value; }
    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public int Heart { get => heart; set => heart = value; }
    public float CurrentBar { get => currentBar;  }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateBar();
        
    }

    

    private void UpdateBar()
    {
        currentBar = Mathf.Lerp(currentBar, currentScore, Time.deltaTime * 5);

        
    }

    private void Init()
    {
        CallFromPlayerPrefs();
        ResetBar();
        UIManager.instance.UpdateAll();
    }

    private void ResetBar()
    {
        currentBar = 0;
        level = 1;
    }

    private void CallFromPlayerPrefs()
    {
        CallData();
    }

    public void AddScore(int pointsToAdd)
    {
        currentBar = CurrentScore += pointsToAdd;
        UIManager.instance.UpdateScores();
    }

    public void SetHighScore()
    {
        if (CurrentScore>HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("highscore", CurrentScore);
        }
    }

    private void CallData()
    {
        Heart = PlayerPrefs.GetInt("hearts", 3);
        HighScore = PlayerPrefs.GetInt("highscore", 0);
    }

    public void SetDefaultHearts()
    {
        Heart = 3;
        SaveHeart();
    }

    public void DecreaseHeart()
    {
        Heart--;
        SaveHeart();
    }
    public void IncreaseHeart()
    {
        Heart++;
        SaveHeart();
    }

    private void SaveHeart()
    {
        PlayerPrefs.SetInt("hearts", heart);
    }
}
