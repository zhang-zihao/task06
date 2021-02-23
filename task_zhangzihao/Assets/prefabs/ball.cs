using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//little movement script, when water-balls(small drips after the cube) are spawned, they tend to move a little bit
public class ball : MonoBehaviour
{
    public Vector3 destination;
    Vector3 _travelEachframe;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _travelEachframe = (destination - gameObject.transform.position) / 1000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.localScale -= new Vector3(0.01f,0.01f,0.01f);
        if (gameObject.transform.localScale.x < 0.01f)
            gameObject.SetActive(false);

        gameObject.transform.position += _travelEachframe;
    }
}
