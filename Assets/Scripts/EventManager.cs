using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public delegate void LoadEvent();
    //Event that minigame is being loaded
    public static event LoadEvent OnLoadMinigame;
    //Event that board map is being loaded
    public static event LoadEvent OnLoadBoardMap;
    //Event that Title screen is being loaded
    public static event LoadEvent OnLoadTitle;

    public delegate void BoardEvent();

    public static event BoardEvent OnLandMinigame;

    public static event BoardEvent OnLandCoin;

    public static event BoardEvent OnP1Turn;
    public static event BoardEvent OnP2Turn;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDisable()
    {
        LoadingScreen.OnAnimationFinished -= LoadMinigameFinished;
        LoadingScreen.OnAnimationFinished -= LoadBoardMapFinished;
        LoadingScreen.OnAnimationFinished -= LoadTitleFinished;
    }

    #region Player Turns

    public void NowP1Turn() { OnP1Turn(); }

    public void NowP2Turn() { OnP2Turn(); }

    #endregion

    #region Landed On Minigame Tile
    public static void LandedOnMinigame()
    {
        //Broadcast to system that a minigame tile has been landed on
        OnLandMinigame();
    }
    #endregion

    #region Landed On Coin Tile
    public static void LandedOnCoin()
    {
        //Broadcast to system that a coin tile has been landed on
        OnLandCoin();
    }
    #endregion

    #region Title Screen Loading
    public void LoadTitleTrigger()
    {
        OnLoadTitle();
        LoadingScreen.OnAnimationFinished += LoadTitleFinished;
    }
    private void LoadTitleFinished()
    {
        SceneManager.LoadScene("Title Screen");
        PlayerPrefs.DeleteAll(); // Reset all data when going back to the board
    }
    #endregion

    #region Board Map Loading
    public void LoadBoardMapTrigger()
    {
        OnLoadBoardMap();
        LoadingScreen.OnAnimationFinished += LoadBoardMapFinished;
    }

    private void LoadBoardMapFinished()
    {
        SceneManager.LoadScene("Test Board");
        
    }

    #endregion

    #region Minigame loading
    public void LoadMinigameTrigger()
    {
        // Broadcast the event that we are loading a minigame
        OnLoadMinigame();
        //Subscribe (aka listen) to the event that the loading screen animation has finished
        LoadingScreen.OnAnimationFinished += LoadMinigameFinished;
    }

    /*This function will be called when the "loading screen 
      animation finished" event has been broadcasted    */
    private void LoadMinigameFinished()
    {
        string minigame = SelectMinigame();

        SceneManager.LoadScene(minigame);
    }

        #region Task 1
        /***TO-DO***
         Implement SelectMinigame(), a semi-random selection algorithim to determine minigame.
         First ever selection is random, then all folowing minigame selections should have some
         way of prioritizing unplayed minigames, while retaining a somewhat random nature.   */

        private string SelectMinigame()
        {
            int randomInt = Random.Range(1, 5);
            switch (randomInt)
            {
                case 1:
                    return "Noah Scene";
                    break;
                case 2:
                    return "Dropper Minigame";
                    break;
                case 3:
                    return "test_intro";
                    break;
                case 4:
                    return "Berkeley_World";
                    break;
            }
            return null;
        }
        #endregion

    #endregion
}
