using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 2f;
    [SerializeField] Vector3 startPos = new Vector3(0, -1.5f, 14f);

    private Vector2 inputVector = Vector2.zero;
    private Vector3 modifiableVector = Vector3.zero;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ResetInputVector()
    {
        inputVector = Vector3.zero;
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
        
        Vector3 finalVector = new Vector3(inputVector.x, rb.velocity.y, inputVector.y) + modifiableVector;
     
        rb.velocity = finalVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        int respwanLayer = LayerMask.NameToLayer("Respawn");
        if(other.gameObject.layer == respwanLayer)
        {
            gameObject.transform.position = startPos;
        }
    }
}
