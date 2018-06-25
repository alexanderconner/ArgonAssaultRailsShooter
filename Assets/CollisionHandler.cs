using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [Tooltip("ParticleFX prefab on player. Set to disabled.")] [SerializeField] GameObject deathFX;
    [Tooltip("In seconds, load level x amount of time after player death.")] [SerializeField] float levelLoadDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        print("Player triggered: " + other.gameObject.name.ToString());
        StartDeathSequence();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        gameObject.SendMessage("PlayerDied");
        deathFX.SetActive(true);

    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
