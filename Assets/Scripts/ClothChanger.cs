using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothChanger : MonoBehaviour
{
    [SerializeField] GameObject first, second, third, firstBuyButton, secondBuyButton, thirdBuyButton;
    [SerializeField] Button firstButton, secondButton, thirdButton;

    private void Start()
    {
        if (first.activeSelf)
        {
            firstBuyButton.SetActive(true);
            secondBuyButton.SetActive(false);
            thirdBuyButton.SetActive(false);

            firstButton.interactable = false;
            secondButton.interactable = true;
            thirdButton.interactable = true;
        }
        else if (second.activeSelf)
        {
            firstBuyButton.SetActive(false);
            secondBuyButton.SetActive(true);
            thirdBuyButton.SetActive(false);

            firstButton.interactable = true;
            secondButton.interactable = false;
            thirdButton.interactable = true;
        }
        else
        {
            firstBuyButton.SetActive(false);
            secondBuyButton.SetActive(false);
            thirdBuyButton.SetActive(true);

            firstButton.interactable = true;
            secondButton.interactable = true;
            thirdButton.interactable = false;
        }
    }

    public void OnClickShowFirst()
    {
        first.SetActive(true);
        second.SetActive(false);
        third.SetActive(false);

        firstBuyButton.SetActive(true);
        secondBuyButton.SetActive(false);
        thirdBuyButton.SetActive(false);

        firstButton.interactable = false;
        secondButton.interactable = true;
        thirdButton.interactable = true;
    }

    public void OnClickShowSecond()
    {
        first.SetActive(false);
        second.SetActive(true);
        third.SetActive(false);

        firstBuyButton.SetActive(false);
        secondBuyButton.SetActive(true);
        thirdBuyButton.SetActive(false);

        firstButton.interactable = true;
        secondButton.interactable = false;
        thirdButton.interactable = true;
    }

    public void OnClickShowThird()
    {
        first.SetActive(false);
        second.SetActive(false);
        third.SetActive(true);

        firstBuyButton.SetActive(false);
        secondBuyButton.SetActive(false);
        thirdBuyButton.SetActive(true);

        firstButton.interactable = true;
        secondButton.interactable = true;
        thirdButton.interactable = false;
    }
}
