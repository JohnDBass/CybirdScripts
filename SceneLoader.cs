using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    private void Start()
    {        

    }

    public void Quit()
    {
        Application.Quit();

    }

    public void LoadLevel1()
    {
        Debug.Log("Im trying to load");
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        Debug.Log("Im trying to load");
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        Debug.Log("Im trying to load");
        SceneManager.LoadScene("Level 3");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial Level");
    }

    public void LoadTutorialPrompt()
    {
        SceneManager.LoadScene("TutorialLoad");
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("ComicIntro");
    }

    public void LoadComicOne()
    {
        SceneManager.LoadScene("ComicLevel1");
    }

    public void LoadComicPageTwo()
    {
        SceneManager.LoadScene("ComicPage2");

    }

    public void LoadComicPageThree()
    {
        SceneManager.LoadScene("ComicPage3");
    }

    public void LoadComicPagefour()
    {
        SceneManager.LoadScene("ComicPage4");
    }


    public void LoadComicTwo()
    {
        SceneManager.LoadScene("ComicLevel2");
    }

    public void LoadComicThree()
    {
        SceneManager.LoadScene("ComicLevel3");
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
    }

    public void LoadBoss()
    {
        SceneManager.LoadScene("Level 3 Part 2");
    }

    public void LoadScore()
    {
        SceneManager.LoadScene("ScoreTable");
    }


    /* private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             SceneManager.LoadScene("End");
             Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
         }
     }
     */

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LoadNextScene();
        }
    }

}
