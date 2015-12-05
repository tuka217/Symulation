using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    private float quarterTmp;
    private Color[] colors;
    private int[] positions;

    // Use this for initialization
    void Start()
    {
        Ta = 40;
        Tb = 200;
        float deltaT = 0.2f;
        float deltaX = 1f;
        float alfa = 0.8f;
        quarterTmp = Tb / 4;
        r = (alfa * deltaT) / deltaX;
        actual = new float[1000];
        last = new float[1000];
        matrixA = new float[1000, 1000];
        initLast(ref last, 1000);
        initMatrixA(ref matrixA, 1000,r);
        colors = new Color[1000];
        initColors(ref colors);
        positions = new int[1000];

    }

    private void calculateActual(ref float[] actual, ref float[] last, ref float[,] matrixA)
    {
        for (int i = 0; i < 1000; i++)
        {
            actual[i] = 0;
        }
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                actual[i] += matrixA[i, j] * last[j];
            }
        }
        for (int i = 0; i < 1000; i++)
        {
            last[i] = actual[i];
        }

    }

    private void initLast(ref float[] last, int count)
    {
        last[0] = Ta;
        last[count -1] = Tb;
        for (int i = 1; i < count - 1; i++)
        {
            last[i] = 0;
        }
    }

    private void initMatrixA(ref float[,] matrixA, int count, float r)
    {
        matrixA[0, 0] = 1;
        matrixA[count - 1, count - 1] = 1;
        for (int i = 1; i < count; i++)
        {
            matrixA[0, i] = 0;
        }
        for (int i = count - 2; i <= 0; i--)
        {
            matrixA[count - 1, i] = 0;
        }
        for (int i = 1; i < count - 1; i++)
        {
            for (int j = 0; j < count; j++)
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

    private void initColors(ref Color[] colors)
    {
        for (int i = 0; i < 1000; i++)
        {
            colors[i] = new Color(0, 0, 1);
            if (i == 999)
            {
                colors[i] = new Color(1, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        calculateActual( ref actual, ref last, ref matrixA);
        changeHeatToRGBColor(ref actual, ref colors, 1000);
    }

    private void changeHeatToRGBColor(ref float[] heats, ref Color[] colors, int count)
    {
       // Renderer renderer = gameObject.GetComponent<Renderer>();
       // Debug.Log(renderer.material.name);
        for (int i = 0; i < count; i++)
        {
            positions[i] = i;
            GetComponent<Renderer>().material.SetFloat("_PositionsX" + i.ToString(), positions[i]);
            if (heats[i] <= quarterTmp)
            {
                upG(heats[i],ref colors[i]);
                GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
            }
            else if (heats[i] > quarterTmp && heats[i] <= quarterTmp * 2)
            {
                downB(heats[i], ref colors[i]);
                GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
            }
            else if (heats[i] > quarterTmp * 2 && heats[i] <= quarterTmp * 3)
            {
                upR(heats[i], ref colors[i]);
                GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
            }
            else if (heats[i] > quarterTmp * 3 && heats[i] <= Tb)
            {
                downG(heats[i], ref colors[i]);
                GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
            }
        }
        Debug.Log(colors[998]);
    }

    private void upG(float currentHeat, ref Color color)
    {
        float step = (currentHeat * 0.01f) / (this.quarterTmp / 100);
        step = Mathf.Round(step * 100f) / 100f;
        float G = step;
        G = Mathf.Round(G * 100f) / 100f;
        float B = color.b;
        float R = color.r;
        color =  new Color(R, G, B);
    }

    private void downB(float currentHeat, ref Color color)
    {
        float step = ((currentHeat * 0.01f) / (this.quarterTmp / 100)) - 1;
        step = Mathf.Round(step * 100f) / 100f;
        float B = 1 - step;
        B = Mathf.Round(B * 100f) / 100f;
        float G = color.g;
        float R = color.r;
        color = new Color(R,G, B);
    }

    private void upR(float currentHeat, ref Color color)
    {
        float step = (currentHeat * 0.01f) / (this.quarterTmp / 100);
        step = Mathf.Round(step * 100f) / 100f;
        float R = step - 2;
        R = Mathf.Round(R * 100f) / 100f;
        float G = color.g;
        float B = color.b;
        color = new Color(R, G, B);
    }

    private void downG(float currentHeat, ref Color color)
    {
        float step = ((currentHeat * 0.01f) / (this.quarterTmp / 100));
        step = Mathf.Round(step * 100f) / 100f;
        float G = 1 - (step - 3);
        G = Mathf.Round(G * 100f) / 100f;
        float B = color.b;
        float R = color.r;
        color = new Color(R, G, B);
    }
}
