using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPorcessPeterson : MonoBehaviour
{
    [SerializeField] private Process[] processes;
    [SerializeField] private Transform[] levels;
    [SerializeField] private Transform[] positions;

    private int[] level;
    private int[] lastToEnter;
    private int numberOfProcesses;


    void Start()
    {
        numberOfProcesses = processes.Length;
        level = new int[numberOfProcesses];
        lastToEnter = new int[numberOfProcesses];

    }

    public void _QueueProcesses()
    {
        for (int i = 0; i < numberOfProcesses; i++)
        {
            StartCoroutine(NProcAlgorithm(i));
        }
    }

    private IEnumerator NProcAlgorithm(int pID)
    {
        yield return new WaitForSeconds(Random.Range(0, .5f));
        for (int index = 0; index < numberOfProcesses; index++)
        {
            level[pID] = index;
            lastToEnter[index] = pID;

            yield return new WaitForSeconds(2f);
            processes[pID].transform.position = levels[index].position;

            while (lastToEnter[index] == pID) 
            {
                print("p" + pID + " ta esperando");

                yield return null;
            }

        }

        print("p" + pID + " chegou na critica");
        processes[pID].ResetPosition();
        level[pID] = -1;
    }
}
