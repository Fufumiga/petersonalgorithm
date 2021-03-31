using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    public Queue<Transform> availablePositions;

    void Start()
    {
        availablePositions = new Queue<Transform>();

        foreach (Transform pos in positions)
        {
            availablePositions.Enqueue(pos);
        }
    }
}
