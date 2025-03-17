using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] int loadDelay = 2;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;
   

    bool isControllable = true;
    bool isCollidable = true;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
       // RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
         NextLevel();   
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
                isCollidable =!isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other) 
   {
        if(!isControllable || !isCollidable){ return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
   }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void NextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
     private void StartCrashSequence()
    {
        isControllable = false;
        crashParticles.Play();
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", loadDelay);
    }
     private void StartNextLevelSequence()
     {
        isControllable = false;
        successParticles.Play();
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSound);
        Invoke("NextLevel", loadDelay);
     }
}
