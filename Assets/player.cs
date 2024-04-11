using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private AudioClip[] songs;

    [SerializeField] private AudioSource aSource;

    [SerializeField]private AudioListener aListener;

    [SerializeField] private GameObject songMenu;


    // Start is called before the first frame update
    void Start()
    {

        songs = Resources.LoadAll<AudioClip>("audios");

        foreach (AudioClip clip in songs)
        {
            Debug.Log(clip.name);
            Debug.Log(clip.length);

            Instantiate(songMenu);


            //Instantiate(clip);
            //aSource.clip = clip;
            //aSource.Play();

        }

        //Debug.Log(Resources.LoadAll("/audios"));
        //Debug.Log(songs);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
