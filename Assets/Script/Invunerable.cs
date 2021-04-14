using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invunerable : MonoBehaviour
{
    [SerializeField] Bird bird;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pipe")
        {
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
