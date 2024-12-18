using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource Sound;
    public AudioClip HoverFX;

    public void hoverSound()
    {
        Sound.PlayOneShot(HoverFX);
    }

}
