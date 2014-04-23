using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrassGroupBehavior : MonoBehaviour
{
    List<GrassBehavior> grassBehaviors = new List<GrassBehavior>();
    GameObject[] go_array;
    int arrayLength;
    ParticleSystem particle;

    static float ANGLE_MIN_MIN = -30f;
    static float ANGLE_MAX_MAX = 30f;
    static float SPEED_MIN = 0.1f;
    static float SPEED_MAX = 0.5f;

    float[] angle_array;
    float[] angle_min_array;
    float[] angle_max_array;
    float[] speed_array;
    int[] backForthCount_array;
    bool[] increasing_tags;

    static float DISTURB_DURATION = 2f;
    bool disturbed = false;
    float disturbedTime;

    void Start()
    {
        InitGrass();
        InitParticle();
    }

    void Update()
    {
        UpdateDisturb();
        UpdateInput();
        UpdateGrassGroup();
    }

    void InitParticle()
    {
        particle = GetComponentInChildren<ParticleSystem>(); //GetComponentInChild<ParticleSystem>();
    }

    void InitGrass()
    {
        go_array = GameObject.FindGameObjectsWithTag("grass");
        arrayLength = go_array.Length;

        angle_array = new float[arrayLength];
        angle_min_array = new float[arrayLength];
        angle_max_array = new float[arrayLength];
        speed_array = new float[arrayLength];
        backForthCount_array = new int[arrayLength];
        increasing_tags = new bool[arrayLength];

        //var grassList = gameObject.GetComponentsInChildren<GrassBehavior>();
        for (int i = 0; i < arrayLength; i++)
        {
            go_array[i].AddComponent<GrassBehavior>();
            //grassBehaviors.Add(grassList[i]);
            grassBehaviors.Add(go_array[i].GetComponent<GrassBehavior>());
            Randomize(i);
        }
    }

    void UpdateGrassGroup()
    {
        for (int i = 0; i < arrayLength; i++)
        {
            UpdateGrass(i);
        }
    }

    void UpdateGrass(int i)
    {
        grassBehaviors[i].Bend(angle_array[i]);

        if (angle_array[i] > angle_max_array[i])
        {
            increasing_tags[i] = false;
            backForthCount_array[i]++;
        }
        else if (angle_array[i] < angle_min_array[i])
        {
            increasing_tags[i] = true;
            backForthCount_array[i]++;
        }

        angle_array[i] += increasing_tags[i] ? speed_array[i] : (0 - speed_array[i]);

        if (backForthCount_array[i] == 2)
        {
            backForthCount_array[i] = 0;
            Randomize(i);
        }
    }

    void Randomize(int i)
    {
        angle_min_array[i] = Random.Range(ANGLE_MIN_MIN, -9f);
        angle_max_array[i] = Random.Range(9f, ANGLE_MAX_MAX);
        speed_array[i] = Random.Range(SPEED_MIN, SPEED_MAX);
    }

    void ChangeSpeed(bool increase)
    {
        float increment = increase ? 1f : -1f;
        SPEED_MIN += increment;
        SPEED_MAX += increment;
        for (int i = 0; i < arrayLength; i++)
        {
            speed_array[i] += increment;
        }
    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Disturb();
        }
    }

    public void Disturb()
    {
        if (disturbed)
        {
            return;
        }
        ChangeSpeed(true);
        disturbedTime = 0f;
        disturbed = true;

        particle.Play();
        Debug.Log("disturbed");
    }

    void UpdateDisturb()
    {
        if (disturbed)
        {
            if (disturbedTime > DISTURB_DURATION)
            {
                ChangeSpeed(false);
                disturbed = false;
            }
            else
            {
                disturbedTime += Time.deltaTime;
            }
        }
    }
}
