using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string username;
    public int coinCount;
    public int gemCount;
    public int highScore;
    public List<int> skins;

    public User()
    {
        
    }

    public User(string username, int coinCount, int gemCount, int highScore, List<int> skins)
    {
        this.username = username;
        this.coinCount = coinCount;
        this.gemCount = gemCount;
        this.highScore = highScore;
        this.skins = skins;
    }
}
