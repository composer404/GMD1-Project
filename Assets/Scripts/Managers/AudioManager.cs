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
    private AudioSource walk;

    [SerializeField]
    private AudioSource jump;

    public void PlayMenuItemHover() {
        menuItemHover.Play();
    }

    public void PlayFire() {
        fire.Play();
    }

    public void PlayCoinCollect() {
        coin.Play();
    }

    public void PlayWalk() {
        walk.Play();
    }

    public void PlayJump() {
        jump.Play();
    }

}
