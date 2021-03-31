using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPorcessPeterson : MonoBehaviour
{
    [SerializeField] private Process[] processes;
    [SerializeField] private Level[] levels;
    [SerializeField] private GameObject reenterButton;

    private Queue<int> finishedProcesses;
    private int[] levelOfPID;
    private int[] lastToEnter;
    private int numberOfProcesses;

    void Start()
    {
        numberOfProcesses = processes.Length;
        finishedProcesses = new Queue<int>();
        levelOfPID = new int[numberOfProcesses];
        lastToEnter = new int[numberOfProcesses];
    }

    void OnDisable()
    {
        ResetAll();
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
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        for (int index = 0; index < numberOfProcesses; index++)
        {
            levelOfPID[pID] = index;
            lastToEnter[index] = pID;

            yield return new WaitForSeconds(3f);

            Transform referencePoint = levels[index].availablePositions.Dequeue();
            processes[pID].transform.position = referencePoint.position;

            while (lastToEnter[index] == pID && index != numberOfProcesses-1)
            { 
                yield return null;
            }

            levels[index].availablePositions.Enqueue(referencePoint);

        }

        StartCoroutine(CriticalSection(pID));
    }

    IEnumerator CriticalSection(int pID)
    {
        processes[pID].EnableTimer();

        yield return new WaitForSeconds(5f);

        processes[pID].ResetPosition();

        finishedProcesses.Enqueue(pID);
        reenterButton.SetActive(true);
    }

    public void _ReenterProcess()
    {
        int pID = finishedProcesses.Dequeue();
        StartCoroutine(NProcAlgorithm(pID));
    }

    void ResetAll()
    {
        for (int i = 0; i < processes.Length; i++)
        {
            processes[i].ResetPosition();
        }
        finishedProcesses.Clear();
        StopAllCoroutines();
    }


}
