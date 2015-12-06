using UnityEngine;

public class Symulation1 : MonoBehaviour
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
    private float quarterTmp;
    private static int size = 1000;
    Color[] colors = new Color[size];
    float[] positions = new float[size];

    private float boundaryRed;
    private float boundaryBlue;
    private float boundaryGreen;
    private float maxTmp;
    private float minTmp;

    // Use this for initialization
    void Start()
    {
        Ta = -400;
        Tb = 2000;
        maxTmp = Ta;
        minTmp = Tb;
        if (Tb > Ta)
        {
            maxTmp = Tb;
            minTmp = Ta;
        }
        
        float deltaT = 0.2f;
        float deltaX = 1f;
        float alfa =2f;
        quarterTmp = Tb / 4;
        r = (alfa * deltaT) / deltaX * deltaX;
        actual = new float[size];
        last = new float[size];
        matrixA = new float[size, size];
        initLast();
        initMatrixA();
        calculateBoundaries();
        changeInitialHeatToColor(Ta, 0);
        changeInitialHeatToColor(Tb, 999);
        changeInitialHeatToColor(0, 1);
        initColors();

        for (int i = 0; i < size; ++i)
        {
            positions[i] = i;
            GetComponent<Renderer>().material.SetFloat("_PositionsX" + i.ToString(), positions[i]);
        }
    }

    private void calculateBoundaries()
    {
        float scale = 0;
        scale = Tb;
        if (Ta < 0 || Tb < 0)
        {
            scale = Mathf.Abs(Tb) + Mathf.Abs(Ta);
            quarterTmp = scale / 4;
            boundaryGreen = (Ta < Tb)? Ta + quarterTmp: Tb + quarterTmp;
            boundaryBlue = boundaryGreen + quarterTmp;
            boundaryRed = boundaryBlue + quarterTmp;
        }
        if (Ta > Tb)
        {
            scale = Ta;

        }
        quarterTmp = scale / 4;
        boundaryGreen = quarterTmp;
        boundaryBlue = boundaryGreen + quarterTmp;
        boundaryRed = boundaryBlue + quarterTmp;
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
            last[i] = 0;
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

    private void initColors()
    {
        for (int i = 2; i < colors.Length -1; ++i)
        {
            colors[i] = colors[1];
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
        }
    }
    private void changeInitialHeatToColor( float initialHeat, int index)
    {
        if (initialHeat <= boundaryGreen)
        {
            colors[index] = new Color(0, 0, 1);
            upG(initialHeat, index);
        }
        else if (initialHeat > boundaryGreen && initialHeat <= boundaryBlue)
        {
            colors[index] = new Color(0, 1, 1);
            downB(initialHeat, index);
        }
        else if (initialHeat > boundaryBlue && initialHeat <= boundaryRed)
        {
            colors[index] = new Color(0, 1, 0);
            upR(initialHeat, index);
        }
        else if (initialHeat > boundaryRed && initialHeat <= maxTmp)
        {
            colors[index] = new Color(1, 1, 0);
            downG(initialHeat, index);
        }

        GetComponent<Renderer>().material.SetColor("_Colors" + index.ToString(), colors[index]);
    }

    // Update is called once per frame
    void Update()
    {
        calculateActual();
        changeHeatToRGBColor();
        passColorToSahder();
    }

    private void changeHeatToRGBColor()
    {
        for (int i = 0; i < size; i++)
        {
            if (actual[i] <= boundaryGreen)
            {
                upG(actual[i], i);
            }
            else if (actual[i] > boundaryGreen && actual[i] <= boundaryBlue)
            {
                downB(actual[i], i);
            }
            else if (actual[i] > boundaryBlue && actual[i] <= boundaryRed)
            {
                upR(actual[i], i);
            }
            else if (actual[i] > boundaryRed && actual[i] <= maxTmp)
            {
                downG(actual[i], i);
            }
        }
    }
    private void passColorToSahder()
    {
        for( int i = 0; i < size; i ++)
        {
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
        }
    }

    private void upG(float currentHeat, int index)
    {
        float mapedHeat = Mathf.Abs(minTmp - currentHeat);
        float G = mapedHeat / quarterTmp;
        float B = colors[index].b;
        float R = colors[index].r;
        colors[index] = new Color(R, G, B, 1.0f);

    }

    private void downB(float currentHeat, int index)
    {
        float mapedHeat = Mathf.Abs(boundaryGreen - currentHeat);
        float B = mapedHeat / quarterTmp;
        float G = colors[index].g;
        float R = colors[index].r;
        colors[index] = new Color(R, G, B, 1.0f);
    }

    private void upR(float currentHeat, int index)
    {
        float mapedHeat = Mathf.Abs(boundaryBlue - currentHeat);
        float R = mapedHeat / quarterTmp;
        float G = colors[index].g;
        float B = colors[index].b;
        colors[index] = new Color(R, G, B, 1.0f);
    }

    private void downG(float currentHeat, int index)
    {
        float mapedHeat = Mathf.Abs(boundaryRed - currentHeat);
        float G = mapedHeat / quarterTmp;
        float B = colors[index].b;
        float R = colors[index].r;
        colors[index] = new Color(R, G, B, 1.0f);
    }
}
