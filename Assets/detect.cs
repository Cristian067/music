using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class detect : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI lenght;


    // Start is called before the first frame update
    void Start()
    {

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void naming(string name, string lenghtn)
    {
        title.text = name;
        lenght.text = lenghtn;
    }



}
