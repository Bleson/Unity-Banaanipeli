using UnityEngine;
using System.Collections;

public class Bojoing : MonoBehaviour
{

    void OnCollisionExit()
    {
        AudioManager.Instance.BananaBoing();
    }


}
