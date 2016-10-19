using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(Outline))]
public class LerpTransparency : MonoBehaviour {

    protected Text text;
    Outline outline;

    public bool changeToTransparent;
    public float delay = 1f;
    public float lerpTime = 2f;
    public float currentTime = 0f;

	// Use this for initialization
	void Awake () 
    {
        text = GetComponent<Text>();
        outline = GetComponent<Outline>();
        StartCoroutine(C_TurnTransparent());
	}
	
    void OnEnable()
    {
        Refresh();
    }

	// Update is called once per frame
	void Update () 
    {
	    if (changeToTransparent && lerpTime != 0)
	    {
            currentTime += Time.deltaTime;
            if (currentTime < lerpTime)
            {
                text.color = ColorWithTransparency(text.color, 1 - currentTime / lerpTime);
                outline.effectColor = ColorWithTransparency(outline.effectColor, 1 - currentTime / lerpTime);
            }
            else
            {
                text.color = ColorWithTransparency(text.color, 0f);
                currentTime = 0f;
                changeToTransparent = false;
            }
	    }
	}

    Color ColorWithTransparency(Color color, float newTransparency)
    {
        return new Color(color.r, color.g, color.b, newTransparency);
    }

    protected void Refresh()
    {
        if (text && outline)
        {
            text.color = ColorWithTransparency(text.color, 1f);
            outline.effectColor = ColorWithTransparency(outline.effectColor, 1f);
            changeToTransparent = false;
            StopAllCoroutines();
            StartCoroutine(C_TurnTransparent());
            currentTime = 0f;
        }
    }

    IEnumerator C_TurnTransparent()
    {
        yield return new WaitForSeconds(delay);
        changeToTransparent = true;
        yield return null;
    }
}
