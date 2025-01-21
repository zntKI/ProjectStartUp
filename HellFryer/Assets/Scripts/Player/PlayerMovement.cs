using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 2f;

    private Vector2 inputVector = Vector2.zero;
    private Vector3 modifiableVector = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
        inputVector *= speed;
    }

    public void SetModifiableVector(Vector3 modifiableVector)
    {
        this.modifiableVector = modifiableVector;
    }

    void FixedUpdate()
    {
        Vector3 finalVector = new Vector3(inputVector.x, 0f, inputVector.y) + modifiableVector;
        rb.velocity = finalVector;
    }
}
