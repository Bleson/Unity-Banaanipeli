using UnityEngine;
using System.Collections;

public class Kamera : MonoBehaviour 
{
    new Transform transform;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Transform seurattavaBanaani = PeliManageri.Instance.tämänHetkinenBanaani.transform;
        transform.position = new Vector3(seurattavaBanaani.position.x, seurattavaBanaani.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
