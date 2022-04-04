using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeSection : MonoBehaviour
{
    [SerializeField] List<GameObject> previews = new List<GameObject>();
    [SerializeField] Player player;
    [SerializeField] GameManager gameManager;
    public List<GameObject> buttons = new List<GameObject>();
    [SerializeField] GameObject previewPanel;
    [SerializeField] List<Text> characterName;
    [SerializeField] Text previewName;
    //[SerializeField] Database database;
    [SerializeField] LocalCache cache;
    [SerializeField] SignInSample.SigninSampleScript signIn;


    private void Start()
    {
        FixButtons(PlayerPrefs.GetInt("SkinID"));
    }

    public void OnClickShowPreview(int id)
    {
        previewPanel.SetActive(true);
        for (int i = 0; i < previews.Count; i++)
        {
            if (id == i)
            {
                previews[i].SetActive(true);
                previewName.text = characterName[i].text;
            }              
            else
                previews[i].SetActive(false);
        }
    }

    public void OnClickClosePreview()
    {
        previewPanel.SetActive(false);
    }

    public void OnClickEquipSkin(int id)
    {
        if (buttons[id].GetComponent<Skins>().isUnlocked)
        {
            player.SetSkin(id);
            FixButtons(id);           
        }
        else
        {
            if (buttons[id].GetComponent<Skins>().isCoin)
            {
                if (buttons[id].GetComponent<Skins>().value <= gameManager.totalCoinCount)
                {
                    gameManager.totalCoinCount -= buttons[id].GetComponent<Skins>().value;
                    buttons[id].GetComponent<Skins>().isUnlocked = true;
                    cache.SetClothInfo(buttons[id].GetComponent<Skins>().name, 1);
                    buttons[id].GetComponent<Skins>().buttonText.text = "Select";
                    buttons[id].GetComponent<Skins>().coinImage.gameObject.SetActive(false);
                    cache.SetCoinCount(gameManager.totalCoinCount);
                    gameManager.SetNewCoinAmount();
                    player.SetSkin(id);
                    FixButtons(id);
                    //database.unlockedSkins[buttons[id].GetComponent<Skins>().name] = 1;
                    //if(signIn.signIn)
                       // database.WriteAllData();
                }
                else
                {
                    gameManager.InfoPanel("Not enough Coins", "Warning!");
                }
            }
            else
            {
                if (buttons[id].GetComponent<Skins>().value <= gameManager.totalGem)
                {
                    gameManager.totalGem -= buttons[id].GetComponent<Skins>().value;
                    buttons[id].GetComponent<Skins>().isUnlocked = true;
                    cache.SetClothInfo(buttons[id].GetComponent<Skins>().name, 1);
                    buttons[id].GetComponent<Skins>().buttonText.text = "Select";
                    buttons[id].GetComponent<Skins>().coinImage.gameObject.SetActive(false);
                    cache.SetGemCount(gameManager.totalGem);
                    gameManager.SetNewGemAmount();
                    player.SetSkin(id);
                    FixButtons(id);
                    //database.unlockedSkins[buttons[id].GetComponent<Skins>().name] = 1;
                    //if(signIn.signIn)
                        //database.WriteAllData();
                }
                else
                {
                    gameManager.InfoPanel("Not enough Gems", "Warning!");
                }
            }
            
        }
            
    }

    void FixButtons(int id)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if(id != i && buttons[i].GetComponent<Skins>() != null)
            {
                buttons[i].GetComponent<Button>().interactable = true;
                if(buttons[i].GetComponent<Skins>().isUnlocked)
                    buttons[i].GetComponent<Skins>().buttonText.text = "Select";
            }
            else if (buttons[i].GetComponent<Skins>() != null)
            {
                buttons[i].GetComponent<Button>().interactable = false;
                buttons[i].GetComponent<Skins>().buttonText.text = "Selected";
            }
        }
    }
}
