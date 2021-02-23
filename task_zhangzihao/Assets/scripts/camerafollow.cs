using UnityEngine;

public class camerafollow : MonoBehaviour
{
    //****Camera control class****//
    //mechanics
    Vector3 desired_position;
    Vector3 offset;
    [SerializeField] float smoothTime;
    Vector3 velocity = Vector3.zero;

    [SerializeField] Transform cameraParent_normal;
    [SerializeField] Transform cameraParent_spin;

    void Start()
    {
        //offset: get initial
        offset = new Vector3(0, 30f, -8f);
    }
    public void ResetCamera()
    {
        //position/rotation: restore initial
        gameObject.transform.SetParent(cameraParent_normal);
        cameraParent_spin.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.position = gamemanager.GM.uimanager.controller.cubeActor.transform.position + offset;
    }

    void FixedUpdate()
    {
        //gameplay mode: camera follows player with fixed offset(smooth damp)
        if(!gamemanager.GM.uimanager.controller.stage_autopassLevel)
        {
            //smoothly change camera position
            desired_position = gamemanager.GM.uimanager.controller.cubeActor.transform.position + offset;
            gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, desired_position, ref velocity, smoothTime);

        }
        else
        {
            //rotates around player when clears level
            gameObject.transform.SetParent(cameraParent_spin);
            cameraParent_spin.rotation *= Quaternion.Euler(0, -0.05f, 0);
        }
    }
}

