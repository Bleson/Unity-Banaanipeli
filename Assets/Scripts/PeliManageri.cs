using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PeliManageri : Singleton<PeliManageri> {
    protected PeliManageri() { }
    //-------------------------------------------------------------
    #region Variables
    [Header("Banaanit")]
    public bool spawnaaBanaani = false;
    public List<GameObject> kaikkiBanaanit = new List<GameObject>();
    public Banaani tämänHetkinenBanaani;
    float spawnattavanBanaaninOffsetY = 1f;

    int pisteet = 0;
    int maxPisteet = 0;

    //Losing
    [Header("Häviäminen")]
    public bool canLose = true;
    public bool lost = false;
    public float timeToLose = 5f;
    public float currentTimeToLose = 0f;

    //UI
    Text pisteTeksti;
    Text häviöTeksti;
    #endregion
    //-------------------------------------------------------------
    #region Unity Events
    void Awake()
    {
        BanaaninSpawnaus();

        if (!pisteTeksti)
        {
            GameObject pistetekstiPeliObjekti = GameObject.Find("PisteTeksti");
            if (pistetekstiPeliObjekti)
	        {
                pisteTeksti = pistetekstiPeliObjekti.GetComponent<Text>();
	        }
        }

        if (!häviöTeksti)
        {
            GameObject häviötekstiPeliObjekti = GameObject.Find("HäviöTeksti");
            if (häviötekstiPeliObjekti)
            {
                häviöTeksti = häviötekstiPeliObjekti.GetComponent<Text>();
            }
        }
        häviöTeksti.enabled = false;

        currentTimeToLose = timeToLose;
    }

	void Update () 
    {
        if (!lost)
        {
            tämänHetkinenBanaani.PäivitäKontrollit();
            TarkastaHäviö();
            PäivitäPisteet();
        }
	}

    #endregion
    //-------------------------------------------------------------
    #region Päivityksiä
    void PäivitäPisteet()
    {
        if (pisteet != (int)tämänHetkinenBanaani.transform.position.x)
	    {
		    pisteet = (int)tämänHetkinenBanaani.transform.position.x;
            if (pisteTeksti)
            {
                pisteTeksti.text = Tekstikirjasto.TEXT_SCORE_PRETEXT + pisteet.ToString();
            }
            if (pisteet > maxPisteet)
            {
                maxPisteet = pisteet;
            }
	    }
    }
    #endregion
    //-------------------------------------------------------------
    #region Lose check
    void TarkastaHäviö()
    {
        if (canLose)
        {
            if ((int)tämänHetkinenBanaani.transform.position.x <= maxPisteet)
            {
                currentTimeToLose -= Time.deltaTime;
                if (currentTimeToLose <= 0f)
                {
                    Häviä();
                }
            }
            else
            {
                currentTimeToLose = timeToLose;
            }
        }
    }

    void Häviä()
    {
        lost = true;
        print("LÄL HÄVISIT");
        häviöTeksti.enabled = true;
    }
    #endregion
    //-------------------------------------------------------------
    #region Banaanin spawnaus
    void BanaaninSpawnaus()
    {
        if (!tämänHetkinenBanaani)
        {
            if (spawnaaBanaani)
            {
                Banaani uusiBanaani = GameObject.FindObjectOfType<Banaani>();
                if (uusiBanaani)
                {
                    Destroy(uusiBanaani.gameObject);
                }
                tämänHetkinenBanaani = SpawnaaRandomBanaani();
            }
            else
            {
                Banaani uusiBanaani = GameObject.FindObjectOfType<Banaani>();
                if (uusiBanaani)
                {
                    tämänHetkinenBanaani = uusiBanaani;
                }
                else
                {
                    tämänHetkinenBanaani = SpawnaaRandomBanaani();
                }
            }
        }
    }

    Banaani SpawnaaRandomBanaani() 
    {
        Transform kameraTransformi = Kamera.Instance.transform;
        GameObject banaani = (GameObject)Instantiate(RandomBanaani(), new Vector3(kameraTransformi.position.x, kameraTransformi.position.y, 0f), Quaternion.Euler(Vector3.zero));
        return banaani.GetComponent<Banaani>();
    }

    GameObject RandomBanaani()
    {
        return kaikkiBanaanit[Random.Range(0, kaikkiBanaanit.Count)];
    }

    #endregion
}
