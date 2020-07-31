using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField] 
    private float _waitTime = 2f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int index)
    {
        // play animation
        _animator.SetTrigger("start");

        // let's wait for the animation to finish
        yield return new WaitForSeconds(_waitTime);

        // finally load scene
        SceneManager.LoadScene(index);
    }
}
