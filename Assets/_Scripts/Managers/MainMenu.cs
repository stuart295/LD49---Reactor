using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        gm.Play();
    }

    

    public void StartGame() {
        SceneManager.LoadScene(2);
    }

    public void Sandbox() {
        SceneManager.LoadScene("Sandbox");
    }


    public void Quit() {
        Application.Quit();
    }

}
