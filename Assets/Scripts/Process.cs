using UnityEngine;

public class Process : MonoBehaviour
{
    [SerializeField] GameObject flag;
    private Vector3 originalPosition;

    [SerializeField] private Transform criticalRegion;
    [SerializeField] private Transform waitRegion;

    void Start()
    {
        originalPosition = transform.localPosition;
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
        transform.localPosition = originalPosition;
    }
}
