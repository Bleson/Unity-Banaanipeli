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
    public List<float> lattiapalatLeveys = new List<float>();

    public float floorEndingLeft = 0f;
    public float floorEndingRight = 0f;

    float distanceToSpawnNewFloor = 30f;
    //-------------------------------------------------------------

    void Awake()
    {
        if (!environmentParent)
        {
            environmentParent = GameObject.Find("Ympäristö").transform;
        }
        CombineLists();
        SpawnFirstFloors();
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(PeliManageri.Instance.tämänHetkinenBanaani.transform.position.x) + distanceToSpawnNewFloor > floorEndingRight)
        {
            SpawnFloor(E_FloorDirection.Right);
        }
        if (PeliManageri.Instance.tämänHetkinenBanaani.transform.position.x - distanceToSpawnNewFloor < floorEndingLeft)
        {
            SpawnFloor(E_FloorDirection.Left);
        }
    }
    
    void CombineLists()
    {
        _lattiapalat.Clear();
        for (int i = 0; i < lattiapalat.Count; i++)
        {
            _lattiapalat.Add(new LattiaPala(lattiapalat[i], lattiapalatLeveys[i]));
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
        LattiaPala lattiaPalaToUse = _lattiapalat[0];
        switch (direction)
        {
            case E_FloorDirection.Left:
                SpawnFloor(lattiaPalaToUse, new Vector3(floorEndingLeft - lattiaPalaToUse.width / 2f, levelStartingHeight, 0f));
                floorEndingLeft -= lattiaPalaToUse.width;
                break;
            case E_FloorDirection.Right:
                SpawnFloor(lattiaPalaToUse, new Vector3(floorEndingRight + lattiaPalaToUse.width / 2f, levelStartingHeight, 0f));
                floorEndingRight += lattiaPalaToUse.width;
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
}
