using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    AudioSource musicManager;
    AudioSource bananaBoing;
    AudioSource newHighScore;
    AudioSource pikseliBanaani;
    AudioSource tumps;
    AudioSource metalliBanaani;
    AudioSource paperi;
    AudioSource menuButton1;
    AudioSource menuButton2;
    AudioSource vauhtiViisu;

    void Start()
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
