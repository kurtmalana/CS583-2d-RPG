using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPlanet : MonoBehaviour
{
    private bool enterAllowed;
    private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DunePlanet>())
        {
            sceneToLoad = "Dunes";
            enterAllowed = true;
        }
        /*
        else if (collision.GetComponent<DunePlanet>())
        {
            sceneToLoad = "Level1";
            enterAllowed = true;
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<DunePlanet>() || collision.GetComponent<DunePlanet>())
        {
            enterAllowed = false;
        }
    }

    private void Update()
    {
        if (enterAllowed && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
