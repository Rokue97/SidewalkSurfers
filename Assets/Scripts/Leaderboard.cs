
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    /*
    [SerializeField] GameObject userScore, leaderboardContent, rankText, usernameText, scoreText, image, leaderboardUI, loading;
    [SerializeField] Database database;
    [SerializeField] List<string> names;
    [SerializeField] List<int> scores;
    [SerializeField] List<string> usernames;
    [SerializeField] List<string> urls;
    [SerializeField] LocalCache cache;
    [SerializeField] GameManager gameManager;
    [SerializeField] SignInSample.SigninSampleScript signIn;
    [SerializeField] AdsManager adsManager;

    int intKeeper;
    string stringKeeper;
    string stringNameKeeper;
    string stringUrlKeeper;
    int loop = 0;
    bool list = false;
    int a = 0;

    public void OnClickGetEverything()
    {
        leaderboardUI.SetActive(true);
        names.Clear();
        scores.Clear();
        usernames.Clear();
        urls.Clear();
        FirebaseDatabase.DefaultInstance.GetReference("Top100").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                gameManager.InfoPanel("Please Try Again Later", "Warning!");
                Debug.Log("Please Try Again Later");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot score in snapshot.Children)
                {
                    a++;
                    names.Add(score.Key.ToString());
                    scores.Add(score.Child("score").Value.GetHashCode());
                    usernames.Add(score.Child("name").Value.ToString());
                    urls.Add(score.Child("url").Value.ToString());
                }
                Debug.Log(a);
                ReArrange();
            }
        });
    }

    private void Update()
    {
        if (list)
        {
            loading.SetActive(false);
            for (int i = 0; i < scores.Count; i++)
            {
                GameObject userLeaderboardInfo = Instantiate(userScore, leaderboardContent.transform.position, Quaternion.identity);

                GameObject userLeaderboardRankInfo = Instantiate(rankText, leaderboardContent.transform.position, Quaternion.identity) as GameObject;
                GameObject userLeaderboardImageInfo = Instantiate(image, leaderboardContent.transform.position, Quaternion.identity) as GameObject;
                GameObject userLeaderboardUsernameInfo = Instantiate(usernameText, leaderboardContent.transform.position, Quaternion.identity) as GameObject;
                GameObject userLeaderboardScoreInfo = Instantiate(scoreText, leaderboardContent.transform.position, Quaternion.identity) as GameObject;

                userLeaderboardRankInfo.GetComponent<Text>().text = (i + 1).ToString();
                StartCoroutine(SetImage(urls[i], userLeaderboardImageInfo.GetComponent<RawImage>()));                
                userLeaderboardUsernameInfo.GetComponent<Text>().text = usernames[i];
                userLeaderboardScoreInfo.GetComponent<Text>().text = scores[i].ToString();

                userLeaderboardInfo.transform.SetParent(leaderboardContent.transform);
                userLeaderboardInfo.transform.SetAsLastSibling();
                userLeaderboardRankInfo.transform.SetParent(userLeaderboardInfo.transform);
                userLeaderboardImageInfo.transform.SetParent(userLeaderboardInfo.transform);
                userLeaderboardUsernameInfo.transform.SetParent(userLeaderboardInfo.transform);
                userLeaderboardScoreInfo.transform.SetParent(userLeaderboardInfo.transform);
                userLeaderboardInfo.transform.localScale = new Vector3(1, 1, 1);
                userLeaderboardInfo.transform.localRotation = leaderboardContent.transform.localRotation;
            }
            list = false;
        }
    }

    void ReArrange()
    {
        loop = 0;
        while (loop != scores.Count - 1)
        {
            loop = 0;
            for (int i = 0; i < scores.Count - 1; i++)
            {
                if (scores[i] < scores[i + 1])
                {
                    intKeeper = scores[i];
                    scores[i] = scores[i + 1];
                    scores[i + 1] = intKeeper;

                    stringKeeper = names[i];
                    names[i] = names[i + 1];
                    names[i + 1] = stringKeeper;

                    stringNameKeeper = usernames[i];
                    usernames[i] = usernames[i + 1];
                    usernames[i + 1] = stringNameKeeper;

                    stringUrlKeeper = urls[i];
                    urls[i] = urls[i + 1];
                    urls[i + 1] = stringUrlKeeper;
                }
                else
                {
                    loop++;
                }
            }
        }
        list = true;
    }    

    public void ReArrangeBackground(string userID, int newHighScore, string url)
    {
        names.Clear();
        scores.Clear();
        usernames.Clear();
        urls.Clear();
        FirebaseDatabase.DefaultInstance.GetReference("Top100").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Please Try Again Later");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot score in snapshot.Children)
                {
                    names.Add(score.Key.ToString());
                    scores.Add(score.Child("score").Value.GetHashCode());
                    usernames.Add(score.Child("name").Value.ToString());
                    urls.Add(score.Child("url").Value.ToString());
                }

                while (loop != scores.Count - 1)
                {
                    loop = 0;
                    for (int i = 0; i < scores.Count - 1; i++)
                    {
                        if (scores[i] < scores[i + 1])
                        {
                            intKeeper = scores[i];
                            scores[i] = scores[i + 1];
                            scores[i + 1] = intKeeper;

                            stringKeeper = names[i];
                            names[i] = names[i + 1];
                            names[i + 1] = stringKeeper;

                            stringNameKeeper = usernames[i];
                            usernames[i] = usernames[i + 1];
                            usernames[i + 1] = stringNameKeeper;

                            stringUrlKeeper = urls[i];
                            urls[i] = urls[i + 1];
                            urls[i + 1] = stringUrlKeeper;
                        }
                        else
                        {
                            loop++;
                        }
                    }
                }

                bool exist = false;
                for (int i = 0; i < names.Count; i++)
                {
                    if (names[i] == userID)
                    {
                        exist = true;
                    }
                }

                if (exist)
                {
                    database.reference.Child("Top100").Child(userID).Child("score").SetValueAsync(newHighScore);
                }
                else
                {
                    if (names.Count != 100)
                    {
                        if(signIn.signIn)
                            database.WriteNewScore(userID, cache.GetName(), newHighScore, url);
                    }
                    else
                    {
                        if (newHighScore > scores[scores.Count - 1])
                        {
                            database.reference.Child("Top100").Child(names[names.Count - 1]).RemoveValueAsync();
                            if(signIn.signIn)
                                database.WriteNewScore(userID, cache.GetName(), newHighScore, url);
                        }
                    }
                }

                
            }
        });
    }

    IEnumerator SetImage(string url, RawImage img)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            img.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    public void OnClickCloseLeaderboard()
    {
        foreach (Transform child in leaderboardContent.transform)
        {
            Destroy(child.gameObject);
        }
        loading.SetActive(true);
        leaderboardUI.SetActive(false);
        adsManager.ShowInterstitialAd();
    }
    */
}
