using UnityEngine;
using System.Collections;

public class Tumps : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        AudioManager.Instance.Tumps();
        Debug.Log("Kaboom!");
    }
}
