using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject piece_prefab;
   // float counter;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)//means enemy colllide with player
        {
            PlayerDie();
        }
    }
    void PlayerDie()
    {
        for (int i = 0; i < 15;i++)
        {
            GameObject _piece = Instantiate(piece_prefab);
            _piece.transform.position = gameObject.transform.position;
            _piece.transform.rotation = gameObject.transform.rotation;
            _piece.transform.localScale = Random.Range(1,4) * new Vector3(0.1f,0.1f,0.1f);
            _piece.GetComponent<Rigidbody>().AddExplosionForce(0.2f, gameObject.transform.position, 0.2f);
        }
        gameObject.SetActive(false);

        gamemanager.GM.uimanager.HandleLose();
    }
    //void GenerateTrail()
    //{
    //    GameObject trail = gamemanager.GM.pathmanager.GetPooledObject("trail");
    //    trail.transform.position = gameObject.transform.position;
    //    trail.transform.position += new Vector3(0, 0.55f, 0);
    //    trail.SetActive(true);
    //    gamemanager.GM.pathmanager._generated_pool.Add(trail);
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //counter += Time.deltaTime;
        //if(counter>0.005f)
        //{
          //  GenerateTrail();
            //counter = 0;
        //}

    }
}
