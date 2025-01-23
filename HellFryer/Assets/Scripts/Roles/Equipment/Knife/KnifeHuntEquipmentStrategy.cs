using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KnifeThrowState
{
    None,
    Outgoing,
    Incoming,
}

public class KnifeHuntEquipmentStrategy : EquipmentStrategy
{
    [SerializeField]
    private float initialThrowForce = 2f;
    [SerializeField, Tooltip("How much throw force should be modified after each time throw force is applied")]
    private float throwForceModifier = 20;

    private Vector3 localPosOffsetToParent;

    private Transform playerTransform;

    private KnifeThrowState throwState;

    private Vector3 moveDir; // unit vector
    private float currentMoveForce;


    void Start()
    {
    }

    public override void StartUp()
    {
        SwitchThrowState(KnifeThrowState.Outgoing);
    }

    private void SwitchThrowState(KnifeThrowState newState)
    {
        switch (newState)
        {
            case KnifeThrowState.None:

                if (throwState == KnifeThrowState.Incoming)
                {
                    transform.parent = playerTransform; // Set it back as a child of parent
                    transform.rotation = playerTransform.rotation;
                    transform.localPosition = localPosOffsetToParent;

                    gameObject.GetComponent<BoxCollider>().isTrigger = false; // Set back to normal
                }

                break;
            case KnifeThrowState.Outgoing:

                if (throwState == KnifeThrowState.None)
                {
                    gameObject.GetComponent<BoxCollider>().isTrigger = true; // Set to trigger for enemy and player collision detection

                    localPosOffsetToParent = transform.localPosition;
                    playerTransform = transform.parent;

                    transform.parent = null; // In order to fix rotating around the player when player rotating
                }

                moveDir = playerTransform.forward;
                currentMoveForce = initialThrowForce;

                break;
            case KnifeThrowState.Incoming:

                moveDir = (playerTransform.position - transform.position).normalized;
                currentMoveForce = 0f;

                break;
            default:
                break;
        }
        throwState = newState;
    }

    protected override void Update()
    {
        Perform();
    }

    public override void Perform()
    {
        switch (throwState)
        {
            case KnifeThrowState.None:
                break;
            case KnifeThrowState.Outgoing:

                ApplyOutgoingForce();

                break;
            case KnifeThrowState.Incoming:

                ApplyIncomingForce();

                break;
            default:
                break;
        }
    }

    private void ApplyOutgoingForce()
    {
        transform.position += moveDir * currentMoveForce * Time.deltaTime;

        Vector3 vectorBetweenPlayerAndKnife = transform.position - playerTransform.position;
        transform.LookAt(transform.position + vectorBetweenPlayerAndKnife); // Look at the opposite direction to the player

        currentMoveForce -= throwForceModifier * Time.deltaTime;
        if (currentMoveForce <= 0f)
        {
            SwitchThrowState(KnifeThrowState.Incoming);
        }
    }

    private void ApplyIncomingForce()
    {
        Vector3 vectorBetweenKnifeAndPlayer = playerTransform.position - transform.position;
        moveDir = vectorBetweenKnifeAndPlayer.normalized;
        transform.LookAt(transform.position + (-vectorBetweenKnifeAndPlayer)); // Look at the opposite direction to the player

        transform.position += moveDir * currentMoveForce * Time.deltaTime;

        currentMoveForce += throwForceModifier * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == playerTransform
            && throwState == KnifeThrowState.Incoming)
        {
            SwitchThrowState(KnifeThrowState.None);
        }
        else if (other.transform.TryGetComponent<RiverMonsterController>(out RiverMonsterController riverMonster)
            && !other.isTrigger && throwState != KnifeThrowState.None)
        {
            Vector3 dirV = (playerTransform.position - riverMonster.transform.position).normalized;
            riverMonster.Die(dirV);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (throwState == KnifeThrowState.Outgoing
            && other.transform.TryGetComponent<RiverMonsterController>(out RiverMonsterController riverMonster)
            && !other.isTrigger)
        {
            Vector3 dirV = (playerTransform.position - riverMonster.transform.position).normalized;
            riverMonster.Die(dirV);
        }
    }
}