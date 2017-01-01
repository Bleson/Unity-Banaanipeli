using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    protected AudioManager() {}

    public bool musicOn = true;

    [SerializeField] AudioSource musicManager;
    [SerializeField] AudioSource bananaBoing;
    [SerializeField] AudioSource newHighScore;
    [SerializeField] AudioSource pikseliBanaani;
    [SerializeField] AudioSource tumps;
    [SerializeField] AudioSource metalliBanaani;
    [SerializeField] AudioSource paperi;
    [SerializeField] AudioSource menuButton1;
    [SerializeField] AudioSource menuButton2;
    [SerializeField] AudioSource vauhtiViisu;

    float sfxPauseTime = 0.5f;

    void Start()
    {
        LocateAudiosources();

        if (musicOn)
        {
            PlayMusic();
        }
    }

    void LocateAudiosources()
    {
        musicManager = transform.FindChild("MusicManager").GetComponent<AudioSource>();
        bananaBoing = transform.FindChild("BananaBoing").GetComponent<AudioSource>();
        newHighScore = transform.FindChild("NewHighScore").GetComponent<AudioSource>();
        pikseliBanaani = transform.FindChild("PikseliBanaani").GetComponent<AudioSource>();
        tumps = transform.FindChild("Tumps").GetComponent<AudioSource>();
        metalliBanaani = transform.FindChild("MetalliBanaani").GetComponent<AudioSource>();
        paperi = transform.FindChild("Paperi").GetComponent<AudioSource>();
        menuButton1 = transform.FindChild("MenuButton").GetComponent<AudioSource>();
        menuButton2 = transform.FindChild("MenuButton2").GetComponent<AudioSource>();
        if (!vauhtiViisu)
        {
            vauhtiViisu = transform.FindChild("VauhtiViisu").GetComponent<AudioSource>();
        }
    }

    #region Music

    public void PlayMusic()
    {
        musicManager.Play();
    }

    public void StopMusic()
    {
        musicManager.Stop();
    }

    #endregion

    #region SFX

    public void BananaBoing()
    {
        TryPlay(bananaBoing);
    }
    public void NewHighScore()
    {
        TryPlay(newHighScore);
    }
    public void PikseliBanaani()
    {
        TryPlay(pikseliBanaani);
    }
    public void Tumps()
    {
        TryPlay(tumps);
    }
    public void MetalliBanaani()
    {
        TryPlay(metalliBanaani);
    }
    public void Paperi()
    {
        TryPlay(paperi);
    }
    public void MenuButton1()
    {
        TryPlay(menuButton1);
    }
    public void MenuButton2()
    {
        TryPlay(menuButton2);
    }

    void TryPlay(AudioSource audioSource)
    {
        if (audioSource.time > sfxPauseTime || !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    #endregion

}
