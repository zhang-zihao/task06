using UnityEngine;

public class collisiondetector : MonoBehaviour
{
    int counter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==8 && Vector3.Angle(gameObject.transform.forward, other.transform.forward) > 15)
        {
            gameObject.SetActive(false);
            counter++;
        }

        //avoid meaningless update of this if rail checking is already finished 
        if(counter>2)
        {
            this.enabled = false;
        }
       
    }
}
