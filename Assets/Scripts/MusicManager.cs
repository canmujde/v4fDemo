using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] private AudioSource source;

    public static MusicManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Toggle()
    {
        source.volume = PlayerSettings.instance.MusicOn == true ? 1 : 0;
    }
}
