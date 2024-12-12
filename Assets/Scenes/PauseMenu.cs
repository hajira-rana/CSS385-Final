using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject PlayMenu;
    public GameObject Aim;
    public GameObject VictoryScreen;

    public GameObject player;
    SeedMechanics seed;


    void Start()
    {
        pauseMenu.SetActive(false);
        PlayMenu.SetActive(true);
        player = GameObject.Find("player");
        seed = player.GetComponent<SeedMechanics>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        if (seed.isAiming)
        {
            Aim.SetActive(true);

        }
        else
        {
            Aim.SetActive(false);

        }


    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PlayMenu.SetActive(true);

    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PlayMenu.SetActive(false);

    }
    public void Win()
    {
        VictoryScreen.SetActive(true);
        Time.timeScale = 0f;
        PlayMenu.SetActive(false);
    }
    public void Leveltwo(){
        SceneManager.LoadScene("Level 2");
    }    void Levelone(){
        SceneManager.LoadScene("Level 1");
    }
}
