using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 aim;
    public float moveSpeed;

    private void Awake()
    {
        aim = Vector2.zero;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, aim, moveSpeed*Time.deltaTime);
    }
}
