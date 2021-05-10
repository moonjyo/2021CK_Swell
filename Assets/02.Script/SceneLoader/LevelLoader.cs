using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public float TransitionTIme = 1f;

    public static LevelLoader Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;    
        }

        DontDestroyOnLoad(Instance);
    }



    public void LoadNextLevel()
    {
       StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadNextLevel(int Index)
    {
        StartCoroutine(LoadLevel(Index));
    }

    IEnumerator LoadLevel(int  levelIndex)
    {
        //play animation 

       // transition.SetTrigger("Start");
        //wait

        yield return new WaitForSeconds(TransitionTIme);

        //load scene
        SceneManager.LoadScene(levelIndex);

    }

}


public enum LoadSceneIndex
{
    MotherTalk = 0, 
    LivingRoom1 = 1,
    Cutscene1 = 2,

}