using UnityEngine;
using System.Collections;

public class Tumps : MonoBehaviour {

    void OnCollisionExit2D(Collision2D coll)
    {
        AudioManager.Instance.Tumps();
    }
}
