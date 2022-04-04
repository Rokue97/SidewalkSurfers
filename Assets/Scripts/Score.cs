using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public string name;
    public int score;
    public string url;

    public Score()
    {

    }

    public Score(string name, int score, string url)
    {
        this.name = name;
        this.score = score;
        this.url = url;
    }
}
