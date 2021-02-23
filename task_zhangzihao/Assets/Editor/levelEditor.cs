using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class levelEditor : EditorWindow
{
    //displayed fields
    //target asset
    public levelDataContainer level_asset;
    public pathmanager pathmanager;
    //standard formation anchor
    public GameObject point0;
    public GameObject point1;
    public GameObject point2;
    //setup parameters

    //indicator
    public LineRenderer line;
    public bool displayLine;
    public bool updatePath;


    //mechanic fields
    Vector3[] _positions;
    List<GameObject> _generated = new List<GameObject>();
    //scroll view mechanic
    Vector2 scrollPosition = Vector2.zero;

    //define window
    [MenuItem("Window/assetMakerTool_Scene")]
    static void Init()
    {
        levelEditor window = (levelEditor)EditorWindow.GetWindow(typeof(levelEditor));
        window.Show();

        
    }

    protected void OnGUI()
    {
        //define scroll view
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

        //render field: formation asset
        EditorGUILayout.BeginHorizontal();
        level_asset = (levelDataContainer)EditorGUILayout.ObjectField("leveldata_asset", level_asset, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        //render field: formation asset
        EditorGUILayout.BeginHorizontal();
        pathmanager = (pathmanager)EditorGUILayout.ObjectField("pathmanager", pathmanager, typeof(pathmanager), true);
        EditorGUILayout.EndHorizontal();

        //render field: formation standard anchor
        EditorGUILayout.BeginHorizontal();
        point0 = (UnityEngine.GameObject)EditorGUILayout.ObjectField("point0", point0, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        //render field: formation standard anchor
        EditorGUILayout.BeginHorizontal();
        point1 = (UnityEngine.GameObject)EditorGUILayout.ObjectField("point1", point1, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        //render field: formation standard anchor
        EditorGUILayout.BeginHorizontal();
        point2 = (UnityEngine.GameObject)EditorGUILayout.ObjectField("point2", point2, typeof(Object), true);
        EditorGUILayout.EndHorizontal();

        //render field: formation standard anchor
        EditorGUILayout.BeginHorizontal();
        line = (LineRenderer)EditorGUILayout.ObjectField("line", line, typeof(LineRenderer), true);
        EditorGUILayout.EndHorizontal();

        //render field: formation standard anchor
        EditorGUILayout.BeginHorizontal();
        displayLine = (bool)EditorGUILayout.Toggle("displayLine", displayLine);
        EditorGUILayout.EndHorizontal();

        //render field: formation standard anchor
        EditorGUILayout.BeginHorizontal();
        updatePath = (bool)EditorGUILayout.Toggle("updatePath", updatePath);
        EditorGUILayout.EndHorizontal();


        ScriptableObject scriptableObj = this;
        SerializedObject serialObj = new SerializedObject(scriptableObj);
        SerializedProperty serialProp = serialObj.FindProperty("controlPointsList");

        EditorGUILayout.PropertyField(serialProp, true);
        serialObj.ApplyModifiedProperties();

      
        //button: record data
        if ((GUILayout.Button("Generate path")))
        {
            DrawCurve();
        }
        if ((GUILayout.Button("Destroy path")))
        {
            DestroyPath();
        }
        //button: record data
        if ((GUILayout.Button("[write]record onto asset")))
        {
            Vector3[] _combined = new Vector3[level_asset.positions.Length + _positions.Length];
            level_asset.positions.CopyTo(_combined, 0);
            _positions.CopyTo(_combined, level_asset.positions.Length);
            level_asset.positions = _combined;

            point0.transform.position = point2.transform.position;
            point2.transform.position += new Vector3(0, 0, 5f);
            point1.transform.position = point0.transform.position + new Vector3(3f, 0, 3f);
            //set dirty, if not the data in scriptable object will be lost on unity restart
            EditorUtility.SetDirty(level_asset);
        }

        //button: record data
        if ((GUILayout.Button("[read]build level from asset")))
        {
            pathmanager.InitializeLevelSetup(level_asset);

        }

        //button: record data
        if ((GUILayout.Button("Add Point")))
        {
            GameObject _newPoint = Instantiate(controlPointsList[0].gameObject);
            _newPoint.transform.position = controlPointsList[controlPointsList.Length - 1].position;
            _newPoint.transform.rotation = controlPointsList[controlPointsList.Length - 1].rotation;
            _newPoint.transform.localScale = controlPointsList[controlPointsList.Length - 1].localScale;

            _newPoint.transform.position += new Vector3(0, 0, 1);

            Transform[] _combined = new Transform[controlPointsList.Length + 1];
            controlPointsList.CopyTo(_combined, 0);
            _combined[_combined.Length-1] = _newPoint.transform;
            controlPointsList = _combined;

        }

        //button: record data
        if ((GUILayout.Button("[write]new record curve")))
        {
            //pathmanager.InitializeLevelSetup(level_asset);
           
            level_asset.positions = CaculateCurve();
            EditorUtility.SetDirty(level_asset);
        }
        //end of scroll view
        GUILayout.EndScrollView();
    }
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(updatePath)
        {
            CaculateCurve();
        }

      
        if (displayLine)
        {
            line.positionCount = _positions.Length;
            int pointsCount = level_asset.pointsCount;
            _positions = new Vector3[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                float t = i / (float)pointsCount;
                if (i == 0)
                {
                    _positions[i] = CalculateBezierPoints(t, point0.transform.position, point1.transform.position, point2.transform.position);
                }


                if (i != pointsCount - 1)
                {
                    float t_next = (i + 1) / (float)pointsCount;
                    _positions[i + 1] = CalculateBezierPoints(t, point0.transform.position, point1.transform.position, point2.transform.position);
                }

            }
            line.SetPositions(_positions);
        }
       
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
    private void DrawCurve()
    {
        Debug.Log("running");
        _generated.Clear();
        int pointsCount = level_asset.pointsCount;
        _positions = new Vector3[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)pointsCount;
            if (i == 0)
            {
                _positions[i] = CalculateBezierPoints(t, point0.transform.position, point1.transform.position, point2.transform.position);
            }


            if (i != pointsCount - 1)
            {
                float t_next = (i + 1) / (float)pointsCount;
                _positions[i + 1] = CalculateBezierPoints(t, point0.transform.position, point1.transform.position, point2.transform.position);
                DrawCube(_positions[i], _positions[i + 1]);
            }

        }
        DrawRail();
    }
    private void DrawCube(Vector3 pos, Vector3 pos_next)
    {
        GameObject cube = Instantiate(pathmanager.base_prefab);
        cube.transform.position = pos;
        cube.transform.LookAt(pos_next);
        cube.transform.position += new Vector3(0, -0.7f, 0);

        _generated.Add(cube);
    }
    private void DrawRail()
    {
        int _construct_from = 0;
        int _construct_to = 0;
        bool noend = true;
        if(level_asset.rail_start - level_asset.positions.Length >0)
        {
            _construct_from = level_asset.rail_start - level_asset.positions.Length;
        }
        else _construct_from = 0;

       
        if (level_asset.rail_end <= (level_asset.positions.Length))
        {
            _construct_to = level_asset.rail_end - (level_asset.positions.Length - _positions.Length);
            noend = false;
        }
        else
        {
            _construct_to = _positions.Length;
        }
        Debug.Log(_construct_from.ToString()+ "  " + _construct_to.ToString());
        for (int j = _construct_from; j < _construct_to; j++)
        {
            if (j != _construct_to - 1)
            {
                GameObject rail = Instantiate(pathmanager.rail_prefab);
                rail.transform.position = _positions[j];
                rail.transform.LookAt(_positions[j + 1]);
                _generated.Add(rail);
            }
            else
            {
                if(!noend)
                {
                    GameObject rail = Instantiate(pathmanager.rail_prefab);
                    rail.transform.position = _positions[j];
                    rail.transform.LookAt(point2.transform);
                    GameObject rail_end = Instantiate(pathmanager.rail_end_prefab);
                    rail_end.transform.position = _positions[j];
                    rail_end.transform.LookAt(point2.transform);
                    _generated.Add(rail_end);

                }
            }
        }
    }

    private void DestroyPath()
    {
        foreach(GameObject _g in _generated)
        {
            DestroyImmediate(_g);
        }
        _generated.Clear();
    }



    //Has to be at least 4 points
    public Transform[] controlPointsList;
    //Are we making a line or a loop?
    public bool isLooping = false;
    List<Vector3> positions = new List<Vector3>();
    //Display without having to press play
    Vector3[] CaculateCurve()
    {
       
        positions.Clear();
        Debug.Log("drawing gizmos");
        //Draw the Catmull-Rom spline between the points
        for (int i = 0; i < controlPointsList.Length; i++)
        {
            //Cant draw between the endpoints
            //Neither do we need to draw from the second to the last endpoint
            //...if we are not making a looping line
            if ((i == 0 || i == controlPointsList.Length - 2 || i == controlPointsList.Length - 1) && !isLooping)
            {
                continue;
            }

            DisplayCatmullRomSpline(i);
        }

        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());

        return positions.ToArray();
    }

    //Display a spline between 2 points derived with the Catmull-Rom spline algorithm
    void DisplayCatmullRomSpline(int pos)
    {
        //The 4 points we need to form a spline between p1 and p2
        Vector3 p0 = controlPointsList[ClampListPos(pos - 1)].position;
        Vector3 p1 = controlPointsList[pos].position;
        Vector3 p2 = controlPointsList[ClampListPos(pos + 1)].position;
        Vector3 p3 = controlPointsList[ClampListPos(pos + 2)].position;

        //The start position of the line
        Vector3 lastPos = p1;

        //The spline's resolution
        //Make sure it's is adding up to 1, so 0.3 will give a gap, but 0.2 will work
        float resolution = 0.0125f;//0.0025f;//0.2f;

        //How many times should we loop?
        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {
            //Which t position are we at?
            float t = i * resolution;

            //Find the coordinate between the end points with a Catmull-Rom spline
            Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);


            //Save this pos so we can draw the next line segment
            lastPos = newPos;

            positions.Add(lastPos);
        }
    }

    //Clamp the list positions to allow looping
    int ClampListPos(int pos)
    {
        if (pos < 0)
        {
            pos = controlPointsList.Length - 1;
        }

        if (pos > controlPointsList.Length)
        {
            pos = 1;
        }
        else if (pos > controlPointsList.Length - 1)
        {
            pos = 0;
        }

        return pos;
    }

    //Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
    Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        //The cubic polynomial: a + b * t + c * t^2 + d * t^3
        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }
}
