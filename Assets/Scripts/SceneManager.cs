using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Text m_Text;
    [SerializeField] Image bar;

    AsyncOperation asyncOperation;

    // Start is called before the first frame update
    void Start()
    {
        asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
    }

    private void Update()
    {
        if (!asyncOperation.isDone)
        {
            m_Text.text = (asyncOperation.progress * 100).ToString("0") + "%";
            bar.fillAmount = asyncOperation.progress;
        }
        else
            asyncOperation.allowSceneActivation = true;
    }
}
