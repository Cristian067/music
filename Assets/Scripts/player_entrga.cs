using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    private AudioClip[] songs;

    private int idPlaying;

    //[SerializeField]private List<int> songsList;
    [SerializeField]private List<AudioClip> songsList;

    [SerializeField] private AudioSource aSource;

    [SerializeField]private AudioListener aListener;

    [SerializeField] private GameObject songMenu;

    [SerializeField] private GameObject Grid;

    [SerializeField] private Slider slider;

    [SerializeField] private Slider songLenght;

    [SerializeField] private TextMeshProUGUI songNameText;

    [SerializeField] private GameObject containerObject;

    private detect detectSong;

    private bool isPaused = false;

    private int looped = 0;

    private bool isMuted = false;


    // Start is called before the first frame update
    void Start()
    {


        slider.value = aSource.volume;

        ReloadSongs();

        
        

        //foreach (AudioClip clip in songs)
        

            /*
            if (TryGetComponent<detect>(out detect hinge))
            {
                hinge.naming(clip.name,clip.length);
            }
            */


            //Instantiate(clip);
            //aSource.clip = clip;
            //aSource.Play();

        

        //Debug.Log(Resources.LoadAll("/audios"));
        //Debug.Log(songs);

    }

    // Update is called once per frame
    void Update()
    {


        StartCoroutine(SongTime());

        if (looped == 0)
        {
            if (!aSource.isPlaying && !isPaused)
            {
                aSource.clip = null;
            }

        }
        else if (looped == 1 && aSource.clip != null)
        {
            if(!aSource.isPlaying && !isPaused)
            {
                idPlaying++;
                aSource.clip = songsList[idPlaying];
                
                aSource.Play();
                Debug.Log(songsList[idPlaying]);
                if(idPlaying > songsList.Count)
                {
                    idPlaying = 0;
                    aSource.clip = songsList[idPlaying-1];
                    
                }
            }

        }
        else if (looped == 2 && !isPaused && aSource.clip != null)
        {
            if (!aSource.isPlaying)
            {
                aSource.clip = songsList[idPlaying];
                aSource.Play();
                Debug.Log(songsList[idPlaying]);
            }

        }

        if (aSource.isPlaying)
        {
            songLenght.maxValue = songs[idPlaying].length;
        }
        


    }


    public void PlayClip(AudioClip song, int id)
    {
        songNameText.text = song.name;
        aSource.clip = songsList[id];
        idPlaying = id;
        isPaused = false;

        aSource.Play();

    }


    public void Pause()
    {
        if(!isPaused)
        {
            
            aSource.Pause();
            isPaused = true;
            
        }
        else if(isPaused)
        {
            aSource.UnPause();
            isPaused = false;
        }
        //aSource.Pause();
    }

    public void Loop()
    {
        if (looped == 0)
        {
            looped++;

        }
        else if (looped == 1)
        {
            looped++;

        }
        else if (looped == 2)
        {
            looped = 0;

        }
    }

    public void NextClip()
    {
        idPlaying++;
        
        if (idPlaying >= songsList.Count)
        {
            idPlaying = 0;
            aSource.clip = songsList[idPlaying];
        }
        aSource.clip = songsList[idPlaying];
        
        
        aSource.Play();
    }
    public void BackClip()
    {
        idPlaying--;
        
        
        if (idPlaying < 0)
        {
            idPlaying = 0;
        }
        aSource.clip = songsList[idPlaying];
        aSource.Play();
    }

    public void ChangeVolume(float volume)
    {
        isMuted = false;
        aSource.mute = false;
        aSource.volume = volume;
    }

    public void Mute()
    {
        if(!isMuted)
        {
            aSource.mute = true;
            isMuted=true;
        }
        else if (isMuted)
        {
            aSource.mute = false;
            isMuted=false;
        }
    }

    public void StopSong()
    {
        aSource.Stop();
        idPlaying = -1;
        songNameText.text = string.Empty;
        songLenght.value = 0;
        
    }

    public int GetLoopState()
    {

        return looped;

    }

     public IEnumerator SongTime()
    {
        while (aSource.isPlaying)
        {
            songLenght.value = aSource.time;
            yield return null;
        }
        
    }

    public void ReloadSongs()
    {
        songs = Resources.LoadAll<AudioClip>("audios");

        foreach (Transform child in Grid.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < songs.Length; i++)
        {
            Debug.Log(songs[i].name);
            Debug.Log(songs[i].length);
            songsList.Add(songs[i]);

            GameObject songMen = Instantiate(songMenu);
            songMen.name = "songMenu";
            songMen.transform.SetParent(Grid.transform, true);

            detectSong = songMen.GetComponent<detect>();

            detectSong.setting(i, songs[i].name, songs[i].length, songs[i]);

    
        }
    }

}
