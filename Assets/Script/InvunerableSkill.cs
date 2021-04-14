using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvunerableSkill : MonoBehaviour
{
    [SerializeField] Bird bird;
    private GameObject item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item = collision.gameObject;
        if (item.tag == "Pipe") item.GetComponent<Collider2D>().enabled = false;
    }
}
