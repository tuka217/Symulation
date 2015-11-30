using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeColors : MonoBehaviour {

    private float cachedY;
    private float minXValue;
    private float maxXValue;
    private int currentHeat;
    public int maxHeat;
    public Image visualHeat;
	// Use this for initialization
	void Start () {
        maxHeat = 840;
        cachedY = 0;
        maxXValue = 840;
        minXValue = 0;
        currentHeat = maxHeat;
	}
	
    private int CurrentHeat
    {
        get { return currentHeat; }
        set { 
                currentHeat = value;
                HandleHeat();
            }
    }
	// Update is called once per frame
	void Update () {
        currentHeat -= 1;
        HandleHeat();
        if (currentHeat <= 0)
        {
            currentHeat = 0;
        }
	}

 
    private void HandleHeat()
    {
        float currentXValue = MapValues(currentHeat, 0, maxHeat, minXValue, maxXValue);
        if (currentHeat > maxHeat / 2)
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
    }
}
