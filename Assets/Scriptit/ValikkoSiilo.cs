using UnityEngine;
using System.Collections;

public class ValikkoSiilo : MonoBehaviour
{
    [SerializeField] GameObject PaaValikko;
    [SerializeField] GameObject LuottoRuutu;

    void Start()
    {
        PaaValikko = transform.FindChild("Panel").FindChild("PaaValikko").gameObject;
        LuottoRuutu = transform.FindChild("Panel").FindChild("LuottoRuutu").gameObject;

        PaaValikko.gameObject.SetActive(true);
    }

    public void PolkaisePeliKayntiin()
    {
        Application.LoadLevel(1);
        //Debug.Log("Olis varmaan kiva pelatakki jotain...");
    }

    public void SiirryPaaValikkoon()
    {
        PaaValikko.SetActive(true);
        LuottoRuutu.SetActive(false);
    }

    public void SiirryLuottoRuutuun()
    {
        PaaValikko.SetActive(false);
        LuottoRuutu.SetActive(true);
    }

    public void LopetaPeli()
    {
        Debug.Log("Hyvästi Tuulenpesä!");
        Application.Quit();
        
    }

}
