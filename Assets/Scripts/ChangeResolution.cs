using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeResolution : MonoBehaviour
{
    public static ChangeResolution instance;

    Coroutine changeResolution_coroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        changeResolution_coroutine = StartCoroutine(ChangeResolutionFunction());
    }

    IEnumerator ChangeResolutionFunction()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F11));

            Screen.fullScreen = !Screen.fullScreen;

            Debug.Log(Screen.fullScreen);

            Invoke(nameof(RestartCoroutine), .5f); //Necesito para y reiniciar la corrutina porque entraba dos veces seguidas si no la paraba
            StopCoroutine(changeResolution_coroutine);
        }
    }

    void RestartCoroutine()
    {
        changeResolution_coroutine = StartCoroutine(ChangeResolutionFunction());
    }
}