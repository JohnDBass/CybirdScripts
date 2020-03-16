using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicAudio : MonoBehaviour {

    public AudioClip comicLine; // Audio file containing comic dialogue
    private AudioSource source;

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
        source.PlayOneShot(comicLine);
    }
}
