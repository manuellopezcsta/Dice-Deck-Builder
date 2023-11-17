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
    public AudioClip ataqueSound;
    public AudioClip mataqueSound;
    public AudioClip armaduraSound;
    public AudioClip mrSound;
    public AudioClip venenoSound;
    public AudioClip romperSound;
    public AudioClip potenciarSound;
    public AudioClip curarSound;
    public AudioClip parrySound;
    public AudioClip bloquearSound;
    public AudioClip poisonTickSound;
    public AudioClip hoverSound;

    [Header("Clips de Main Song")]
    public AudioClip menuSong;
    public AudioClip nivel1Song;
    public AudioClip nivel2Song;
    public AudioClip combateSong;
    public AudioClip gameOverSong;


    // El Enum que se usa de Key para llamar a la funcion PlayVFX.
    public enum VFXName {
        ATAQUE,
        MATAQUE,
        ARMADURA,
        MR,
        VENENO,
        ROMPER,
        POTENCIAR,
        CURAR,
        PARRY,
        BLOQUADOR,
        POISON_TICK,
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
        // Para que no se destuya.
        DontDestroyOnLoad(this.gameObject);   // Tendria q ser un objeto separado para q funcione , sino no te lo va a tomar si es hijo de manager.
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
        
        //LLenamos la libreria de Sonidos
        FillSoundLibrary();
        RegulateVolume();
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
        clipLibrary.Add(VFXName.ATAQUE, Tuple.Create(ataqueSound, 1f));
        clipLibrary.Add(VFXName.MATAQUE, Tuple.Create(mataqueSound, 0.6f));
        clipLibrary.Add(VFXName.ARMADURA, Tuple.Create(armaduraSound, 0.6f));
        clipLibrary.Add(VFXName.MR, Tuple.Create(mrSound, 0.28f));
        clipLibrary.Add(VFXName.VENENO, Tuple.Create(venenoSound, 0.28f));
        clipLibrary.Add(VFXName.ROMPER, Tuple.Create(romperSound, 0.28f));
        clipLibrary.Add(VFXName.POTENCIAR, Tuple.Create(potenciarSound, 0.28f));
        clipLibrary.Add(VFXName.CURAR, Tuple.Create(curarSound, 0.28f));
        clipLibrary.Add(VFXName.PARRY, Tuple.Create(parrySound, 0.28f));
        clipLibrary.Add(VFXName.BLOQUADOR, Tuple.Create(bloquearSound, 0.28f));
        clipLibrary.Add(VFXName.POISON_TICK, Tuple.Create(poisonTickSound, 0.28f));
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

    void RegulateVolume()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            mainSong.volume = PlayerPrefs.GetFloat("musicVolume");
        }
    }

}
