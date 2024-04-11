using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private AudioClip[] songs;

    [SerializeField] private AudioSource aSource;

    [SerializeField]private AudioListener aListener;

    [SerializeField] private GameObject songMenu;

    [SerializeField] private GameObject Grid;

    private detect detectSong;


    // Start is called before the first frame update
    void Start()
    {

        songs = Resources.LoadAll<AudioClip>("audios");

        foreach (AudioClip clip in songs)
        {
            Debug.Log(clip.name);
            Debug.Log(clip.length);

            GameObject songMen = Instantiate(songMenu);

            songMen.transform.SetParent(Grid.transform, true);

            detectSong = songMen.GetComponent<detect>();

            detectSong.setting(clip.name, clip.length, clip);

            /*
            if (TryGetComponent<detect>(out detect hinge))
            {
                hinge.naming(clip.name,clip.length);
            }
            */


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


    public void PlayClip(AudioClip song)
    {
        aSource.clip = song;
        aSource.Play();

    }


    public void Pause()
    {
        aSource.Pause();
    }


}
