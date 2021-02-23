using UnityEngine;

public class ballSpawner : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float spawnGap;
    float counter;
    [SerializeField] Transform usedObjPool;
    Vector3 lastvector;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lastvector != gameObject.transform.position)
        {
            counter += Time.deltaTime;
            if (counter > spawnGap)
            {
                GameObject _newball = gamemanager.GM.pathmanager.GetPooledObject("player_bubble");
                _newball.SetActive(true);
                _newball.transform.SetParent(gameObject.transform);
                _newball.transform.localPosition = new Vector3(0.1f * Random.Range(-4, 5), 0.05f * Random.Range(0, 2), -0.1f * Random.Range(0, 3));
                int scale = Random.Range(1, 6);

                _newball.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f) * scale;

                _newball.GetComponent<ball>().destination = (_newball.transform.position + (5 * transform.InverseTransformDirection(0, -0.2f, -0.5f)));
                _newball.transform.SetParent( usedObjPool);

                spawnGap = 0f;
            }
        }
        lastvector = gameObject.transform.position;
    }
}
