using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Banaani : MonoBehaviour 
{
    public float vääntövoima = 3f;
    public float tönäsyvoima = 0.3f;
    public float maksimiVääntöaika = 1f;
    public float jäljelläOlevaVääntöaika;
    public float vääntövoimanLatausKerroin = 2f;

    Rigidbody2D kaksiulotteinenJämäkkäKeho;
    
    //-----------------------------------------------------------------
    void Awake()
    {
        if (!kaksiulotteinenJämäkkäKeho)
        {
            kaksiulotteinenJämäkkäKeho = GetComponent<Rigidbody2D>();
        }
        jäljelläOlevaVääntöaika = maksimiVääntöaika;
    }

	void FixedUpdate () 
    {
	    
	}

    internal void PäivitäKontrollit()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (jäljelläOlevaVääntöaika > 0f)
            {
                jäljelläOlevaVääntöaika = Mathf.Clamp(jäljelläOlevaVääntöaika - Time.deltaTime, 0f, maksimiVääntöaika);
                KäännäBanaania();
            }
        }
        else
        {
            jäljelläOlevaVääntöaika = Mathf.Clamp(jäljelläOlevaVääntöaika + Time.deltaTime * vääntövoimanLatausKerroin, 0f, maksimiVääntöaika);
        }
    }

    internal void KäännäBanaania()
    {
        kaksiulotteinenJämäkkäKeho.AddTorque(vääntövoima * Time.deltaTime, ForceMode2D.Impulse);
        kaksiulotteinenJämäkkäKeho.AddForce(new Vector2(tönäsyvoima, 0f), ForceMode2D.Force);
    }

}
