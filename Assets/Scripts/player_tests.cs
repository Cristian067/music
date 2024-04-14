using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows;
using UnityEngine.Networking;
using System.IO;


public class player_test : MonoBehaviour
{

    private AudioClip[] songs;
    private string[] songname;

    private int idPlaying;

    //[SerializeField]private List<int> songsList;
    [SerializeField]private List<AudioClip> songsList;

    [SerializeField] private AudioSource aSource;

    [SerializeField]private AudioListener aListener;

    [SerializeField] private GameObject songMenu;

    [SerializeField] private GameObject Grid;

    [SerializeField] private Slider slider;

    private detect detectSong;

    [SerializeField]private TextMeshProUGUI pathtext;

    private bool isPaused = false;

    private int looped = 0;

    private bool isMuted = false;


    // Start is called before the first frame update
    void Start()
    {
        slider.value = aSource.volume;

        string[] songsdir = Directory.GetFiles(Application.dataPath + "/Resources/audios/", "*.mp3") /*"file:///" +*/ /*Application.dataPath*/;

        Debug.Log(songsdir[0]);

        ///songs; //Resources.LoadAll<AudioClip>("audios");

        //Debug.Log(UnityWebRequestMultimedia.GetAudioClip(songsdir), AudioType.MPEG);

        songs = new AudioClip[songsdir.Length];
        songname = new string[songsdir.Length];
            //UnityWebRequestMultimedia.GetAudioClip(songsdir, AudioType.MPEG);
        //Debug.Log(Resources.LoadAll<AudioClip>("songsdir/Resources/audios"));

        //foreach (AudioClip clip in songs)

        //mandar a cargar los audios
        for (int i = 0; i < songsdir.Length; i++) 
        {

            string path = "file:///" + songsdir[i];

            Debug.Log(path);

            WWW www = new WWW(path);

            songs[i] = www.GetAudioClip();
            songname[i] = Path.GetFileNameWithoutExtension(songsdir[i]);
            //Debug.Log(songname[i]);
            



            Debug.Log(songname[i]);
            Debug.Log(songs[i].length);
            songsList.Add(songs[i]);

            GameObject songMen = Instantiate(songMenu);

            songMen.transform.SetParent(Grid.transform, true);

            detectSong = songMen.GetComponent<detect>();

            detectSong.setting(i, songname[i], songs[i].length, songs[i]);

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
        

        if (looped == 0)
        {

        }
        else if (looped == 1)
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
        else if (looped == 2 && !isPaused)
        {
            if (!aSource.isPlaying)
            {
                aSource.clip = songsList[idPlaying];
                aSource.Play();
                Debug.Log(songsList[idPlaying]);
            }

        }


    }


    public void PlayClip(AudioClip song, int id)
    {
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
    }

}
