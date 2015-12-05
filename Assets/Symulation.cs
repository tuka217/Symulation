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
    private float quarterTmp;
    private static int size = 1000;
    Color[] colors = new Color[size];
    int[] positions = new int[size];

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
        actual = new float[size];
        last = new float[size];
        matrixA = new float[size, size];
        initLast();
        initMatrixA();
        initColors();

        for (int i = 0; i < size; ++i)
        {
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
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            if (i == 999)
            {
                colors[i] = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
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
       // Renderer renderer = gameObject.GetComponent<Renderer>();
       // Debug.Log(renderer.material.name);
        for (int i = 0; i < size; i++)
        {
            if (actual[i] <= quarterTmp)
            {
                upG(actual[i], i);
            }
            else if (actual[i] > quarterTmp && actual[i] <= quarterTmp * 2)
            {
                downB(actual[i], i);
            }
            else if (actual[i] > quarterTmp * 2 && actual[i] <= quarterTmp * 3)
            {
                upR(actual[i], i);
            }
            else if (actual[i] > quarterTmp * 3 && actual[i] <= Tb)
            {
                downG(actual[i], i);
            }

            //Debug.Log(actual[i]);
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
        }
        //Debug.Log(colors[998]);
    }

    private void upG(float currentHeat, int index)
    {
        float step = (currentHeat * 0.01f) / (this.quarterTmp / 100);
        step = Mathf.Round(step * 100f) / 100f;
        float G = step;
        G = Mathf.Round(G * 100f) / 100f;
        float B = colors[index].b;
        float R = colors[index].r;
        colors[index] = new Color(R, G, B, 1.0f);
        //Debug.Log(colors[index]);
    }

    private void downB(float currentHeat, int index)
    {
        float step = ((currentHeat * 0.01f) / (this.quarterTmp / 100)) - 1;
        step = Mathf.Round(step * 100f) / 100f;
        float B = 1 - step;
        B = Mathf.Round(B * 100f) / 100f;
        float G = colors[index].g;
        float R = colors[index].r;
        colors[index] = new Color(R, G, B, 1.0f);
    }

    private void upR(float currentHeat, int index)
    {
        float step = (currentHeat * 0.01f) / (this.quarterTmp / 100);
        step = Mathf.Round(step * 100f) / 100f;
        float R = step - 2;
        R = Mathf.Round(R * 100f) / 100f;
        float G = colors[index].g;
        float B = colors[index].b;
        colors[index] = new Color(R, G, B, 1.0f);
    }

    private void downG(float currentHeat, int index)
    {
        float step = ((currentHeat * 0.01f) / (this.quarterTmp / 100));
        step = Mathf.Round(step * 100f) / 100f;
        float G = 1 - (step - 3);
        G = Mathf.Round(G * 100f) / 100f;
        float B = colors[index].b;
        float R = colors[index].r;
        colors[index] = new Color(R, G, B, 1.0f);
    }
}
