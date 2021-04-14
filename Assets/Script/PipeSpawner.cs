using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    private float period = 0f;
    public float everySeconds = 2f;
    [SerializeField] private GameObject pipe;
    public Bird bird;

    void Update()
    {
        if (!bird.getDead())
        {
            if (period > everySeconds)
            {
                if (pipe != null) Instantiate(pipe, transform.position, Quaternion.identity);
                period = 0f;
            }
            period += Time.deltaTime;
        }
    }
}
