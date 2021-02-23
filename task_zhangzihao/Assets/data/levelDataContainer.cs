using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Level_data", order = 0)]
public class levelDataContainer : ScriptableObject
{

    public Vector3[] positions;
    public int rail_start;
    public int rail_destination;
    public int rail_end;
    public int pointsCount;
    public EnemyData[] enemies;

   
}
