using UnityEngine;
using System.Collections;

public class PyoriNyt : MonoBehaviour
{
    Vector2 suunta;
    public float nopeus = 10f;

    void Start()
    {
        suunta = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void Update()
    {
        transform.Rotate(Vector3.forward);
        transform.localPosition = new Vector2 (transform.localPosition.x + suunta.x * nopeus * Time.deltaTime, transform.localPosition.y + suunta.y * nopeus * Time.deltaTime);
        TarkistaRajat();
    }

    void TarkistaRajat()
    {
        if (transform.localPosition.x > 1005)
        {
            float y = transform.localPosition.y;
            transform.localPosition = new Vector2(-1000f, y);
        }
        else if (transform.localPosition.x < -1005)
        {
            float y = transform.localPosition.y;
            transform.localPosition = new Vector2(1000, y);
        }
        else if (transform.localPosition.y > 535)
        {
            float x = transform.localPosition.x;
            transform.localPosition = new Vector2(-530, x);
        }
        else if (transform.localPosition.y < -535)
        {
            float x = transform.localPosition.x;
            transform.localPosition = new Vector2(530, x);
        }

    }
}
