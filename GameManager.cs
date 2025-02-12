using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject controlMenuUI;
    CinemachineVirtualCamera virtualCam;

    [SerializeField] Transform player;
    [SerializeField] Transform boundLeft;
    [SerializeField] Transform boundRight;

    [SerializeField] float xOffset;

    private void Awake()
    {
        if (virtualCam == null)
            return;
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.position.x < boundLeft.position.x - xOffset || player.position.x > boundRight.position.x + xOffset)
        //{
        //    virtualCam.enabled = false;
        //    Debug.Log("GAME OVER");
        //}
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }    

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Victory()
    {
        print("VVV");
        SceneManager.LoadScene("Victory");
    }

    public void Title()
    {
        print("TTT");
        SceneManager.LoadScene("Start Screen");
    }

    public void ShowControl()
    {
        controlMenuUI.SetActive(true);
    }

    public void HideControl()
    {
        controlMenuUI.SetActive(false);
    }
}
