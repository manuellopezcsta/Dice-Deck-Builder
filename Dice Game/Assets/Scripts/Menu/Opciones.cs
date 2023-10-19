using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Opciones : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider volumeSlider;
    [SerializeField] TMP_Dropdown textSpeedSelection;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            //CargaPreferencias();
        }
        if(!PlayerPrefs.HasKey("dialogueSpeed")){
            PlayerPrefs.SetInt("dialogueSpeed", 0);
        }
        CargaPreferencias();
    }

    public void ChangeVolume () {
        //AudioListener.volume = volumeSlider.value; 
        SFXManager.instance.mainSong.volume=volumeSlider.value;
        GuardaPreferencias();
    }

    private void CargaPreferencias () {
        volumeSlider.value=PlayerPrefs.GetFloat("musicVolume");
        textSpeedSelection.value=PlayerPrefs.GetInt("dialogueSpeed");
    }

    private void GuardaPreferencias () {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void ChangeDialogueSpeed (int selection) {
        if(selection == 0){
            DialogueManager.instance.dialogueSpeed=10;
        }
        if(selection == 1){
            DialogueManager.instance.dialogueSpeed=5;
        }
        if(selection == 2){
            DialogueManager.instance.dialogueSpeed=0;
        }
        SetDialogueSpeed();
    }

    public void SetDialogueSpeed(){
        PlayerPrefs.SetInt("dialogueSpeed", textSpeedSelection.value);
    }
}
