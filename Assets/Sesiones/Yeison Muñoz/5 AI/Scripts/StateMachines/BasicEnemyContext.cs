using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class BasicEnemyContext
{
    public GameObject agent;
    public GameObject player;
    public Transform target;
    public float targetDistance;
}
