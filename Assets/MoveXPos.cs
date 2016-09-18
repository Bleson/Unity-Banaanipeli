using UnityEngine;
using System.Collections;

public class MoveXPos : MonoBehaviour {

    new Transform transform;

    void Awake()
    {
        transform = GetComponent<Transform>();
    }

	// Update is called once per frame
	void Update () 
    {
        transform.position = new Vector3(Kamera.Instance.transform.position.x, transform.position.y, transform.position.z);
	}
}
