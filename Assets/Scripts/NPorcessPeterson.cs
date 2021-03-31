using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPorcessPeterson : MonoBehaviour
{
    [SerializeField] private Process[] processes;
    [SerializeField] private Level[] levels;
    [SerializeField] private GameObject reenterButton;

    private Queue<int> finishedProcesses;
    private List<List<Transform>> takenPositions;

    private int[] levelOfPID;
    private int[] lastToEnter;
    private int numberOfProcesses;

    void Start()
    {
        finishedProcesses = new Queue<int>();
        takenPositions = new List<List<Transform>>();

        numberOfProcesses = processes.Length;
        levelOfPID = new int[numberOfProcesses];
        lastToEnter = new int[numberOfProcesses];

        for (int i = 0; i < numberOfProcesses; i++)
        {
            levelOfPID[i] = -1;
            takenPositions.Add(new List<Transform>());
        }
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
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        for (int level = 0; level < numberOfProcesses; level++)
        {
            #region Unity Stuff

            if (level != 0) yield return new WaitForSeconds(2f);

            Transform referencePoint = levels[level].availablePositions.Dequeue();           
            processes[pID].transform.position = referencePoint.position;

            takenPositions[level].Add(referencePoint);

            #endregion

            levelOfPID[pID] = level;
            lastToEnter[level] = pID;

            while (lastToEnter[level] == pID && level != numberOfProcesses-1)
            { 
                yield return null;
            }

            #region Unity Stuff
            levels[level].availablePositions.Enqueue(referencePoint);
            takenPositions[level].Remove(referencePoint);
            #endregion
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
        levelOfPID[pID] = -1;
    }

    public void _ReenterProcess()
    {
        int pID = finishedProcesses.Dequeue();
        StartCoroutine(NProcAlgorithm(pID));
    }

    public void ResetAll()
    {
        ReturnTakenPositions();
        StopAllCoroutines();

        finishedProcesses.Clear();
        for (int i = 0; i < processes.Length; i++)
        {
            processes[i].ResetPosition();
            processes[i].DisableSlider();
        }

        reenterButton.SetActive(false);
    }

    void ReturnTakenPositions()
    {
        
        for (int index = 0; index < numberOfProcesses; index++)
        {
            foreach(Transform pos in takenPositions[index])
            {
                levels[index].availablePositions.Enqueue(pos);
            }       
        }
        
        
    }

}
