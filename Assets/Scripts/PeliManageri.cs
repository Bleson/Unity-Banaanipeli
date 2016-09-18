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
    public Vector3 bananaSpawningPosition = new Vector3(0f, 3f, 0f);

    [Header("Respawning")]
    public Checkpoint currentCheckpoint;
    int pisteet = 0;
    int maxPisteet = 0;

    //Losing
    [Header("Häviäminen")]
    public bool canLose = true;
    public bool lost = false;
    public float startingTime = 15f;
    public float maxTimeToLose = 60f;
    public float currentTimeToLose = 0f;
    public float timeToAdd = 10f;

    //UI
    GameObject GameOverUI;
    GameObject GameUI;
    Text pisteTekstiGameplay;
    Text pisteTekstiGameOver;
    Text timeDisplay;
    Text bpGameplay;
    Text bpGameOver;
    #endregion
    //-------------------------------------------------------------
    #region Unity Events
    void Awake()
    {
        if (!GameOverUI)
        {
            GameOverUI = GameObject.Find("GameOverUI");
        }
        if (!GameUI)
        {
            GameUI = GameObject.Find("GameplayUI");
        }
        if (!pisteTekstiGameplay)
        {
            pisteTekstiGameplay = GetTextByObjectName("ScoreDisplay-GP");
        }
        if (!pisteTekstiGameOver)
        {
            pisteTekstiGameOver = GetTextByObjectName("ScoreDisplay-GO");
        }
        if (!timeDisplay)
        {
            timeDisplay = GetTextByObjectName("Timer-GP");
        }
        if (!bpGameplay)
        {
            bpGameplay = GetTextByObjectName("PBText-GP");
        }
        if (!bpGameOver)
        {
            bpGameOver = GetTextByObjectName("PBText-GO");
        }

        StartGame();
    }

	void Update () 
    {
        if (!lost)
        {
            tämänHetkinenBanaani.PäivitäKontrollit();
            TarkastaHäviö();
            PäivitäPisteet();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartToLastCheckpoint();
        }
	}

    #endregion
    //-------------------------------------------------------------
    //Game Events
    public void StartGame()
    {
        pisteet = 0;
        maxPisteet = 0;

        lost = false;

        GameUI.SetActive(true);
        GameOverUI.SetActive(false);
        
        if (tämänHetkinenBanaani)
        {
            Destroy(tämänHetkinenBanaani.gameObject);
            tämänHetkinenBanaani = null;
        }
        BanaaninSpawnaus();
        currentTimeToLose = startingTime;
    }

    public void RestartToLastCheckpoint()
    {
        if (currentCheckpoint)
        {
            tämänHetkinenBanaani.Teleport(currentCheckpoint.transform.position);
        }
    }

    public void AddTime()
    {
        currentTimeToLose = Mathf.Clamp(currentTimeToLose + timeToAdd, 0f, maxTimeToLose);
    }
    //-------------------------------------------------------------
    #region Päivityksiä
    void PäivitäPisteet()
    {
        if (pisteet != (int)tämänHetkinenBanaani.transform.position.x)
	    {
		    pisteet = (int)tämänHetkinenBanaani.transform.position.x;
            if (pisteTekstiGameplay)
            {
                pisteTekstiGameplay.text = Tekstikirjasto.TEXT_SCORE_PRETEXT_GP + pisteet.ToString();
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
            //if ((int)tämänHetkinenBanaani.transform.position.x <= maxPisteet)
            //{
            //    currentTimeToLose -= Time.deltaTime;
            //    if (currentTimeToLose <= 0f)
            //    {
            //        Häviä();
            //    }
            //}
            //else
            //{
            //    currentTimeToLose = maxTimeToLose;
            //}

            currentTimeToLose -= Time.deltaTime;
            timeDisplay.text = Mathf.RoundToInt(currentTimeToLose).ToString() + Tekstikirjasto.TEXT_TIME_POSTTEXT;
            if (currentTimeToLose <= 0f)
            {
                Häviä();
            }
        }
    }

    void Häviä()
    {
        lost = true;
        GameUI.SetActive(false);
        GameOverUI.SetActive(true);
        pisteTekstiGameOver.text = Tekstikirjasto.TEXT_SCORE_PRETEXT_GO + maxPisteet.ToString();
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
        GameObject banaani = (GameObject)Instantiate(RandomBanaani(), bananaSpawningPosition, Quaternion.Euler(Vector3.zero));
        return banaani.GetComponent<Banaani>();
    }

    GameObject RandomBanaani()
    {
        return kaikkiBanaanit[Random.Range(0, kaikkiBanaanit.Count)];
    }

    #endregion

    Text GetTextByObjectName(string objectName)
    {
        GameObject go = GameObject.Find(objectName);
        if (go)
        {
            return go.GetComponent<Text>();
        }
        else
        {
            return null;
        }
    }
}
