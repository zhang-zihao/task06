using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxmanager : MonoBehaviour
{
    public GameObject waterSplash;
    public GameObject confetti;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetConfetti(Vector3 position,Vector3 lookAtPosition)
    {
        GameObject _c = Instantiate(confetti);
        _c.transform.position = position;
        _c.transform.LookAt(lookAtPosition);
    }
}
