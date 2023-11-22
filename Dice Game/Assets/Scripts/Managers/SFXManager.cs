using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    /// <summary>
    /// Permite reproducir SFX
    /// </summary>
    [SerializeField] string sceneName;
    public AudioSource mainSong;
    // El objeto que se va a generar cada vez que se pide un SFX
    public GameObject parlante;
    public float volumen;
    

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
    public AudioClip charSelectionSong;
    public AudioClip tutorialSong;
    public AudioClip nivel1;
    public AudioClip nivel2;
    public AudioClip nivel3;
    public AudioClip CombateDefault;
    public AudioClip CombateBoss;
    public AudioClip finalBattleSong;

    float effectModifier = 1f;

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
        CharSelection,
        Tutorial,
        Nivel1,
        Nivel2,
        Nivel3,
        CombatDefault,
        CombateBoss,
        FinalBattle,
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
        RegulateEffectsVolume();
    }
    void OnEnable()
    {
        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Asegúrate de desuscribirte del evento al desactivar el script para evitar fugas de memoria
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Dependiendo de la escena cargada, cambia la canción principal (main song)
        string sceneName = scene.name;
        
        // Detiene la canción actual si está reproduciéndose
        if (mainSong.isPlaying)
        {
            mainSong.Stop();
        }

        // Reproducir la canción correspondiente a la escena cargada
        switch (sceneName)
        {
            case "MenuPrincipal":
                PlaySong(MSName.MENU);
                break;
            case "CharSelection":
                PlaySong(MSName.CharSelection);
                break;
            case "Tutorial":
                PlaySong(MSName.Tutorial); 
                break;
            case "GameScene":
                PlaySong(MSName.Nivel1);
                break;
            case "FinalBattle":
                PlaySong(MSName.FinalBattle);
                break;
            // Agrega más casos según las escenas que tengas
            default:
                PlaySong(MSName.MENU); // Establecer una canción predeterminada para otras escenas no definidas
                break;
        }
    }

    // Funcion que reproduce cualquier VFX que queramos.
    public void PlayVFX(VFXName sonido){
        // Creamos un parlante
        RegulateEffectsVolume();
        GameObject efectoSonoro = Instantiate(parlante);
        // Lo colocamos como hijo del VFX Manager, para no spamear la jerarquia.
        efectoSonoro.transform.parent = transform;
        // Le ponemos un nombre para identificarlo
        efectoSonoro.name = "VFX SOUND - " + sonido.ToString();
        // Accedemos a su audioSource y seteamos el clip, el soonido
        AudioSource audioS = efectoSonoro.GetComponent<AudioSource>();
        audioS.clip = clipLibrary[sonido].Item1;
        audioS.volume = clipLibrary[sonido].Item2 * effectModifier;
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
        mainSongLibrary.Add(MSName.MENU, Tuple.Create(menuSong, 1f));
        mainSongLibrary.Add(MSName.CharSelection, Tuple.Create(charSelectionSong, 1f));
        mainSongLibrary.Add(MSName.Tutorial, Tuple.Create(tutorialSong, 1f));
        mainSongLibrary.Add(MSName.Nivel1, Tuple.Create(nivel1, 1f));
        mainSongLibrary.Add(MSName.Nivel2, Tuple.Create(nivel2, 1f));
        mainSongLibrary.Add(MSName.Nivel3, Tuple.Create(nivel3, 1f));
        mainSongLibrary.Add(MSName.CombatDefault, Tuple.Create(CombateDefault, 1f));
        mainSongLibrary.Add(MSName.CombateBoss, Tuple.Create(CombateBoss, 1f));
        mainSongLibrary.Add(MSName.FinalBattle, Tuple.Create(finalBattleSong, 1f));
    }

    // Esta funcion utiliza el Music Player Original.
    /*public void PlaySong(MSName song){
        if(mainSong.isPlaying)
        {
            mainSong.Stop();
        } 
        // Seteamos los valores.
        mainSong.clip = mainSongLibrary[song].Item1;
        mainSong.volume = mainSongLibrary[song].Item2;
        mainSong.Play();
    }*/
    public void PlaySong(MSName song)
    {
        StartCoroutine(FadeOutAndPlay(song));
    }

    private IEnumerator FadeOutAndPlay(MSName song)
    {
        float fadeDuration = 1.0f; // Duración del fade-out en segundos

        if (mainSong.isPlaying)
        {
            float startVolume = mainSong.volume;

            float currentTime = 0;
            while (currentTime < fadeDuration)
            {
                currentTime += Time.deltaTime;
                mainSong.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeDuration);
                yield return null;
            }

            mainSong.Stop();
        }

        // Seteamos los valores y reproducimos la nueva canción
        mainSong.clip = mainSongLibrary[song].Item1;
        mainSong.volume = 0f;
        mainSong.Play();

        // Realizamos el Fade-In
        float targetVolume = mainSongLibrary[song].Item2 * PlayerPrefs.GetFloat("musicVolume");
        float currentVolume = 0f;

        while (currentVolume < targetVolume)
        {
            currentVolume += Time.deltaTime;
            mainSong.volume = Mathf.Lerp(0, targetVolume, currentVolume / fadeDuration);
            yield return null;
        }
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
            Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
        }
    }
    void RegulateEffectsVolume()
    {
        if (PlayerPrefs.HasKey("effectVolume"))
        {
            effectModifier = PlayerPrefs.GetFloat("effectVolume");
        }
    }
    public AudioClip GetAudioClip()
    {
        return mainSong.clip;
    }


}
