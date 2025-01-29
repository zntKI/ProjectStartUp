using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: make it work also for two enemies

public class RiverMonsterController : MonoBehaviour
{
    [SerializeField]
    GameObject bloodPrefab;
    [SerializeField]
    GameObject depressedSoulsPrefab;

    void Die(Vector3 spawnItemDir)
    {
        PullRangeController pullRangeController = GetComponentInChildren<PullRangeController>();
        
        pullRangeController.StopPulling(); // Stop pulling the player
        
        Destroy(gameObject);

        float distance = pullRangeController.transform.GetComponent<SphereCollider>().radius;
        Vector3 spawnPos = transform.position + spawnItemDir * distance;

        Instantiate(bloodPrefab, spawnPos, Quaternion.identity);
        Instantiate(depressedSoulsPrefab, spawnPos, Quaternion.identity);

        SoundManager.instance.CharonRiverMonsterDeath();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<KnifeHuntEquipmentStrategy>(out KnifeHuntEquipmentStrategy knife))
        {
            Die(-other.transform.forward);

            SoundManager.instance.KnifeHit();
        }
    }
}
