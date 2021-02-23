using UnityEngine;

public class gamemanager : MonoBehaviour
{
    //****CENTRAL MANAGEMENT****//
    //****This Project Uses ECS and MVC design patterns****//
    //refers all other managers
    //public gameplaymanager gameplaymanager;
    //public unitsmanager unitsmanager;
    //public uimanager uimanager;
    //public vfxmanager vfxmanager;
    public uimanager uimanager;
    public pathmanager pathmanager;
    public vfxmanager vfxmanager;

    public static gamemanager GM;  //make it the only singleton in this game

    //set up
    void Awake()
    {
        if (GM != null) Destroy(GM);
        else GM = this;
        DontDestroyOnLoad(this);
    }


    public void InitializeGame()
    {

        pathmanager.PopulateLevel();
    }

    //we call initialize game here instead of in seperate classes because future this should be called from "server-got data" kind of logic
    //this is easier to identify and move
    void Start()
    {
        InitializeGame();
    }
}
