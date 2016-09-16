using UnityEngine;
using System.Collections;

public class Banaani : MonoBehaviour 
{
    public float vääntövoima = 3f;

    Rigidbody2D kaksiulotteinenJämäkkäKeho;
    
    //-----------------------------------------------------------------
    void Awake()
    {
        if (!kaksiulotteinenJämäkkäKeho)
        {
            kaksiulotteinenJämäkkäKeho = GetComponent<Rigidbody2D>();
        }
    }

	void FixedUpdate () 
    {
	
	}

    internal void KäännäBanaania()
    {
        kaksiulotteinenJämäkkäKeho.AddTorque(vääntövoima * Time.deltaTime, ForceMode2D.Impulse);
    }


}
