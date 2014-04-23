using UnityEngine;
using System.Collections;

public class GrassBehavior : MonoBehaviour
{

    Transform[] joints = new Transform[10];

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            joints[i] = transform.Find("Joint_" + i.ToString());
        }
    }

    void Update()
    {
    }

    public void Bend(float angle)
    {
        for (int i = 9; i >= 0; i--)
        {
            joints[i].eulerAngles = new Vector3(0f, 0f, angle);
            angle -= 0.1f * angle;
        }
    }
}
