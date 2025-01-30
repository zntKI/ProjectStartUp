using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigAreaController : MonoBehaviour
{
    [SerializeField]
    private GameObject limbsPrefab;
    [SerializeField]
    private GameObject heartPrefab;
    [SerializeField]
    private GameObject zomnbiePrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            SoundManager.instance.PlayerDiggingGrave();

            // Spawn item
            DigOutItem gameObjectSpawned = (DigOutItem)Random.Range((int)DigOutItem.Limbs, (int)DigOutItem.Zombie + 1);
            if (gameObjectSpawned == DigOutItem.Zombie)
            {
                // Spawn zombie
                GameObject spawnedZombie = Instantiate(zomnbiePrefab, player.transform.position, Quaternion.identity);
                Vector3 diffVec = (player.transform.position - transform.position).normalized;
                Vector3 lookVec = new Vector3(diffVec.x, 0, diffVec.z);
                spawnedZombie.transform.LookAt(player.transform.position + lookVec);

                player.DisableMovement();

                SoundManager.instance.ZombieWakingUp();
            }
            else
            {
                SpawnItem(gameObjectSpawned);
            }

            // Disable detection functionality
            //GetComponent<BoxCollider>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void SpawnItem(DigOutItem prefabId)
    {
        Vector3 spawnPos = transform.position;
        GameObject spawnedItem = null;

        switch (prefabId)
        {
            case DigOutItem.Limbs:
                spawnedItem = Instantiate(limbsPrefab, spawnPos, Quaternion.identity);
                break;
            case DigOutItem.Heart:
                spawnedItem = Instantiate(heartPrefab, spawnPos, Quaternion.identity);
                break;
            default:
                Debug.LogWarning("Incorrect spawn item Id in graveyard dig out");
                return;
        }
    }

}

public enum DigOutItem
{
    Limbs = 0,
    Heart,
    Zombie,
}