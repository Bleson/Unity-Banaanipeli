using UnityEngine;
using System.Collections;

public class Kamera : Singleton<Kamera> 
{
    protected Kamera() { }

    new Transform transform;
    Camera kamera;

    public float minSpeed = 0.5f;
    public float maxSpeed = 4f;

    public float minFollowingPercent = 0.3f;
    public float maxFollowingPercent = 0.7f;

    public float minOffsetY = 0f;
    public float maxOffsetY = 5f;

    public float minSize = 1f;
    public float maxSize = 5f;

    float speedPercent = 0.4f;
    float speedChangingSpeed = 0.5f;

    void Start()
    {
        transform = GetComponent<Transform>();
        kamera = GetComponent<Camera>();
    }

    void Update()
    {
        Transform seurattavaBanaani = PeliManageri.Instance.tämänHetkinenBanaani.transform;

        float targetspeedPercent = Mathf.InverseLerp(minSpeed, maxSpeed, Mathf.Abs(seurattavaBanaani.GetComponent<Rigidbody2D>().velocity.magnitude));
        speedPercent = Mathf.Lerp(speedPercent, targetspeedPercent, speedChangingSpeed * Time.deltaTime);

        float followingSpeed = Mathf.Lerp(minFollowingPercent, maxFollowingPercent, targetspeedPercent) * Time.deltaTime;
        transform.position = Vector3.Lerp(
            transform.position, 
            new Vector3(
                seurattavaBanaani.position.x, 
                seurattavaBanaani.position.y + Mathf.Lerp(minOffsetY, maxOffsetY, speedPercent), 
                transform.position.z), 
            followingSpeed);

        transform.rotation = Quaternion.Euler(Vector3.zero);
        kamera.orthographicSize = Mathf.Lerp(minSize, maxSize, speedPercent * 1.5f);
        //kamera.orthographicSize = Mathf.Lerp(minSize, maxSize, Mathf.InverseLerp(0f, 6f, transform.position.y));
    }
}
