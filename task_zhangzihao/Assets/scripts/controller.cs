using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class controller : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
IPointerUpHandler
{
    public GameObject cubeActor;
    [SerializeField] GameObject cubeActor_prefab;
    public pathmanager pathmanager;
    [SerializeField] int progress;
    int maxProgress; //used to display
    [SerializeField] bool move;
    public bool stage_autopassLevel;
    [SerializeField] int speed;
    public camerafollow camerafollow;


    public void OnPointerDown(PointerEventData eventData)
    {
        move = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        move = false;
    }

    public void ReadyPlayer()
    {
        move = false;
        stage_autopassLevel = false;
        //cubeActor.transform.GetChild(0).gameObject.SetActive(false);
        //if (cubeActor != null)
            cubeActor.SetActive(false);
        // Destroy(cubeActor);

        cubeActor.transform.GetChild(0).GetComponent<TrailRenderer>().Clear();
        //cubeActor = Instantiate(cubeActor_prefab);
        cubeActor.transform.position = pathmanager.level_selected.positions[pathmanager.level_selected.rail_start];
        cubeActor.transform.LookAt(pathmanager.level_selected.positions[pathmanager.level_selected.rail_start+1]);
        progress = pathmanager.level_selected.rail_start;
        maxProgress = pathmanager.level_selected.rail_destination;
        cubeActor.SetActive(true);
        camerafollow.ResetCamera();
        //cubeActor.transform.GetChild(0).gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        if(!stage_autopassLevel)
        {
            if (move)
            {
                progress += speed;
                if (progress < pathmanager.level_selected.rail_destination)
                {
                    cubeActor.transform.position = pathmanager.level_selected.positions[progress];
                    cubeActor.transform.LookAt(pathmanager.level_selected.positions[progress + 1]);

                }
                else
                {
                    stage_autopassLevel = true;
                    //gamemanager.GM.uimanager.HandlePassedDestination();
                }
                //else
                //{
                //    cubeActor.transform.position = pathmanager.level_selected.positions[progress];
                //}

            }

            gamemanager.GM.uimanager.RefreshUIProgress(progress - pathmanager.level_selected.rail_start, maxProgress - pathmanager.level_selected.rail_start);
        }
        else
        {
            progress += 1;
            if (progress < pathmanager.level_selected.rail_end)
            {
                cubeActor.transform.position = pathmanager.level_selected.positions[progress];
                cubeActor.transform.LookAt(pathmanager.level_selected.positions[progress + 1]);

               // int  pathmanager.level_selected.rail_end
                //confetti
                if (progress == pathmanager.level_selected.rail_destination +1)
                {
                    gamemanager.GM.vfxmanager.GetConfetti(pathmanager.level_selected.positions[progress], pathmanager.level_selected.positions[progress + 1]);
                }
                if (progress == pathmanager.level_selected.rail_destination + ((pathmanager.level_selected.rail_end -pathmanager.level_selected.rail_destination)/2))
                {
                    gamemanager.GM.vfxmanager.GetConfetti(pathmanager.level_selected.positions[progress], pathmanager.level_selected.positions[progress + 1]);
                }
                if (progress == pathmanager.level_selected.rail_end-1)
                {
                    gamemanager.GM.vfxmanager.GetConfetti(pathmanager.level_selected.positions[progress], pathmanager.level_selected.positions[progress + 1]);
                }

            }
            else
            {
                gamemanager.GM.pathmanager.chest.GetComponent<Animator>().SetTrigger("open");
                gamemanager.GM.uimanager.HandleWin();
                gameObject.SetActive(false);
            }
        }


    }
}
