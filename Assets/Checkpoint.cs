using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour 
{
    SpriteRenderer sRenderer;

    public bool activated = false;

    int blinkingTimes = 3;
    float timeBetweenColorChanges = 0.2f;
    public Color blinkingColor = new Color(1f, 1f, 1f, 0.3f);
    Color originalColor = Color.white;

    public float OffsetY = 2.4f;

    //-------------------------------------------------------------
    void Awake()
    {
        if (!sRenderer)
        {
            sRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated)
        {
            if (other.GetComponent<Banaani>())
            {
                Activate();
            }
        }
    }
    //-------------------------------------------------------------

    void Activate()
    {
        activated = true;
        PeliManageri.Instance.currentCheckpoint = this;
        PeliManageri.Instance.AddTime();
        PeliManageri.Instance.tämänHetkinenBanaani.Refresh();
        StartCoroutine(E_Blinking());
    }

    IEnumerator E_Blinking()
    {
        for (int i = 0; i < blinkingTimes; i++)
        {
            sRenderer.color = blinkingColor;
            yield return new WaitForSeconds(timeBetweenColorChanges);
            sRenderer.color = originalColor;
            yield return new WaitForSeconds(timeBetweenColorChanges);
        }
        yield return null;
    }
}
