﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Symulation : MonoBehaviour
{
    private float deltaT;
    private float deltaX;
    private float alfa;
    private float r;
    private float[] actual;
    private float[] last;
    private float[,] matrixA;
    private float Ta;
    private float Tb;
    private float mainTemp;
    private static int size = 100;
    float[] positions = new float[size];
    List<Color> colorsOfTheRainbow = new List<Color>();
    float mintemp = 0;

    // Use this for initialization
    void Start()
    {
        Ta = -30;
        Tb = 1250;
        float deltaT = 0.2f;
        float deltaX = 1f;
        float alfa = 0.8f;
        mainTemp = 20;
        r = (alfa * deltaT) / deltaX;
        actual = new float[size];
        last = new float[size];
        matrixA = new float[size, size];
        initLast();
        initMatrixA();
        colorsOfTheRainbow = GetRainbowColors(1300);
        if (Ta < Tb)
        {
            mintemp = Ta;
        }
        else
        {
            mintemp = Tb;
        }

        for (int i = 0; i < size; ++i)
        {
            actual[i] = 0;
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colorsOfTheRainbow[(int)(actual[i] + Math.Abs(mintemp))]);

            positions[i] = i;
            GetComponent<Renderer>().material.SetFloat("_PositionsX" + i.ToString(), positions[i]);
        }
    }

    private void calculateActual()
    {
        for (int i = 0; i < size; i++)
        {
            actual[i] = 0;
        }
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                actual[i] += matrixA[i, j] * last[j];
            }
        }
        for (int i = 0; i < size; i++)
        {
            last[i] = actual[i];
        }

    }

    private void initLast()
    {
        last[0] = Ta;
        last[size - 1] = Tb;
        for (int i = 1; i < size - 1; i++)
        {
            last[i] = mainTemp;
        }
    }

    private void initMatrixA()
    {
        matrixA[0, 0] = 1;
        matrixA[size - 1, size - 1] = 1;
        for (int i = 1; i < size; i++)
        {
            matrixA[0, i] = 0;
        }
        for (int i = size - 2; i <= 0; i--)
        {
            matrixA[size - 1, i] = 0;
        }
        for (int i = 1; i < size - 1; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (j == i)
                {
                    matrixA[i, j] = (1 - 2 * this.r);
                }
                else if (j == (i + 1) || j == (i - 1))
                {
                    matrixA[i, j] = this.r;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateActual();
        changeHeatToRGBColor();
    }

    private void changeHeatToRGBColor()
    {
        for (int i = 0; i < size; i++)
        {
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colorsOfTheRainbow[(int)(actual[i]+Math.Abs(mintemp))]);
        }
    }

    public static List<Color> GetRainbowColors(int colorCount)
    {
        List<Color> ret = new List<Color>(colorCount);

        float p = 270.0f / (float)colorCount;

        for (int n = 0; n < colorCount; n++)
        {
            ret.Add(HsvToRgb(n * p, 1.0f, 1.0f));
        }

        ret.Reverse();
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
        return new Color(r, g, b, 1.0f);
    }
}
