using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private string rewardedPlacement = "rewardedVideo";
    [Tooltip("Is it test mode?")]
    [SerializeField] private bool isTest;

    public static AdManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3861709", isTest);

    }

    public void ShowAd(string placement)
    { 
        Advertisement.Show(placement);
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            PlayerStats.instance.SetDefaultHearts();
            UIManager.instance.UpdateAll();
            UIManager.instance.CloseAdPanel();
        }
        else if(showResult == ShowResult.Failed)
        {
            Debug.Log("Reklam izlenmedi!");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsReady(string placementId)
    {

    }

    

}
