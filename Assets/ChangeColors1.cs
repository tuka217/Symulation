using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeColors1 : MonoBehaviour {

    private float cachedY;
    private float minXValue;
    private float maxXValue;
    private float currentHeat;
    private float quarterTmp;
    private Color[] colors;
    private float[] heats;
    public int maxHeat;
    public 
    float G;
    float B;
    float R;
	// Use this for initialization
    float zmienna = 1.0f;
	void Start () {
        maxHeat = 100;
        cachedY = 0;
        maxXValue = 840;
        minXValue = 0;
        currentHeat = 100;
        quarterTmp = maxHeat / 4;
        R = 1.0f;
        G = 0;
        B = 0;
        heats = new float[1000];
	}
	
    private float CurrentHeat
    {
        get { return currentHeat; }
        set { 
                currentHeat = value;
               // HandleHeat();
            }
    }
	// Update is called once per frame
    void Update()
    {
        currentHeat -= 0.01f;
        heats[0] = currentHeat;
        for (int i = 1; i < 1000; i++)
        {
            if (currentHeat - 0.1f >= 0)
            {
                heats[i] = currentHeat - (1 * i);
            }
            else
            {
                heats[i] = 0;
            }
        }
        //HandleHeat();
        if (currentHeat <= 0)
        {
            currentHeat = 0;
        }
        changeHeatToRGBColor(heats);
    }

    private void changeHeatToRGBColor(float[] heats)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        for (int i = 0; i < 1000; i++)
        {
            string name = "_Colors" + i;
            Color color = new Color(0, 0, 0);
            if (heats[i] <= quarterTmp)
            {
                color = upG(currentHeat);
                renderer.material.SetColor(name, color);
            }
            else if (heats[i] > quarterTmp && heats[i] <= quarterTmp * 2)
            {
                color = downB(currentHeat);
                renderer.material.SetColor(name, color);
            }
            else if (heats[i] > quarterTmp * 2 && heats[i] <= quarterTmp * 3)
            {
                color = upR(currentHeat);
                renderer.material.SetColor(name, color);
            }
            else if (heats[i] > quarterTmp * 3 && heats[i] <= maxHeat)
            {
                color = downG(currentHeat);
                renderer.material.SetColor(name, color);
            }
        }
    }

    private Color upG(float currentHeat)
    {
        float step = (currentHeat * 0.01f) / (this.quarterTmp / 100);
        step = Mathf.Round(step * 100f) / 100f;
        G = step;
        G = Mathf.Round(G * 100f) / 100f;
        return new Color(R,G,B);
    }

    private Color downB(float currentHeat)
    {
        float step =( (currentHeat * 0.01f) / (this.quarterTmp / 100) ) -1;
        step = Mathf.Round(step * 100f) / 100f;
        B = 1 - step;
        B = Mathf.Round(B * 100f) / 100f;
        return new Color(R, G, B);
    }

    private Color upR(float currentHeat)
    {
        float step = (currentHeat * 0.01f) / (this.quarterTmp / 100);
        step = Mathf.Round(step * 100f) / 100f;
        R = step -2;
        R = Mathf.Round(R * 100f) / 100f;
        return new Color(R, G, B);
    }

    private Color downG(float currentHeat)
    {
        float step =( (currentHeat * 0.01f) / (this.quarterTmp / 100)) ;
        step = Mathf.Round(step * 100f) / 100f;
        G =  1-(step -3);
        G = Mathf.Round(G * 100f) / 100f;
        return new Color(R, G, B);
    }

}
