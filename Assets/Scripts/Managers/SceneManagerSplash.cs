using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerSplash : MonoBehaviour
{
    public Image carga;
    public float maxcharge;

    float c;

    private void Awake()
    {
        ChargeImage(c / maxcharge);
    }

    public void Update()
    {
        Splash();
    }

    public void Splash()
    {
        StartCoroutine(SplashEnumerator());
    }

    IEnumerator SplashEnumerator()
    {       
        c += Time.deltaTime;

        ChargeImage(c / maxcharge);

        if(c >= maxcharge)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            SceneManager.LoadScene("Menu");       

        }

    }
     
    public void ChargeImage(float percent)
    {
        carga.fillAmount = percent;
    }
}
