using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
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
        vauhtiViisu = transform.FindChild("VauhtiViisu").GetComponent<AudioSource>();
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
        bananaBoing.Play();
    }
    public void NewHighScore()
    {
        newHighScore.Play();
    }
    public void PikseliBanaani()
    {
        pikseliBanaani.Play();
    }
    public void Tumps()
    {
        tumps.Play();
    }
    public void MetalliBanaani()
    {
        metalliBanaani.Play();
    }
    public void Paperi()
    {
        paperi.Play();
    }
    public void MenuButton1()
    {
        menuButton1.Play();
    }
    public void MenuButton2()
    {
        menuButton2.Play();
    }

    #endregion

}
