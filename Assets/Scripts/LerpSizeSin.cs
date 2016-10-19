using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LerpSizeSin : MonoBehaviour {

    RectTransform rect;
    protected Text text;

    float maxSize; //Max size is taken from text component.

    [Range(0f,1f)]
    public float minSizePercent;
    float minSize;

    public float speed = 2;
    float currentTime = 0f;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        if (text)
        {
            maxSize = text.fontSize;
        }
        else
        {
            Debug.LogWarning(gameObject.name + " is missing Text in LerpSizeSin!");
            this.enabled = false;
        }
        UpdateSizes();
	}

    void OnValidate()
    {
        UpdateSizes();
    }

	// Update is called once per frame
	virtual internal void Update () 
    {
        currentTime += Time.deltaTime * speed;
        text.fontSize = Mathf.RoundToInt(Mathf.Lerp(minSize, maxSize, Mathf.Abs(Mathf.Sin(currentTime))));
	}

    void UpdateSizes()
    {
        minSize = maxSize * minSizePercent;
    }
}
