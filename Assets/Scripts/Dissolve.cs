using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDissolved;
    bool isDissolving;
    float fade = 1f;
    
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    public void DissolveDeath(){
        isDissolving = true;
        
    }

    public bool hasDissolved(){
        return isDissolved;
    }

    void Update(){
        if(isDissolving){
            fade -= Time.deltaTime;
            if(fade <= 0f){
                fade = 0f;
                isDissolving = false;
                isDissolved = true;
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
