using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 100;

    private Vector2 inputVector = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void Update()
    {
        Vector3 velocity = Vector3.Normalize(new Vector3(inputVector.x, 0, inputVector.y));
        velocity *= Time.deltaTime * speed;

        rb.velocity = velocity;
    }
}
