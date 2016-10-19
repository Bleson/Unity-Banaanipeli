using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : Singleton<LevelGenerator> {
    protected LevelGenerator() { }

    enum E_FloorDirection
    {
        Left,
        Right
    }

    class LattiaPala
    {
        public GameObject gO;
        public float width;

        public LattiaPala(GameObject gameObject, float width)
        {
            this.gO = gameObject;
            this.width = width;
        }
    }
    //-------------------------------------------------------------
    Transform environmentParent;
    public float levelStartingHeight = 0f;
    List<LattiaPala> _lattiapalat = new List<LattiaPala>();
    public List<GameObject> lattiapalat = new List<GameObject>();
    public float lattiapalatLeveys = 17.56f;

    public GameObject checkpoint;
    Checkpoint checkpointClass;

    public Vector2 floorEndingLeft = Vector2.zero;
    public Vector2 floorEndingRight = Vector2.zero;

    float distanceToSpawnNewFloor = 30f;
    //-------------------------------------------------------------

    void Awake()
    {
        if (!environmentParent)
        {
            environmentParent = GameObject.Find("Ympäristö").transform;
        }
        if (!checkpointClass)
        {
            if (checkpoint)
            {
                checkpointClass = checkpoint.GetComponent<Checkpoint>();
            }
            else
            {
                print("MISSING CHECKPOINT");
            }
        }
        CombineLists();
        SpawnFirstFloors();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(PeliManageri.Instance.tämänHetkinenBanaani.transform.position.x) + distanceToSpawnNewFloor > floorEndingRight.x)
        {
            SpawnFloor(E_FloorDirection.Right);
        }
        if (PeliManageri.Instance.tämänHetkinenBanaani.transform.position.x - distanceToSpawnNewFloor < floorEndingLeft.x)
        {
            SpawnFloor(E_FloorDirection.Left);
        }
    }
    
    void CombineLists()
    {
        _lattiapalat.Clear();
        for (int i = 0; i < lattiapalat.Count; i++)
        {
            _lattiapalat.Add(new LattiaPala(lattiapalat[i], lattiapalatLeveys));
        }
    }

    void SpawnFirstFloors()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnFloor(E_FloorDirection.Left);
            SpawnFloor(E_FloorDirection.Right);
        }
    }

    void SpawnFloor(E_FloorDirection direction)
    {
        LattiaPala lattiaPalaToUse = _lattiapalat[Random.Range(0,_lattiapalat.Count)];
        switch (direction)
        {
            case E_FloorDirection.Left:
                SpawnFloor(lattiaPalaToUse, new Vector3(floorEndingLeft.x - lattiaPalaToUse.width / 2f, levelStartingHeight, 0f));
                floorEndingLeft.x -= lattiaPalaToUse.width;
                SpawnCheckPoint(floorEndingLeft);
                break;
            case E_FloorDirection.Right:
                SpawnFloor(lattiaPalaToUse, new Vector3(floorEndingRight.x + lattiaPalaToUse.width / 2f, levelStartingHeight, 0f));
                floorEndingRight.x += lattiaPalaToUse.width;
                SpawnCheckPoint(floorEndingRight);
                break;
            default:
                break;
        }
    }

    void SpawnFloor(LattiaPala floorToSpawn, Vector3 location)
    {
        GameObject go = (GameObject)Instantiate(floorToSpawn.gO, location, Quaternion.Euler(Vector3.zero));
        go.transform.parent = environmentParent;
    }

    void SpawnCheckPoint(Vector2 location)
    {
        Instantiate(checkpoint, new Vector2(location.x, location.y + checkpointClass.OffsetY), Quaternion.Euler(Vector3.zero));
    }
}
