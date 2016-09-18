using UnityEngine;
using System.Collections;

public class Bojoing : MonoBehaviour
{
    void OnCollisionExit2D(Collision2D coll)
    {
        AudioManager.Instance.BananaBoing();
    }
}
