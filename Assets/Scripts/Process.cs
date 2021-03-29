using System.Collections;
using UnityEngine;

public class Process : MonoBehaviour
{
    public float executionTime;
    [SerializeField] GameObject flag;
    private Vector3 originalPosition;

    [SerializeField] private Transform criticalRegion;
    [SerializeField] private Transform waitRegion;

    void Start()
    {
        executionTime = Random.Range(10, 15);
    }


    public void TwoProcAlgorithm(int pID)
    {
        int other = 1 - pID;

        TwoProcessPeterson.flags[pID] = true;
        RaiseFlag();
        TwoProcessPeterson.turn = other;

        while (TwoProcessPeterson.flags[other] == true && TwoProcessPeterson.turn == other)
        {
            //Espera
            transform.position = waitRegion.position;
        }

        //Região Crítica
        StartCoroutine(CriticalTask(pID));

    }

    IEnumerator CriticalTask(int pID)
    {
        transform.position = criticalRegion.position;
        yield return new WaitForSeconds(executionTime);
        TwoProcessPeterson.flags[pID] = false;
        TwoProcessPeterson.turn = 1 - pID;
        LowerFlag();
    }

    public void RaiseFlag()
    {
        flag.SetActive(true);
    }

    public void LowerFlag()
    {
        flag.SetActive(false);
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
    }
}
