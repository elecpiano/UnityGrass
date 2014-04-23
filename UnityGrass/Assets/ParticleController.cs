using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    ParticleSystem ps;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ps.Play();
        }
    }
}
