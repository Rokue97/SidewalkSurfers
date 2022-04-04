using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    /*
    public enum ItemType
    {
        coin7500,
        coin45000,
        coin90000,
        coin180000,
        gems25,
        gems50,
        gems100,
        gems300
    }

    public ItemType itemType;
    [SerializeField] Purchaser purchaser;
    [SerializeField] Text priceText;

    private string defaultText;

    // Start is called before the first frame update
    void Start()
    {
        defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }

    public void ClickBuy()
    {
        switch (itemType)
        {
            case ItemType.coin7500:
                purchaser.BuyCoin7500();
                break;

            case ItemType.coin45000:
                purchaser.BuyCoin45000();
                break;

            case ItemType.coin90000:
                purchaser.BuyCoin90000();
                break;

            case ItemType.coin180000:
                purchaser.BuyCoin180000();
                break;

            case ItemType.gems25:
                purchaser.BuyGems25();
                break;

            case ItemType.gems50:
                purchaser.BuyGems50();
                break;

            case ItemType.gems100:
                purchaser.BuyGems100();
                break;

            case ItemType.gems300:
                purchaser.BuyGems300();
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (!purchaser.IsInitialized())
        {
            yield return null;
        }

        string loadedPrice = "";

        switch (itemType)
        {
            case ItemType.coin7500:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.coin7500);
                break;

            case ItemType.coin45000:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.coin45000);
                break;

            case ItemType.coin90000:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.coin90000);
                break;

            case ItemType.coin180000:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.coin180000);
                break;

            case ItemType.gems25:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.gems25);
                break;

            case ItemType.gems50:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.gems50);
                break;

            case ItemType.gems100:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.gems100);
                break;

            case ItemType.gems300:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.gems300);
                break;
        }

        priceText.text = loadedPrice;
    }
    */
}
