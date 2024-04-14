using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]private GameObject[] loopFases;

    private player player;


    // Start is called before the first frame update
    void Start()
    {

        player = FindAnyObjectByType<player>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(player.GetLoopState() == 0)
        {
            loopFases[0].SetActive(true);
            loopFases[1].SetActive(false);
            loopFases[2].SetActive(false);
        }
        else if (player.GetLoopState() == 1)
        {
            loopFases[0].SetActive(false);
            loopFases[1].SetActive(true);
            loopFases[2].SetActive(false);
        }
        else if (player.GetLoopState() == 2)
        {
            loopFases[0].SetActive(false);
            loopFases[1].SetActive(false);
            loopFases[2].SetActive(true);
        }

    }
}
