using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPipe : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private Bird bird;
    private float random;
    private GameObject upPipe;
    private GameObject downPipe;

    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<Bird>();
        if (!bird.isEzWay)
        {
            random = Random.Range(-1.45f, 3.9f);
            transform.position = new Vector2(transform.position.x, random);
        } 
        else
        {
            upPipe = transform.GetChild(0).gameObject;
            upPipe.transform.position = new Vector2(transform.position.x, 8.32f);
            downPipe = transform.GetChild(1).gameObject;
            downPipe.transform.position = new Vector2(transform.position.x, -6.96f);
        } 
    }

    void Update()
    {
        if (!bird.getDead())
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-100, 0), speed * Time.deltaTime);

        if (transform.position.x <= -10) Destroy(gameObject);
    }
}