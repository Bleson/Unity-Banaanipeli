using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeliManageri : Singleton<PeliManageri> {
    protected PeliManageri() { }

    int pisteet = 0;
    public Banaani tämänHetkinenBanaani;

    //UI
    Text pisteTeksti;

    void Awake()
    {
        if (!tämänHetkinenBanaani)
        {
            tämänHetkinenBanaani = GameObject.FindObjectOfType<Banaani>().GetComponent<Banaani>();
        }
        if (!pisteTeksti)
        {
            GameObject pistetekstiPeliObjekti = GameObject.Find("PisteTeksti");
            if (pistetekstiPeliObjekti)
	        {
                pisteTeksti = pistetekstiPeliObjekti.GetComponent<Text>();
	        }
        }
    }

	void Update () 
    {
        PäivitäKontrollit();
        PäivitäPisteet();
	}

    void PäivitäKontrollit()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            tämänHetkinenBanaani.KäännäBanaania();
        }
    }

    void PäivitäPisteet()
    {
        pisteet = (int)tämänHetkinenBanaani.transform.position.x;
        if (pisteTeksti)
        {
            pisteTeksti.text = Tekstikirjasto.TEXT_SCORE_PRETEXT + pisteet.ToString();
        }
    }
}
