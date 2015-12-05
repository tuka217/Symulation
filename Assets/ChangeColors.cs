using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeColors : MonoBehaviour {

    private float cachedY;
    private float minXValue;
    private float maxXValue;
    private float currentHeat;
    private float quarterTmp;
    public int maxHeat;
    public Image visualHeat;
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
        visualHeat.color = new Color(1.0f, 0, 0);
        quarterTmp = maxHeat / 4;
        R = 1.0f;
        G = 0;
        B = 0;
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
	void Update () {
        currentHeat -= 0.01f;
        //HandleHeat();
        if (currentHeat <= 0)
        {
            currentHeat = 0;
        }
        changeHeatToRGBColor();
	}

    private void changeHeatToRGBColor()
    {
        if(currentHeat <= quarterTmp){
            visualHeat.color = upG(currentHeat);
        }
        else if(currentHeat > quarterTmp && currentHeat <= quarterTmp*2 ){
            visualHeat.color = downB(currentHeat);
        }
        else if(currentHeat > quarterTmp*2 && currentHeat <= quarterTmp*3){
            visualHeat.color = upR(currentHeat);
        }
        else if(currentHeat > quarterTmp*3 && currentHeat <= maxHeat){
            visualHeat.color = downG(currentHeat);
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
        Debug.Log("G " + G);
        Debug.Log("current " + currentHeat);
        return new Color(R, G, B);
    }
 
    private void HandleHeat()
    {
       // float currentXValue = MapValues(currentHeat, 0, maxHeat, minXValue, maxXValue);
        if (currentHeat > maxHeat)
        {
            visualHeat.color = new Color32((byte)MapValues(currentHeat, maxHeat / 2, maxHeat, 255, 0), 255, 0, 255);
            
        }
        else //less than 50%
        {
            visualHeat.color = new Color32(255, (byte)MapValues(currentHeat, 0, maxHeat / 2, 0, 255), 0, 255);
           
        }
    }
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

       // float quarterMaxXValue = maxXValue / 4;
        
    }
}
