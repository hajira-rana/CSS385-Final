using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dig : MonoBehaviour
{
    public AudioSource audi;
    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed = 1;
    int idx;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        StartDialogue();
        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            if(text.text == lines[idx]){
                NextLine();
            } else{
                StopAllCoroutines();
                text.text = lines[idx];
            }
        }
    }
    void StartDialogue(){
        idx = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine(){
        foreach (char c in lines[idx].ToCharArray()){
            text.text+=c;
            audi.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine(){
        if(idx < lines.Length-1){
            idx++;
            text.text = "";
                    StartCoroutine(TypeLine());

        }else{
            gameObject.SetActive(false);
        }
    }
    public void meow(string[] blah){
        text.text = "";
        lines = blah;
        StartDialogue();
    }
}
