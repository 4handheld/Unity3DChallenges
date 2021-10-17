using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames.BasicApi.SavedGame;
using System;

public class PlayGameServicesScript : MonoBehaviour
{

    public static PlayGameServicesScript instance;
    private ISavedGameClient saver;
    private ISavedGameMetadata meta;

    private void Awake()
    {
        instance = this;
        

        GooglePlayGames.BasicApi.PlayGamesClientConfiguration playGamesClientConfiguration =
          new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder()
          .EnableSavedGames()
          .RequestIdToken()
          .Build();
        GooglePlayGames.PlayGamesPlatform.InitializeInstance(playGamesClientConfiguration);
        GooglePlayGames.PlayGamesPlatform.DebugLogEnabled = true;
        GooglePlayGames.PlayGamesPlatform.Activate();
        OnConnectionResponse(GooglePlayGames.PlayGamesPlatform.Instance.localUser.authenticated);
        
    }

    public void OnConnectClicked()
    {
        Social.localUser.Authenticate((bool success)=> {  
            OnConnectionResponse(success);
        });
    }

    private void OnConnectionResponse(bool IsAuthenticated)
    {
        if (IsAuthenticated)
        {
            PlayGameServicesScript.instance.UnlockAchievement("");
            GetSaver();
        }
        GameObject.FindGameObjectWithTag("LoginButton").SetActive(!IsAuthenticated);
        GameObject.FindGameObjectWithTag("LeaderBoardButton").SetActive(IsAuthenticated);
        GameObject.FindGameObjectWithTag("AchievementButton").SetActive(IsAuthenticated);
        
    }

    public void OnAchievementClicked()
    {
        if (!Social.localUser.authenticated)
        {
            return;
        }

        Social.ShowAchievementsUI();
    }

    public void UnlockAchievement(string achievementID)
    {
        Social.ReportProgress(achievementID, 100, (bool success) => {
        
        });
    }

    public void OnLeaderboardClicked()
    {
        if (!Social.localUser.authenticated)
        {
            return;
        }

        Social.ShowLeaderboardUI();
    }

    public void ReportScore(int score,string leaderboardID)
    {
        Social.ReportScore(score, leaderboardID, (bool success) => {

        });
    }

    //Cloud Save

    private void GetSaver()
    {
        if (!Social.localUser.authenticated)
        {
            return;
        }
        ((GooglePlayGames.PlayGamesPlatform)Social.Active).SavedGame
            .OpenWithAutomaticConflictResolution(
            "RunningPingu",
            GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseMostRecentlySaved,SavedGamesOpened);
    
    }

    private void SavedGamesOpened(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            saver = ((GooglePlayGames.PlayGamesPlatform)Social.Active).SavedGame;
            this.meta = meta;
        }

    }

    public void Save(String str)
    {
        if(saver == null)
        {
            return;
        }
        byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes("ABCD EFGH IJKL");
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder()
            .WithUpdatedDescription("Saved at "+DateTime.Now.ToString() ).Build();
        saver.CommitUpdate(meta, update, data,SaveCallback);
    }

    public void Load(String str)
    {
        if (saver == null)
        {
            return;
        }
        saver.ReadBinaryData(meta, ReadCallback);
    }

    private void ReadCallback(SavedGameRequestStatus status, byte[] data)
    {
        Debug.Log(status.ToString());
        if (status == SavedGameRequestStatus.Success)
        {
            String info = System.Text.ASCIIEncoding.ASCII.GetString(data);
            Debug.Log(info.ToString());
        }
    }

    private void SaveCallback(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        Debug.Log(status.ToString());
    }
}
