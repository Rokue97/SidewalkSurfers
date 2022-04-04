

using System.Collections.Generic;
using UnityEngine;
using SignInSample;

public class Database: MonoBehaviour
{/*
    public DatabaseReference reference;
    [SerializeField] GameManager gameManager;
    [SerializeField] SigninSampleScript signIn;
    [SerializeField] LocalCache cache;
    [SerializeField] MeSection me;
    public List<int> unlockedSkins;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://sidewalk-surfers-og-82823611.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;       
    }

    public void ReadyWriteNewUser()
    {
        WriteNewUser(signIn.currentUser.UserId, signIn.currentUser.DisplayName, gameManager.totalCoinCount, gameManager.totalGem, gameManager.highScore, unlockedSkins);
        cache.SetName(signIn.currentUser.DisplayName);
    }

    public void GetAllData()
    {
        GetDataCoinCount(signIn.currentUser.UserId);
        GetDataGemCount(signIn.currentUser.UserId);
        GetDataHighScore(signIn.currentUser.UserId);
        GetDataSkins(signIn.currentUser.UserId);
        cache.SetName(signIn.currentUser.DisplayName);
    }

    public void WriteAllData()
    {
        WriteNewData(signIn.currentUser.UserId, cache.GetCoinCount(), cache.GetGemCount(), cache.GetHighScore());
        gameManager.CheckTop100(signIn.currentUser.UserId, signIn.currentUser.PhotoUrl.ToString());
    }

    public void WriteNewUser(string userId, string name, int coinCount, int gemCount, int highScore, List<int> skins)
    {
        User user = new User(name, coinCount, gemCount, highScore, skins);
        string json = JsonUtility.ToJson(user);

        reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

    public void WriteNewScore(string userID, string name, int highScore, string url)
    {
        Score score = new Score(name, highScore, url);
        string json = JsonUtility.ToJson(score);

        reference.Child("Top100").Child(userID).SetRawJsonValueAsync(json);
    }

    public void WriteNewData(string userId, int coinCount, int gemCount, int highScore)
    {
        reference.Child("users").Child(userId).Child("coinCount").SetValueAsync(coinCount);
        reference.Child("users").Child(userId).Child("gemCount").SetValueAsync(gemCount);
        reference.Child("users").Child(userId).Child("highScore").SetValueAsync(highScore);
        reference.Child("users").Child(userId).Child("skins").SetValueAsync(unlockedSkins);
    }

    public void GetDataCoinCount(string userId)
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("users")
      .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              cache.SetCoinCount(snapshot.Child(userId).Child("coinCount").Value.GetHashCode());
              gameManager.UpdateCoinText();
              // Do something with snapshot...
          }
      });
    }

    public void GetDataGemCount(string userId)
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("users")
      .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              cache.SetGemCount(snapshot.Child(userId).Child("gemCount").Value.GetHashCode());
              gameManager.UpdateGemText();
              // Do something with snapshot...
          }
      });
    }

    public void GetDataHighScore(string userId)
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("users")
      .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              cache.SetHighScore(snapshot.Child(userId).Child("highScore").Value.GetHashCode());
              gameManager.UpdateHighScoreText();
              // Do something with snapshot...
          }
      });
    }

    public void GetDataSkins(string userId)
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("users")
      .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              for(int i = 0; i < 21; i++)
              {
                  unlockedSkins[i] = snapshot.Child(userId).Child("skins").Child(i.ToString()).Value.GetHashCode();
                  if(i == 0)
                  {
                      unlockedSkins[i] = 1;
                  }
                  cache.SetClothInfo(i, unlockedSkins[i]);
                  me.buttons[i].GetComponent<Skins>().Start();
              }             
              // Do something with snapshot...
          }
      });
    }

    public void GetDataHighScoreFromTop100(string userId, int highScore)
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("users")
      .GetValueAsync().ContinueWith(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              highScore = snapshot.Child(userId).Child("highScore").Value.GetHashCode();
              // Do something with snapshot...
          }
      });
    }
    */
}
