using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    [Tooltip("ParticleFX prefab on player. Set to disabled.")] [SerializeField] GameObject deathFX;
    [Tooltip("Points awarded for destroying this enemy")] [SerializeField] int scoreValue = 8;
    [Tooltip("Number of hits to kill")] [SerializeField] int health = 1;
    ScoreBoard scoreBoard;


    // Use this for initialization
    void Start() {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        gameObject.AddComponent<BoxCollider>();
    }

    void OnParticleCollision(GameObject other)
    {
        //print(gameObject.name + " Hit by Particles: " + other.gameObject.name);
        //make enemy die
        health--;
        if (health < 1 || health <= 0)
        {
            playDeathSequence();
        }
        
    }

    private void playDeathSequence()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        scoreBoard.ScoreHit(scoreValue);
        Destroy(gameObject);
    }
}
