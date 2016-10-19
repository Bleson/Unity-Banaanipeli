using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*
    Data saving IDs ():
    1 - Xbox
    2 - Aito
    3 - Kuorittu
    4 - Schoko
    5 - Pixel
    6 - Vauva
    7 - Oranssi
    8 - Äitile
    9 - Ananas
    10 - Wooden
    11 - Phone
    12 - Metal
    255 - Rare
 *  "Total distance" - Total distance traveled
 *  "Games played" - Games played
 */

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
    public int pisteet = 0;
    public int maxPisteet = 0;
    public int personalBest = 0;

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
    TimerWarning timeWarning;
    public Text bpGameplay;
    public Text bpGameOver;
    GameObject newbpGameOver;
    NameDisplay nameDisplay;
    #endregion
    //-------------------------------------------------------------
    #region Unity Events
    void Awake()
    {
        Init();
    }

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Application.LoadLevel(1);
            StartGame();
        }
	}

    #endregion
    //-------------------------------------------------------------
    //Game Events
    void Init()
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
        if (!timeWarning)
        {
            timeWarning = GameObject.FindObjectOfType<TimerWarning>();
        }
        if (!bpGameplay)
        {
            bpGameplay = GetTextByObjectName("PbDisplay-GP");
        }
        if (!bpGameOver)
        {
            bpGameOver = GetTextByObjectName("PBText-GO");
        }
        if (!newbpGameOver)
        {
            newbpGameOver = GameObject.Find("NewPBText-GO");
        }
        if (!nameDisplay)
        {
            nameDisplay = GameObject.FindObjectOfType<NameDisplay>();
        }
    }

    public void StartGame()
    {
        pisteet = 0;
        maxPisteet = 0;
        currentCheckpoint = null;

        lost = false;

        GameUI.SetActive(true);
        GameOverUI.SetActive(false);

        BanaaninSpawnaus();

        currentTimeToLose = startingTime;
        foreach (Checkpoint cp in GameObject.FindObjectsOfType<Checkpoint>())
        {
            cp.activated = false;
        }

        if (timeWarning)
        {
            timeWarning.TurnOff();
        }
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
        timeWarning.TurnOff();
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
            timeWarning.UpdateTime(ref currentTimeToLose);

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
        EnnätysCheck();
        LisääPeli();
    }

    #endregion
    //-------------------------------------------------------------
    #region Banaanin spawnaus
    void BanaaninSpawnaus()
    {
        if (tämänHetkinenBanaani)
        {
            Destroy(tämänHetkinenBanaani.gameObject);
            tämänHetkinenBanaani = null;
        }
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
        LataaEnnätys();
        nameDisplay.ShowName(tämänHetkinenBanaani.nimi);
    }

    Banaani SpawnaaBanaani(Banaani banaani)
    {
        Transform kameraTransformi = Kamera.Instance.transform;
        GameObject banaani_ = (GameObject)Instantiate(banaani, bananaSpawningPosition, Quaternion.Euler(Vector3.zero));
        return banaani_.GetComponent<Banaani>();
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

    void EnnätysCheck()
    {
        if (pisteet > personalBest)
        {
            EnnätysTallennus();
            newbpGameOver.SetActive(true);
        }
        else
        {
            newbpGameOver.SetActive(false); 
        }
    }

    void EnnätysTallennus()
    {
        personalBest = pisteet;
        PlayerPrefs.SetInt(tämänHetkinenBanaani.id.ToString(), personalBest);
        bpGameplay.text = Tekstikirjasto.TEXT_BESTSCORE_PRETEXT + personalBest.ToString();
        bpGameOver.text = Tekstikirjasto.TEXT_BESTSCORE_PRETEXT + personalBest.ToString();
    }

    void LataaEnnätys()
    {
        personalBest = PlayerPrefs.GetInt(tämänHetkinenBanaani.id.ToString());
        bpGameplay.text = Tekstikirjasto.TEXT_BESTSCORE_PRETEXT + personalBest.ToString();
        bpGameOver.text = Tekstikirjasto.TEXT_BESTSCORE_PRETEXT + personalBest.ToString();
    }

    void LisääPeli()
    {
        int gamesPlayed = PlayerPrefs.GetInt("Games played") + 1;
        PlayerPrefs.SetInt("Games played", gamesPlayed);

        int totalDistance = PlayerPrefs.GetInt("Total distance") + Mathf.Abs(pisteet);
        PlayerPrefs.SetInt("Total distance", totalDistance);
        PlayerPrefs.Save();
    }
}
