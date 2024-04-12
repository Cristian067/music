using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class detect : MonoBehaviour
{
    public int id;
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI lenght;
    [SerializeField] private AudioClip song;
    
    private player player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<player>();

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setting(int idn, string name, float lenghtn, AudioClip clip)
    {
        id = idn;
        title.text = name;
        lenght.text = lenghtn.ToString();
        song = clip;
    }


    public void Play()
    {
        player.PlayClip(song, id);

    }


}
