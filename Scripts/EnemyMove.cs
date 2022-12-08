using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform p1, p2;
    public float speed = 1f;
    public Transform startPos;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        if (transform.position == p1.position)
        {
            nextPos = p2.position;
            transform.localScale = new Vector2(-1, 1);
        }

        if (transform.position == p2.position)
        {
            nextPos = p1.position;
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(p1.position, p2.position);
    }
}
