using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{
    public bool isUnlocked = false;
    public Text buttonText;
    public Image coinImage;
    public bool isCoin;
    public int value;
    public int name;
    [SerializeField] LocalCache cache;

    public void Start()
    {
        if (cache.GetClothInfo(name) == 1)
        {
            isUnlocked = true;
            buttonText.text = "Select";
        }
        else if (cache.GetClothInfo(name) == 0)
        {
            isUnlocked = false;
            buttonText.text = value.ToString();
        }
        
        if (isUnlocked)
        {
            coinImage.gameObject.SetActive(false);
        }            
    }
}
