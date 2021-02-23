using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flyingcoin : MonoBehaviour
{
    Vector3 first_target;
    bool flytoSecondTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        flytoSecondTarget = false;
        first_target = gameObject.GetComponent<RectTransform>().anchoredPosition3D + new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0f);
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!flytoSecondTarget)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition3D += 0.2f * (first_target - gameObject.GetComponent<RectTransform>().anchoredPosition3D).normalized;
        }
        else
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition3D += 0.2f * (gamemanager.GM.uimanager.currencyDisplay.GetComponent<RectTransform>().anchoredPosition3D - gameObject.GetComponent<RectTransform>().anchoredPosition3D).normalized;
        }

        if((first_target - gameObject.GetComponent<RectTransform>().anchoredPosition3D).sqrMagnitude<1)
        {
            flytoSecondTarget = true;
        }
    }
}
