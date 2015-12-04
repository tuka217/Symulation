using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    static int width = 500;
    Color[] colors = new Color[width];
    List<Color> colorsOfTheRainbow = new List<Color>();
    float[] positions = new float[width];
    System.Random random = new System.Random();

	// Use this for initialization
	void Start ()
	{
	    colorsOfTheRainbow = GetRainbowColors(width);

        for (int i = 0; i < width; ++i)
        {
            positions[i] = i;
            GetComponent<Renderer>().material.SetFloat("_PositionsX" + i.ToString(), positions[i]);
        }

        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = new Color(colorsOfTheRainbow[i].r, colorsOfTheRainbow[i].g, colorsOfTheRainbow[i].b,1.0f);
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
        }

	}

	// Update is called once per frame
	void Update () {

	}

    public static List<Color> GetRainbowColors(int colorCount)
    {
        List<Color> ret = new List<Color>(colorCount);

        float p = 360.0f / (float)colorCount;

        for (int n = 0; n < colorCount; n++)
        {
            ret.Add(HsvToRgb(n * p, 1.0f, 1.0f));
        }

        return ret;
    }
    public static Color HsvToRgb(float h, float s, float v)
    {
        int hi = (int)Math.Floor(h / 60.0f) % 6;
        float f = (h / 60.0f) - (float)Math.Floor(h / 60.0f);

        float p = v * (1.0f - s);
        float q = v * (1.0f - (f * s));
        float t = v * (1.0f - ((1.0f - f) * s));

        Color ret = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        switch (hi)
        {
            case 0:
                ret = GetRgb(v, t, p);
                break;
            case 1:
                ret = GetRgb(q, v, p);
                break;
            case 2:
                ret = GetRgb(p, v, t);
                break;
            case 3:
                ret = GetRgb(p, q, v);
                break;
            case 4:
                ret = GetRgb(t, p, v);
                break;
            case 5:
                ret = GetRgb(v, p, q);
                break;
        }
        return ret;
    }
    public static Color GetRgb(float r, float g, float b)
    {
        return new Color(r,g,b,1.0f);
    }
}
