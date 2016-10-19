using UnityEngine;
using System.Collections;

public class Audio_Bojoing : MonoBehaviour
{
    void OnCollisionExit2D(Collision2D coll)
    {
        AudioManager.Instance.BananaBoing();
    }
}
