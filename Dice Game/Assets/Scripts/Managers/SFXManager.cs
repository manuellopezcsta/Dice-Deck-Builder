using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    /// <summary>
    /// Permite reproducir SFX
    /// </summary>

    public AudioSource mainSong;
    // El objeto que se va a generar cada vez que se pide un SFX
    public GameObject parlante;

    [Header("Clips de VFX")]
    public AudioClip hitSound;
    public AudioClip rollSound;
    public AudioClip pickupSound;
    public AudioClip hoverSound;

    [Header("Clips de Main Song")]
    public AudioClip menuSong;
    public AudioClip nivel1Song;
    public AudioClip nivel2Song;
    public AudioClip combateSong;
    public AudioClip gameOverSong;


    // El Enum que se usa de Key para llamar a la funcion PlayVFX.
    public enum VFXName {
        HIT,
        ROLL,
        PICKUP,
        HOVER
    }

    public enum MSName {
        MENU,
        NIVEL1,
        NIVEL2,
        COMBATE,
        GAME_OVER
    }
    // Un diccionario que va a contener todos los clips de audio con su respectivos volumenes.
    Dictionary<VFXName, Tuple<AudioClip, float>> clipLibrary = new Dictionary<VFXName, Tuple<AudioClip, float>>();
    Dictionary<MSName, Tuple<AudioClip, float>> mainSongLibrary = new Dictionary<MSName, Tuple<AudioClip, float>>();

    public static SFXManager instance = null;

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
        
        //LLenamos la libreria de Sonidos
        FillSoundLibrary();
        // Para que no se destuya.
        //DontDestroyOnLoad(this.gameObject);
    }

    // Funcion que reproduce cualquier VFX que queramos.
    public void PlayVFX(VFXName sonido){
        // Creamos un parlante
        GameObject efectoSonoro = Instantiate(parlante);
        // Lo colocamos como hijo del VFX Manager, para no spamear la jerarquia.
        efectoSonoro.transform.parent = transform;
        // Le ponemos un nombre para identificarlo
        efectoSonoro.name = "VFX SOUND - " + sonido.ToString();
        // Accedemos a su audioSource y seteamos el clip, el soonido
        AudioSource audioS = efectoSonoro.GetComponent<AudioSource>();
        audioS.clip = clipLibrary[sonido].Item1;
        audioS.volume = clipLibrary[sonido].Item2;
        // Lo reproducimos, y le decimos que se destruya cuando termine.
        audioS.Play();
        Destroy(efectoSonoro, audioS.clip.length); // Destruye el objeto una vez que termine de reproducirse.    
    }

    void FillSoundLibrary(){
        // Usando las keys de los enum, puedo obtener el sonido y el volumen que corresponde.
        clipLibrary.Add(VFXName.HIT, Tuple.Create(hitSound, 1f));
        clipLibrary.Add(VFXName.ROLL, Tuple.Create(rollSound, 0.6f));
        clipLibrary.Add(VFXName.PICKUP, Tuple.Create(pickupSound, 0.28f));
        clipLibrary.Add(VFXName.HOVER, Tuple.Create(hoverSound, 1f));
        
        // Los de main song
        mainSongLibrary.Add(MSName.NIVEL1, Tuple.Create(nivel1Song, 1f));
        mainSongLibrary.Add(MSName.NIVEL2, Tuple.Create(nivel2Song, 1f));
        mainSongLibrary.Add(MSName.MENU, Tuple.Create(menuSong, 1f));
        mainSongLibrary.Add(MSName.COMBATE, Tuple.Create(combateSong, 1f));
        mainSongLibrary.Add(MSName.GAME_OVER, Tuple.Create(gameOverSong, 1f));
    }

    // Esta funcion utiliza el Music Player Original.
    public void PlaySong(MSName song){
        if(mainSong.isPlaying)
        {
            mainSong.Stop();
        }
        // Seteamos los valores.
        mainSong.clip = mainSongLibrary[song].Item1;
        mainSong.volume = mainSongLibrary[song].Item2;
        mainSong.Play();
    }

    // Esta funcion es para que hagan ruido los botones al poner el mouse sobre ellos.
    // Dice que no tiene referencias pero si se esta usando en los botones.
    public void PlayButtonHoverSound(){
        PlayVFX(VFXName.HOVER);  
    }

}
