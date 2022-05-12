using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Singleton

    private static AudioManager instance;

    private void Awake() {
        instance = this;
    }

    public static AudioManager GetInstance() {
        return instance;
    }

    #endregion

    [SerializeField]
    private AudioSource menuItemHover;

    [SerializeField]
    private AudioSource fire;

    [SerializeField]
    private AudioSource coin;

    [SerializeField]
    private AudioSource jump;

    [SerializeField]
    private AudioSource gameOver;

    [SerializeField]
    private AudioSource grunt;

    [SerializeField]
    private AudioSource sword;

    [SerializeField]
    private AudioSource heart;

    [SerializeField]
    private AudioSource walk;

    [SerializeField]
    private AudioSource sprint;

    [SerializeField]
    private AudioSource monster;

    public void PlayMenuItemHover() {
        menuItemHover.Play();
    }

    public void PlayFire() {
        fire.Play();
    }

    public void PlayCoinCollect() {
        coin.Play();
    }

    public void PlayJump() {
        jump.Play();
    }

     public void PlayGameOver() {
        gameOver.Play();
    }

    public void PlayGrunt() {
        grunt.Play();
    }

    public void PlaySword() {
        sword.Play();
    }

    public void PlayHeart() {
        heart.Play();
    }

    public void PlayWalk() {
        walk.Play();
    }

    public void PlaySprint() {
        sprint.Play();
    }

    public void PlayMonster() {
        monster.Play();
    }
}
