using UnityEngine;
using System.Collections;

public class Lippuscripti : MonoBehaviour
{
    Vector2 originalPosition;
    Vector2 targetPosition;
    float progress = 0;
    float nopeus = 6;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            originalPosition = transform.position;
            targetPosition = transform.position + new Vector3(0, 300);
            progress = 0;
        }

        progress += Time.deltaTime * nopeus;
        transform.position =  Vector2.Lerp(originalPosition, targetPosition, progress);
    }
}
