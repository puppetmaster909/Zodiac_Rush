using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSortLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem particleSystem;
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        particleSystem = this.GetComponent<ParticleSystem>();
        particleSystem.GetComponent<Renderer>().sortingOrder = 1;
        
        
    }

    // Update is called once per frame
    
}
