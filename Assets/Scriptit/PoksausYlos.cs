using UnityEngine;
using System.Collections;

public class PoksausYlos : MonoBehaviour
{
    #region Muuttujat

    TextMesh tekstiMeshi;

    public float nopeus = 60;
    public float elinaika = 2;

    public bool valkkyvaTeksti = true;
    public float valkyntaNopeus = 1f;

    #endregion

    void Start()
    {
        tekstiMeshi = transform.GetComponent<TextMesh>();
    }

    void OnEnable()
    {
        Invoke("VammautaObjekti", elinaika);

        if (valkkyvaTeksti)
        {
            ValkkyvaTeksti();
        }
    }

    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * nopeus);
    }

    #region Valkkyva Teksti

    void ValkkyvaTeksti()
    {
        InvokeRepeating("TekstiEnsimmaiseksiVariksi", 0f, valkyntaNopeus);
        InvokeRepeating("TekstiToiseksiVariksi", valkyntaNopeus/3, valkyntaNopeus);
        InvokeRepeating("TekstiKolmanneksiVariksi", valkyntaNopeus*2/3, valkyntaNopeus);
    }

    void TekstiEnsimmaiseksiVariksi()
    {
        tekstiMeshi.color = new Color(255, 0, 0);
    }

    void TekstiToiseksiVariksi()
    {
        tekstiMeshi.color = new Color(0, 255, 0);
    }

    void TekstiKolmanneksiVariksi()
    {
        tekstiMeshi.color = new Color(0, 0, 255);
    }

    #endregion

    void VammautaObjekti()
    {
        gameObject.SetActive(false);
    }
}
