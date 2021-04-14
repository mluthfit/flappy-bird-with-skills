using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustScrolling : MonoBehaviour
{
    [SerializeField] GameObject[] background;
    private GameObject temp;
    private Vector2 setLastBackground;
    [SerializeField] float speed = 1f;

    void Start()
    {
        setLastBackground = background[1].transform.position;
    }

    void Update()
    {
        background[0].transform.position = Vector2.MoveTowards(background[0].transform.position, new Vector2(-100, 0), speed * Time.deltaTime);
        background[1].transform.position = Vector2.MoveTowards(background[1].transform.position, new Vector2(-100, 0), speed * Time.deltaTime);
        if (background[1].transform.position.x <= 0)
        {
            background[0].transform.position = setLastBackground;
            temp = background[0];
            background[0] = background[1];
            background[1] = temp;
        }
    }
}
