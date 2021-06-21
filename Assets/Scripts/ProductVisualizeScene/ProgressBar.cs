using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ShowProgressInBar());
    }

    private IEnumerator ShowProgressInBar()
    {
        //float progress = ProductManager.Instance.GetDownloadProgress();
        float progress = ProductManager.Instance.DownloadProgress;

        if (progress > 0 && progress < 1)
        {
            slider.gameObject.SetActive(true);
            slider.value = progress;
        }
        else
        {
            slider.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
    }
}
