using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private bool musicOn;
    [SerializeField] private Toggle toggle;

    public static PlayerSettings instance;
    

    public bool MusicOn { get => musicOn; set => musicOn = value; }

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
        musicOn = PlayerPrefs.GetInt("music", 1) == 1 ? true : false;
        toggle.isOn = musicOn;
        MusicManager.instance.Toggle();
    }

    public void SetMusicState()
    {
        musicOn = !musicOn;
        MusicManager.instance.Toggle();
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("music", musicOn == true ? 1 : 0);
    }
}
