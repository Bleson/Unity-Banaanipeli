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

    public Rigidbody2D kaksiulotteinenJämäkkäKeho;
    
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

    internal virtual void PäivitäKontrollit()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (jäljelläOlevaVääntöaika > 0f)
            {
                jäljelläOlevaVääntöaika = Mathf.Clamp(jäljelläOlevaVääntöaika - Time.deltaTime, 0f, maksimiVääntöaika);
                KäännäBanaania();
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (jäljelläOlevaVääntöaika > 0f)
            {
                jäljelläOlevaVääntöaika = Mathf.Clamp(jäljelläOlevaVääntöaika - Time.deltaTime, 0f, maksimiVääntöaika);
                KäännäBanaania(-1f);
            }
        }
        else
        {
            jäljelläOlevaVääntöaika = Mathf.Clamp(jäljelläOlevaVääntöaika + Time.deltaTime * vääntövoimanLatausKerroin, 0f, maksimiVääntöaika);
        }
    }

    internal virtual void KäännäBanaania(float directionMultiplier = 1f)
    {
        kaksiulotteinenJämäkkäKeho.AddTorque(vääntövoima * Time.deltaTime * directionMultiplier, ForceMode2D.Impulse);
        kaksiulotteinenJämäkkäKeho.AddForce(new Vector2(tönäsyvoima * directionMultiplier, tönäsyvoima * directionMultiplier / 2f), ForceMode2D.Force);
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
        kaksiulotteinenJämäkkäKeho.velocity = Vector2.zero;
        kaksiulotteinenJämäkkäKeho.rotation = 0f;
    }

    internal void Refresh()
    {
        jäljelläOlevaVääntöaika = maksimiVääntöaika;
    }
}
