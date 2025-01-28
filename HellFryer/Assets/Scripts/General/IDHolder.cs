using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDHolder : MonoBehaviour
{
    public int ID => id;

    [SerializeField]
    private int id;
}
