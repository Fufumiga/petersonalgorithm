using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Process : MonoBehaviour
{
    [SerializeField] private GameObject flag;
    [SerializeField] private Slider slider;
    
    private Vector3 originalPosition;

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

    public void DisableSlider()
    {
        slider.gameObject.SetActive(false);
    }

    public void EnableTimer()
    {
        slider.gameObject.SetActive(true);
        StartCoroutine(SliderFill());
    }

    IEnumerator SliderFill()
    {
        float timer = 0.0f;
        while(timer <= 5f)
        {
            timer += Time.deltaTime;
            slider.value = timer;

            yield return null;
        }

        slider.gameObject.SetActive(false);
    }
}
