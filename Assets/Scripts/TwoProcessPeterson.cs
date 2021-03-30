using System.Collections;
using UnityEngine;

public class TwoProcessPeterson : MonoBehaviour
{
    public static int turn;

    public static bool[] flags = new bool[2];
    
    [SerializeField] private Process[] processes;
    [SerializeField] private Transform criticalRegion;
    [SerializeField] private Transform waitRegion;
    [SerializeField] public static bool isCriticalZoneOccupied;

    void OnDisable()
    {
        ResetAll();
    }

    public void _QueueProcesses()
    {
        for (int i = 0; i < processes.Length; i++)
        {
            StartCoroutine(TwoProcAlgorithm(i));
        }
    }

    public IEnumerator TwoProcAlgorithm(int pID)
    {
        print("p" + pID + " chegou");
        int other = 1 - pID;

        flags[pID] = true;
        processes[pID].RaiseFlag();
        turn = other;

        while (flags[other] == true && turn == other)
        {
            //Espera
            processes[pID].transform.position = waitRegion.position;
            yield return null;
        }

        //Seção Crítica
        StartCoroutine(CriticalSection(pID));

    }

    IEnumerator CriticalSection(int pID)
    {
        processes[pID].transform.position = criticalRegion.position;

        yield return new WaitForSeconds(2f);

        flags[pID] = false;
        processes[pID].LowerFlag();
        processes[pID].ResetPosition();
    }


    void ResetAll()
    {
        for (int i = 0; i < processes.Length; i++)
        {
            processes[i].ResetPosition();
            flags[i] = false;
        }
    }

}
