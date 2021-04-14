using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPipe : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private Bird bird;
    private float random;

    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<Bird>();
        random = Random.Range(-1.45f, 3.9f);
        transform.position = new Vector2(transform.position.x, random);
    }

    void Update()
    {
        if (!bird.getDead())
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-100, 0), speed * Time.deltaTime);

        if (transform.position.x <= -10) Destroy(gameObject);
    }
}
