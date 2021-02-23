using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uimanager : MonoBehaviour
{
    public controller controller;
    public GameObject startScreen;
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject currencyDisplay;

    [SerializeField] Image levelProgressFill;
    [SerializeField] TextMeshProUGUI progressPercentge;

    [SerializeField] GameObject flyingCoins;

    GameObject screenToOpen;
    int counter;

    public void HandleLose()
    {
        controller.gameObject.SetActive(false);
        WaitAndOpenScreen(loseScreen);
        //loseScreen.SetActive(true);
    }
    //public void HandlePassedDestination()
    //{
    //   // controller.gameObject.SetActive(false);
    //}
    public void HandleWin()
    {
        Debug.Log("handling win");
        WaitAndOpenScreen(winScreen);
        //winScreen.SetActive(true);
    }

    public void Button_TapToRestart()
    {
        startScreen.SetActive(true);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        gamemanager.GM.InitializeGame();
    }

    public void RefreshUIProgress(int progress, int maxprogress)
    {
        float progress_Maxis1 = (float)progress / (float)maxprogress;
        levelProgressFill.fillAmount = progress_Maxis1;
        progressPercentge.text = Mathf.FloorToInt(progress_Maxis1 * 100).ToString() + "%";
    }

    public void SpawnFlyingCoins()
    {
        for (int i = 0; i < 15;i++)
        {
            GameObject _c = Instantiate(flyingCoins);
            _c.GetComponent<RectTransform>().anchoredPosition3D = winScreen.GetComponent<RectTransform>().anchoredPosition3D;
        }
    }

    void WaitAndOpenScreen(GameObject screen)
    {
        counter = 0;
        screenToOpen = screen;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(screenToOpen !=null)
        {
            counter++;
            if (counter > 150)
            {
                screenToOpen.SetActive(true);
                screenToOpen = null;
            }
        }
       
    }
}
