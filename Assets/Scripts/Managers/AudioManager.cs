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

    public void PlayMenuItemHover() {
        menuItemHover.Play();
    }

    public void PlayFire() {
        fire.Play();
    }

    public void PlayCoinCollect() {
        coin.Play();
    }

}
