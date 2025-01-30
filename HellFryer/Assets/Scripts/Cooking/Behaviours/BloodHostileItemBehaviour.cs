using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodHostileItemBehaviour : HostileItemBehaviour
{
    [SerializeField] GameObject bloodSplatterPrefab;
    [SerializeField] float bloodSpawnHeight = 1.5f;
    [SerializeField] float bloodSpawnRate = 0.5f;
    [SerializeField] float bloodSpawnRange = 1.5f;

    private WaitForSeconds bloodSpawnInterval;

    List<GameObject> bloodSplatters = new List<GameObject>();

    private void Start()
    {
        bloodSpawnInterval = new WaitForSeconds(bloodSpawnRate);
    }

    public override void Activate()
    {
        StartCoroutine(SpawnBloodCoroutine());

        SoundManager.instance.PlayBloodSound();
    }

    public override void Deactivate()
    {
        StopCoroutine(SpawnBloodCoroutine());
        DespawnBloodSplatters();

        SoundManager.instance.StopBloodSound();
    }

    IEnumerator SpawnBloodCoroutine()
    {
        while (true) {
            yield return bloodSpawnInterval;

            GameObject bloodSplatter = Instantiate(bloodSplatterPrefab, GetRandomSpawnPos(), GetRandomSpawnRot());
            bloodSplatters.Add(bloodSplatter);
        }
    }

    void DespawnBloodSplatters()
    {
        while (bloodSplatters.Count > 0)
        {
            DespawnFirstBloodSplatter();
        }
    }

    void DespawnFirstBloodSplatter()
    {
        GameObject curBloodSplatter = bloodSplatters[0];
        bloodSplatters.RemoveAt(0);
        Destroy(curBloodSplatter);
    }

    Vector3 GetRandomSpawnPos()
    {
        Vector3 spawnPos;

        float randX = Random.Range(-bloodSpawnRange, bloodSpawnRange);
        float randZ = Random.Range(-bloodSpawnRange, bloodSpawnRange);
        spawnPos.y = transform.position.y - bloodSpawnHeight;

        spawnPos = new Vector3(transform.position.x + randX, transform.position.y - bloodSpawnHeight, transform.position.z + randZ);

        return spawnPos;
    }

    Quaternion GetRandomSpawnRot() {
        return Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
    }
}
