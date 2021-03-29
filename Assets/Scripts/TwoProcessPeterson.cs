using System.Collections;
using UnityEngine;

public class TwoProcessPeterson : MonoBehaviour
{
    public static int turn;

    public static bool[] flags = new bool[2];
    
    [SerializeField] private Process[] processes;
    [SerializeField] private Transform criticalRegion;
    [SerializeField] private Transform waitRegion;

    void OnDisable()
    {
        ResetAll();
    }

    public void _StartAlgorithm()
    {
        for (int i = 0; i < processes.Length; i++)
        {
            processes[i].TwoProcAlgorithm(i);
        }
    }
/*
    public void PetersonAlgorithm(int pID)
    {
        int other = 1 - pID;

        flags[pID] = true;
        processes[pID].RaiseFlag();
        turn = other;

        while(flags[other] == true && turn == other)
        {
            //Espera
            processes[pID].transform.position = waitRegion.position;
        }

        //Região Crítica
        StartCoroutine(CriticalTask(pID));

    }

    IEnumerator CriticalTask(int pID)
    {
        yield return new WaitForSeconds(processes[pID].executionTime);
        flags[pID] = false;
        turn = 1 - pID;
        processes[pID].LowerFlag();
    }
*/
    void ResetAll()
    {
        for (int i = 0; i < processes.Length; i++)
        {
            processes[i].ResetPosition();
            flags[i] = false;
        }
    }

}
