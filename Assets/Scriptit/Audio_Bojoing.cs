using UnityEngine;
using System.Collections;

public class Audio_Bojoing : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.relativeVelocity.magnitude > 5)
        {
            AudioManager.Instance.BananaBoing();
        }
    }
}
