using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            CargaPreferencias();
        }
        else{
            CargaPreferencias();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeVolume () {
        //AudioListener.volume = volumeSlider.value; 
        SFXManager.instance.mainSong.volume=volumeSlider.value;
        GuardaPreferencias();
    }

    private void CargaPreferencias () {
        volumeSlider.value=PlayerPrefs.GetFloat("musicVolume");
    }

    private void GuardaPreferencias () {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
