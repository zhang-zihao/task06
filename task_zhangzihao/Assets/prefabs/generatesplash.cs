using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatesplash : MonoBehaviour
{
   

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.transform.position.y<-5)
        {
            GameObject splash = Instantiate(gamemanager.GM.vfxmanager.waterSplash);
            splash.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
