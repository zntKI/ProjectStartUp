using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attatched to each monster
/// </summary>
public class RespawnController : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeRespawn;

    private Dictionary<GameObject, List<float>> monsters;

    void Start()
    {
        monsters = new Dictionary<GameObject, List<float>>();

        for (int i = 0; i < transform.childCount; i++)
        {
            monsters.Add(transform.GetChild(i).gameObject, new List<float>() { 0f });
        }
    }

    void Update()
    {
        foreach (var monster in monsters.Where(m => !m.Key.activeInHierarchy))
        {
            monster.Value[0] += Time.deltaTime;
            if (monster.Value[0] > timeBeforeRespawn)
            {
                monster.Key.SetActive(true);
                monster.Value[0] = 0f;
            }
        }
    }
}