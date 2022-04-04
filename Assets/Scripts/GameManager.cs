using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText, coinsText, modifierText, totalGemText, infoText, timerText, infoTitle;
    public int score, coins, totalCoinCount, totalGem, highScore;
    public float modifier;

    [SerializeField] GameObject settingsPopup, menuObjects, gameObjects, hider, shop, gameOverPanel, gameOver, gameOverInfo, meSection, infoPanel, timer, menuElements, betaInfoPanel;
    [SerializeField] Animator menuAnim, gameAnim, cameraAnim;
    [SerializeField] LevelManager levelManager;
    [SerializeField] Button starterButton, settingsButton, shopButton, leaderboardButton, meButton;
    [SerializeField] Text endGameScoreText, endGameCoinText, highScoreText, totalCoin;
    [SerializeField] AdsManager adsManager;

    int lastScore;
    public bool pressedStarter = false;

    Player player;
   //[SerializeField] Database database;
    [SerializeField] SignInSample.SigninSampleScript signInSampleScript;
    [SerializeField] LocalCache cache;
    //[SerializeField] Leaderboard leaderboard;
    [SerializeField] Canvas canvas;
    [SerializeField] MenuItemSpawner itemSpawner;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        score = 0;
        modifier = 1;
        modifierText.text = "x1.0";
        highScoreText.text = cache.GetHighScore().ToString();
        totalCoin.text = cache.GetCoinCount().ToString();
        totalCoinCount = cache.GetCoinCount();
        totalGem = cache.GetGemCount();
        totalGemText.text = cache.GetGemCount().ToString();
        scoreText.text = score.ToString("0");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (cache.GetBetaInfoPanel() == 1)
            betaInfoPanel.SetActive(false);
    }

    void Update()
    {
        if(pressedStarter && !player.isGameStarted)
        {
            player.isGameStarted = true;
            menuAnim.SetBool("FadeOut", true);
            cameraAnim.SetBool("Rotate", true);
            StartCoroutine(DisableUIObjects(menuObjects));
            gameObjects.SetActive(true);
            player.StartRunning();
            player.speedIncreaseLastTick = Time.time;
        }

        if (player.isGameStarted && !player.gameOver)
        {
            score += (int) (Time.deltaTime * modifier * 100);
            if (lastScore != score)
            {
                lastScore = score;
                scoreText.text = score.ToString("0");
            }
        }

        if (player.gameOver)
        {
            gameOverPanel.SetActive(true);
            endGameScoreText.text = scoreText.text;
            endGameCoinText.text = coinsText.text;
            if (score > cache.GetHighScore())
            {
                cache.SetHighScore(score);
                highScore = score;
                /*
                if (signInSampleScript.signIn)
                {
                    //database.WriteAllData();
                    //leaderboard.ReArrangeBackground(signInSampleScript.currentUser.UserId, cache.GetHighScore(), signInSampleScript.currentUser.PhotoUrl.ToString());
                }  
                */
            }
                
        }
    }

    public void GetCoin()
    {
        coins++;
        coinsText.text = coins.ToString("0");       
    }

    void AddCoinsAtTheEnd(int coinAmount)
    {
        totalCoinCount += coinAmount;
        cache.SetCoinCount(totalCoinCount);
        //if(signInSampleScript.signIn)
            //database.WriteAllData();
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifier = 1.0f + modifierAmount;
        modifierText.text = "x" + modifier.ToString("0.0");

    }

    public void OnClickMainMenu(int id)
    {
        pressedStarter = false;
        Time.timeScale = 1;
        hider.SetActive(true);
        player.isGameStarted = false;

        if (id == 1)
            AddCoinsAtTheEnd(coins);

        StartCoroutine(ResetGame());
        StartCoroutine(DisableHider());
    }

    public void OnClickPlayAgain()
    {
        hider.SetActive(true);
        pressedStarter = true;
        StartCoroutine(PlayAgain());
        StartCoroutine(DisableHider());
    }

    IEnumerator DisableUIObjects(GameObject objects)
    {
        yield return new WaitForSeconds(1);

        objects.SetActive(false);

    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1);

        score = 0;
        coins = 0;
        coinsText.text = coins.ToString();
        UpdateModifier(0);
        gameOver.SetActive(true);
        gameOverInfo.SetActive(false);
        gameOverPanel.SetActive(false);
        player.anim.SetBool("isWalking", true);
        player.ResetGame(false);
        levelManager.ResetGame(false);
        menuObjects.SetActive(true);
        menuAnim.SetBool("ResetPos", true);
        gameObjects.SetActive(false);
        settingsPopup.SetActive(false);   
        cameraAnim.SetBool("Rotate", false);
        highScoreText.text = cache.GetHighScore().ToString();
        totalCoin.text = cache.GetCoinCount().ToString();
        totalCoinCount = cache.GetCoinCount();
        totalGemText.text = cache.GetGemCount().ToString();
        settingsButton.interactable = true;
        shopButton.interactable = true;
        //leaderboardButton.interactable = true;
        meButton.interactable = true;
        player.isGamePaused = false;
        itemSpawner.isGameStarted = false;
        itemSpawner.Start();
        starterButton.gameObject.SetActive(true);
    }

    IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(1);

        AddCoinsAtTheEnd(coins);
        score = 0;
        coins = 0;
        coinsText.text = coins.ToString();
        UpdateModifier(0);
        gameOver.SetActive(true);
        gameOverInfo.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPopup.SetActive(false);
        player.anim.SetBool("isAlive", true);
        player.ResetGame(true);
        levelManager.ResetGame(true);
    }

    IEnumerator DisableHider()
    {
        yield return new WaitForSeconds(2);
        hider.SetActive(false);
    }

    IEnumerator Resume()
    {
        yield return new WaitForSeconds(1);

        levelManager.ResumeGame();
        pressedStarter = true;
        player.gameOver = false;
        gameOver.SetActive(false);
        gameOverInfo.SetActive(true);
        gameOverPanel.SetActive(false);
        player.anim.SetBool("isAlive", true);
        player.runSpeed = player.originalSpeed + modifier - 1;
    }

    public void OnClickStartGame()
    {
        pressedStarter = true;
        starterButton.gameObject.SetActive(false);
        levelManager.currentSpawnZ = player.transform.position.z + 30;
        settingsButton.interactable = false;
        shopButton.interactable = false;
        //leaderboardButton.interactable = false;
        meButton.interactable = false;
        itemSpawner.isGameStarted = true;
        StartCoroutine(MenuElementsRemover());
    }

    public void OnClickOpenMeSection()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        meSection.SetActive(true);
    }

    public void OnClickCloseMeSection()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        meSection.SetActive(false);
        //adsManager.ShowInterstitialAd();
    }

    public void OnClickOpenShop()
    {
        shop.SetActive(true);
    }

    public void OnClickCloseShop()
    {
        shop.SetActive(false);
        //adsManager.ShowInterstitialAd();
    }

    public void SetNewCoinAmount()
    {
        totalCoin.text = cache.GetCoinCount().ToString();
    }

    public void SetNewGemAmount()
    {
        totalGemText.text = cache.GetGemCount().ToString();
    }

    public void OnClickSkipRevive()
    {
        gameOver.SetActive(false);
        gameOverInfo.SetActive(true);
    }

    public void OnClickResumeWithGem()
    {
        if(totalGem >= 1)
        {
            totalGem--;
            cache.SetGemCount(totalGem);
            hider.SetActive(true);
            StartCoroutine(Resume());
            StartCoroutine(DisableHider());
            //if(signInSampleScript.signIn)
                //database.WriteAllData();
        }
    }

    public void OnClickResumeWithAd()
    {
        //adsManager.UserChoseToWatchAd();
        //adsManager.RequestRewardedAd();
    }

    public void ResumeWithAd()
    {
        hider.SetActive(true);
        StartCoroutine(Resume());
        StartCoroutine(DisableHider());
    }

    public void OnClickOpenCredits()
    {
        InfoPanel("Music from https://www.zapsplat.com", "Credits");
    }

    public void OClickCloseBetaPanel()
    {
        betaInfoPanel.SetActive(false);
        cache.SetBetaInfoPanel(1);
    }
    
    public void InfoPanel(string info, string title)
    {
        infoPanel.SetActive(true);
        infoText.text = info;
        infoTitle.text = title;
    }

    public void OnClickCloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    public void UpdateCoinText()
    {
        totalCoin.text = cache.GetCoinCount().ToString();
        totalCoinCount = cache.GetCoinCount();
    }

    public void UpdateGemText()
    {
        totalGemText.text = cache.GetGemCount().ToString();
        totalGem = cache.GetGemCount();
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text = cache.GetHighScore().ToString();
        highScore = cache.GetHighScore();
    }

    public void AddCoins(int amount)
    {
        totalCoinCount += amount;
        cache.SetCoinCount(totalCoinCount);
        totalCoin.text = cache.GetCoinCount().ToString();
        //database.WriteAllData();
    }

    public void AddGems(int amount)
    {
        totalGem += amount;
        cache.SetGemCount(totalGem);
        totalGemText.text = cache.GetGemCount().ToString();
        //database.WriteAllData();
    }

    public void CheckTop100(string userID, string photoUrl)
    {
        //leaderboard.ReArrangeBackground(userID, cache.GetHighScore(), photoUrl);
    }

    public void Timer()
    {
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown(float countdownValue = 3)
    {
        timer.SetActive(true);
        float currCountdownValue = countdownValue;

        while (currCountdownValue > 0)
        {
            timerText.text = currCountdownValue.ToString("0");
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSecondsRealtime(1.0f);
            currCountdownValue--;
        }

        timer.SetActive(false);
        Time.timeScale = 1;
    }   

    IEnumerator MenuElementsRemover()
    {
        yield return new WaitForSeconds(2);
        menuElements.SetActive(false);
    }
}
