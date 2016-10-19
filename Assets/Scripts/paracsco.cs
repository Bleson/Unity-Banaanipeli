using UnityEngine;
using System.Collections;

public class paracsco : MonoBehaviour {

    public float multiplier = 1f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(Kamera.Instance.transform.position.x * multiplier, transform.position.y, transform.position.z);
	}
}
