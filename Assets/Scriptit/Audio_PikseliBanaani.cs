using UnityEngine;
using System.Collections;

public class Audio_PikseliBanaani : MonoBehaviour
{

    void OnCollisionEnter()
    {
        AudioManager.Instance.PikseliBanaani();
    }
}
