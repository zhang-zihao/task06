using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_losescreen : MonoBehaviour
{
    [SerializeField] GameObject rewardedGroup;
    [SerializeField] GameObject tapToRestart;
    public void CloseRewardAdChoice()
    {
        rewardedGroup.SetActive(false);
        tapToRestart.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
