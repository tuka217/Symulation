using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    static int width = 500;
    Color[] colors = new Color[width];
    List<Color> colorsOfTheRainbow = new List<Color>();
    int[] positions = new int[width];
    System.Random random = new System.Random(); // tylko dla testów
    private float time = 0; // tylko dla testów

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
	void Update ()
	{
	    time += 0.001f;

        for (int i = 0; i < colors.Length; ++i)
        {
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), TemperatureToRgbColor(CalculateTemperature(positions[i], time)));
        }
	}

    private double CalculateTemperature(int x, float time)
    {
        //return Math.Sin((Math.PI*x)/2.0f)*Math.Exp(-(58.0f*Math.Pow(Math.PI, 2)*time)/4.0f);
        return x*time+time*200; // tylko dla testów
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

    public Color TemperatureToRgbColor(double temp)
    {
        int indexOfColor = (int)temp; // tylko dla testów
        indexOfColor = width - indexOfColor - 200; // tylko dla testów
        if (indexOfColor > width-1)
        {
            indexOfColor = width;
        }
        if (indexOfColor < 0)
        {
            indexOfColor = 0;
        }
        return colorsOfTheRainbow[indexOfColor];
    }
}
