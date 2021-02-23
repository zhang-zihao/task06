using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest_ejectcoin : MonoBehaviour
{
    [SerializeField] GameObject coin_prefab;
    public void EjectCoins()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject _piece = Instantiate(coin_prefab);
            _piece.transform.position = gameObject.transform.position;
            _piece.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            //_piece.transform.localScale = Random.Range(1, 4) * new Vector3(0.1f, 0.1f, 0.1f);
            _piece.GetComponent<Rigidbody>().AddExplosionForce(0.2f, gameObject.transform.up + new Vector3( Random.Range(3f,5f),0f,0f) , 0.2f);

        }
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
