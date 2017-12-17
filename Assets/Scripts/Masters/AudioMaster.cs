using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour {

    [SerializeField] AudioSource sourceMaster;

    [SerializeField] AudioClip swordSwoosh;
    [SerializeField] AudioClip slimeMovement;
    [SerializeField] AudioClip slimeDividing;
    [SerializeField] AudioClip playerGotDamage;
    
    private void PlaySound(AudioClip clip)
    {

    }


    public void PlayerDamageSound()
    {
        sourceMaster.clip = playerGotDamage;
        sourceMaster.Play();
    }

    public void SwordSound()
    {
        sourceMaster.clip = swordSwoosh;
        sourceMaster.pitch = Random.Range(0.9f, 1.1f);
        sourceMaster.Play();
    }

}
