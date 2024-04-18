using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class player : MonoBehaviour
{
    private UIManager uiManager;

    private AudioClip[] songs;

    private int idPlaying;

    [SerializeField]private List<AudioClip> songsList;

    [SerializeField] private AudioSource aSource;

    [SerializeField]private AudioListener aListener;

    [SerializeField] private GameObject songMenu;

    [SerializeField] private GameObject Grid;

    [SerializeField] private Slider slider;

    [SerializeField] private Slider songLenght;

    [SerializeField] private TextMeshProUGUI songNameText;

    [SerializeField] private GameObject containerObject;

    [SerializeField] private Texture2D backgroundImg ;
    [SerializeField] private Image background;

    private detect detectSong;

    private bool isPaused = false;

    private int looped = 0;

    private bool isMuted = false;

    [SerializeField]private bool isRandom = false;


    // Start is called before the first frame update
    void Start()
    {


        slider.value = aSource.volume;

        ReloadSongs();

        uiManager = FindAnyObjectByType<UIManager>();

       

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SongTime());

        if (aSource.clip == null && isRandom)
        {
            int random = Random.Range(0, songs.Length);
            PlayClip(songsList[random],random );
            aSource.Play();
        }

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
        uiManager.PauseButtonChange(isPaused);

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

        uiManager.PauseButtonChange(isPaused);
        
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
            //PlayClip(songsList[idPlaying], idPlaying);
        }
        PlayClip(songsList[idPlaying], idPlaying);
        
        
        aSource.Play();
    }
    public void BackClip()
    {
        idPlaying--;
        
        
        if (idPlaying < 0)
        {
            idPlaying = 0;
        }
        PlayClip(songsList[idPlaying], idPlaying);
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
        isRandom = false;
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

    public void RandomButton()
    {
        if (!isRandom)
        {
            isRandom = true;

        }
        else if (isRandom)
        {
            isRandom = false;
        }

        uiManager.RandomButtonChange(isRandom);
        
        
    }

    /*
    public IEnumerator RandomSong()
    {
        if (!aSource.isPlaying && random)
        {
            aSource.clip = songsList[Random.Range(0, songs.Length)];
            yield return null; //new WaitForSeconds(1);
        }

    }*/

    public void ReloadSongs()
    {
        songs = Resources.LoadAll<AudioClip>("audios");
        songsList.Clear();

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

    public void ChangeBackground()
    {
        //Texture2D background = new Texture2D(2, 2);
        string path = EditorUtility.OpenFilePanel("Select a image for the background", "", "png");

        if (path.Length != 0)
        {
            WWW www = new WWW("file:///" + path);

            backgroundImg = www.texture;
            //www.texture = EditorUtility.OpenFilePanel("Select a image for the background", "", "png");
            //background = www.LoadImageIntoTexture(backgroundimg);
            //background.sprite = www.LoadImageIntoTexture(backgroundImg);
        }
    }


}
