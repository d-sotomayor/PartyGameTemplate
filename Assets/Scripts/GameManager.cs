using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static bool MinigameInProgress = false;

    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnLoadMinigame += MinigameStart;

        EventManager.OnLoadMinigame += FadeOutMusic;
        audioS = GetComponent<AudioSource>();
        FadeInMusic();
    }

    private void OnDisable()
    {
        EventManager.OnLoadMinigame -= MinigameStart;
        EventManager.OnLoadMinigame -= FadeOutMusic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MinigameStart()
    {
        MinigameInProgress = true;
        Debug.Log(MinigameInProgress);
    }

    private void FadeInMusic()
    {
        //Fade in the music in 5 seconds
        audioS.Play();
        audioS.volume = 0;
        audioS.DOFade(0.35f, 8f);
    }

    private void FadeOutMusic()
    {
        audioS.DOFade(0f, 2f);
    }
}
