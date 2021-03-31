using System.Collections;
using UnityEngine;

public class TwoProcessPeterson : MonoBehaviour
{
    public static int turn;
    public static bool[] flags = new bool[2];

    [SerializeField] private Process[] processes;
    [SerializeField] private Transform criticalRegion;
    [SerializeField] private Transform waitRegion;
    [SerializeField] private GameObject startButton;

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
        int other = 1 - pID;

        flags[pID] = true;
        turn = other;
        processes[pID].RaiseFlag();

        yield return new WaitForSeconds(2f);
        processes[pID].transform.position = waitRegion.position;

        while (flags[other] == true && turn == other)
        {
            //Espera
            yield return null;
        }

        //Seção Crítica
        StartCoroutine(CriticalSection(pID));

    }

    IEnumerator CriticalSection(int pID)
    {
        processes[pID].transform.position = criticalRegion.position;
        processes[pID].EnableTimer();

        yield return new WaitForSeconds(5f);

        flags[pID] = false;
        processes[pID].LowerFlag();
        processes[pID].ResetPosition();
        TryReactivateStart();
    }


    void ResetAll()
    {
        StopAllCoroutines();
        for (int i = 0; i < processes.Length; i++)
        {
            processes[i].ResetPosition();
            processes[i].LowerFlag();
            processes[i].DisableSlider();
            flags[i] = false;
        }

        startButton.SetActive(true);
    }


    void TryReactivateStart()
    {
        startButton.SetActive(
            flags[0] is false && flags[1] is false ? true : false
            );
    }
}
