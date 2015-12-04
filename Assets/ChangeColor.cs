using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

    static int width = 1000;
    Color[] colors = new Color[width];
    float[] positions = new float[width];
    System.Random random = new System.Random();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < width; ++i)
        {
            positions[i] = i;
            GetComponent<Renderer>().material.SetFloat("_PositionsX" + i.ToString(), positions[i]);
        }

	}

	// Update is called once per frame
	void Update () {

        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = new Color(random.Next(0, 1000) / 1000.0f, random.Next(0, 1000) / 1000.0f, random.Next(0, 1000) / 1000.0f, 1.0f);
            GetComponent<Renderer>().material.SetColor("_Colors" + i.ToString(), colors[i]);
        }
	}
}
