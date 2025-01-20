using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 2f;

    private Vector2 inputVector = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
        inputVector *= speed;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(inputVector.x, rb.velocity.y, inputVector.y);
    }
}
