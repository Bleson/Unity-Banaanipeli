using UnityEngine;
using System.Collections;

public class Audio_MetalliBanaani : MonoBehaviour
{

    void OnCollisionEnter()
    {
        AudioManager.Instance.MetalliBanaani();
    }
}
