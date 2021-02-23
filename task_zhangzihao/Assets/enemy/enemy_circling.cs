using UnityEngine;

public class enemy_circling : MonoBehaviour
{
    [SerializeField] Vector3[] positions;
    //[SerializeField]int totalPoints;
    //[SerializeField] float radius;
    [SerializeField] Transform rotateCenter;

    public float ThetaScale = 0.01f;
    public float radius = 3f;
    private int Size;
   // private LineRenderer LineDrawer;
    private float Theta = 0f;

    private void Start()
    {
    

        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);

        positions = new Vector3[Size];

        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float y = radius * Mathf.Sin(Theta);
          
            positions[i] =gameObject.transform.position+ new Vector3(x, 0, y);
        }

        gamemanager.GM.pathmanager.DrawRail(positions);
    }
    private void FixedUpdate()
    {
        rotateCenter.Rotate(Vector3.up, 2f);
    }
}
