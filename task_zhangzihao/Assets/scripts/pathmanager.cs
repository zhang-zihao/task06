using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathmanager : MonoBehaviour
{
    public LineRenderer line;
    public Vector3[] points;

    public Transform point0, point1, point2;

    [SerializeField]int pointsCount;
    public Vector3[] positions;

    public GameObject base_prefab;
    public GameObject rail_prefab;
    public GameObject rail_left_prefab;
    public GameObject rail_right_prefab;
    public GameObject rail_bottomonly_prefab;
    public GameObject rail_end_prefab;
    public GameObject finishingline_prefab;
    public GameObject chest;
    public List<GameObject> _generated_pool = new List<GameObject>();
    public levelDataContainer level_selected;
    [SerializeField] levelDataContainer[] level_assets;
    [SerializeField] GameObject[] enemy_prefabs;





    //obj pooler
    [Header("Object Pooler")]
    [HideInInspector] public List<GameObject> pooledobjects;  //object pool container
    public List<ObjectPoolItem> itemsToPool;  //object pool setup
    public Transform itemPool_Parent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PopulateLevel()
    {
        //points = new Vector3[line.positionCount];
        //line.GetPositions( points);

        //DrawCurve();
        InitializeLevelSetup(level_selected);
        gamemanager.GM.uimanager.controller.ReadyPlayer();
    }

    private Vector3 CalculateBezierPoints(float t, Vector3 p0, Vector3 p1, Vector3 p2_controlpoint)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2_controlpoint;
        return p;


    }

    //private void DrawCurve()
    //{
    //    line.positionCount = pointsCount;
    //    positions = new Vector3[pointsCount];
    //    for (int i = 0; i < pointsCount; i++)
    //    {
    //        float t = i / (float)pointsCount;
    //        if(i ==0)
    //        {
    //            positions[i] = CalculateBezierPoints(t, point0.position, point1.position, point2.position);
    //        }


    //        if(i!=pointsCount-1)
    //        {
    //            float t_next = (i+1) / (float)pointsCount;
    //            positions[i+1] = CalculateBezierPoints(t, point0.position, point1.position, point2.position);
    //            DrawCube(positions[i],positions[i+1]);
    //        }

    //    }
    //    line.SetPositions(positions);
    //}
    //private void DrawCube(Vector3 pos, Vector3 pos_next)
    //{
    //    GameObject cube = Instantiate(base_prefab);
    //    cube.transform.position = pos;
    //    cube.transform.LookAt(pos_next);
    //    cube.transform.position += new Vector3(0, -0.7f, 0);

    //}
    private void DrawCube(levelDataContainer _level)
    {
        for (int j = 0; j < _level.positions.Length; j++)
        {
            if (j != _level.positions.Length - 1)
            {
                GameObject cube = GetPooledObject("level_base");
                cube.transform.position = _level.positions[j];
                cube.transform.LookAt(_level.positions[j + 1]);
                cube.transform.position += new Vector3(0, -0.7f, 0);

                cube.SetActive(true);

                _generated_pool.Add(cube);
            }


        }
    }
    public void DrawRail(levelDataContainer _level)
    {
        for (int j = _level.rail_start; j < _level.rail_end; j++)
        {
            if (j != _level.rail_end - 1)
            {
                GameObject rail = GetPooledObject("level_rail_bottom");
                rail.transform.position = _level.positions[j];
                rail.transform.LookAt(_level.positions[j + 1]);

                GameObject rail_left = GetPooledObject("level_rail_left");
                rail_left.transform.position = _level.positions[j];
                rail_left.transform.LookAt(_level.positions[j + 1]);
                rail_left.transform.position += new Vector3(0, -0.05f, 0);

                GameObject rail_right = GetPooledObject("level_rail_right");
                rail_right.transform.position = _level.positions[j];
                rail_right.transform.LookAt(_level.positions[j + 1]);
                rail_right.transform.position += new Vector3(0, -0.05f, 0);

                rail.SetActive(true);
                rail_left.SetActive(true);
                rail_right.SetActive(true);

                _generated_pool.Add(rail);
                _generated_pool.Add(rail_left);
                _generated_pool.Add(rail_right);
            }
            else
            {
                GameObject rail = GetPooledObject("level_rail_bottom");
                rail.transform.position = _level.positions[j];
               
                GameObject rail_end = Instantiate(rail_end_prefab);
                rail_end.transform.position = _level.positions[j];
                if(j+1< _level.positions.Length)
                {
                    rail.transform.LookAt(_level.positions[j + 1]);
                    rail_end.transform.LookAt(_level.positions[j + 1]);
                }
                else 
                {
                    rail.transform.LookAt(point2.transform);
                    rail_end.transform.LookAt(point2.transform);
                }


                rail.SetActive(true);

                _generated_pool.Add(rail);
                _generated_pool.Add(rail_end);
            }

            if(j == _level.rail_destination)
            {
                GameObject finishingline = Instantiate(finishingline_prefab);
                finishingline.transform.position = _level.positions[j];
                if (j + 1 < _level.positions.Length)
                {
                    finishingline.transform.LookAt(_level.positions[j + 1]);
                    //finishingline.transform.Rotate(finishingline.transform.right, 90f);
                    finishingline.transform.rotation *= Quaternion.Euler(90f,0,0);
                }
                finishingline.transform.position += new Vector3(0, -0.17f, 0);
            }
           

        }
    }

    public void DrawRail(Vector3[] positions)
    {
        for (int j = 0; j < positions.Length-1; j++)
        {

                GameObject rail = GetPooledObject("level_rail_bottom");
                rail.transform.position = positions[j];
                rail.transform.LookAt(positions[j + 1]);

                GameObject rail_left = GetPooledObject("level_rail_left");
                rail_left.transform.position = positions[j];
                rail_left.transform.LookAt(positions[j + 1]);
                rail_left.transform.position += new Vector3(0, -0.05f, 0);

                GameObject rail_right = GetPooledObject("level_rail_right");
                rail_right.transform.position = positions[j];
                rail_right.transform.LookAt(positions[j + 1]);
                rail_right.transform.position += new Vector3(0, -0.05f, 0);

                rail.SetActive(true);
                rail_left.SetActive(true);
                rail_right.SetActive(true);

                _generated_pool.Add(rail);
                _generated_pool.Add(rail_left);
                _generated_pool.Add(rail_right);



        }
    }

    public void DrawEnemies(levelDataContainer _level)
    {
        foreach(EnemyData e in _level.enemies)
        {
            GameObject _enemy = Instantiate(enemy_prefabs[e.code]);
            _enemy.transform.position = e.pos;
            _enemy.transform.rotation = e.rot;
            _generated_pool.Add(_enemy);
        }
    }

    public void InitializeLevelSetup(levelDataContainer _level)
    {
        DrawCube(_level);
        DrawRail(_level);
        DrawEnemies(_level);


    }
    public void ClearLevelSetup(levelDataContainer _level)
    {
        for (int i = 0; i < _generated_pool.Count;i++)
        {
            _generated_pool[i].SetActive(false);
        }
        _generated_pool.Clear();

    }




    //object pooler
    public GameObject GetPooledObject(string tag)
    {
        //check all pooled objects
        for (int i = 0; i < pooledobjects.Count; i++)
        {
            //get the one that's inactive
            if (!pooledobjects[i].activeInHierarchy && pooledobjects[i].CompareTag(tag))
            {
                return pooledobjects[i];
            }
        }
        //if no inactive object then we instantiate one and expand the pool 
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objecttopool.CompareTag(tag))
            {

                GameObject obj = (GameObject)Instantiate(item.objecttopool);
                obj.SetActive(false);
                pooledobjects.Add(obj);
                return obj;
            }
        }
        //if there's no bugs this below 2 lines should never run
        Debug.LogError("bug in unitsmanager.GetPooledObject: got no object");
        return null;
    }
}
[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objecttopool;
    public int amounttopool;
}

[System.Serializable]
public class EnemyData
{
    [SerializeField] public int code;
    [SerializeField] public Vector3 pos;
    [SerializeField] public Quaternion rot;
}
