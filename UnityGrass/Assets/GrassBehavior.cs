using UnityEngine;
using System.Collections;

public class GrassBehavior : MonoBehaviour
{

    Transform[] joints = new Transform[10];
    float previousAngle = 0f;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            joints[i] = transform.Find("Joint_" + i.ToString());
            previousAngle = 0f;
        }
    }

    void Update()
    {
    }

    public void Bend(float angle)
    {
        float deltaAngle = angle - previousAngle;
        for (int i = 9; i >= 0; i--)
        {
            //joints[i].eulerAngles = new Vector3(0f, 0f, angle);
            //angle -= 0.1f * angle;
            joints[i].Rotate(0f, 0f, deltaAngle);
            deltaAngle -= 0.1f * deltaAngle;
        }
        previousAngle = angle;
    }
}
