using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OverWorldManager : MonoBehaviour
{
    /// <summary>
    /// Maneja el screen principal antes de los combates.
    /// </summary>

    // Nivel actual va de 1 a 3
    public int currentLevel = 1;
    public int currentButton = 0;
    public GameObject playerSprite;
    [SerializeField] private GameObject canvas;
    [SerializeField] GameObject initialPlayerPos;

    [Header("Mapas")]
    public GameObject mapa1;
    public GameObject mapa2;
    public GameObject mapa3;
    public GameObject mapa4;
    public GameObject mapa5;
    public GameObject mapa6;
    public GameObject mapa7;

    // Para cambiar de lv.
    public bool levelCompleted = false;
    // Para las narrativas
    public List<GameObject> narrativePanels = new List<GameObject>();
    int narrativeIndex = 0;

    public static OverWorldManager instance = null;
    public string currentLevelName;
    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(playerSprite.transform.position);
        ChangeToNextMap(currentLevel);
        // Creamos los eventos para los botones.
        GameManager.instance.BuildLevelMap();
    }

    public void ChangeToNextMap(int levelNumber)
    {
        // Prendemos la narrativa que corresponda ?
        ActivateNextNarrative();

        // Reseteamos el numero de boton.
        currentButton = 0;
        
        GameObject holder;
        switch (levelNumber)
        {
            case 1:
                holder = Instantiate(mapa1, canvas.transform);
                break;
            case 2:
                holder = Instantiate(mapa2, canvas.transform);
                break;
            case 3:
                holder = Instantiate(mapa3, canvas.transform);
                break;
            case 4:
                holder = Instantiate(mapa4, canvas.transform);
                break;
            case 5:
                holder = Instantiate(mapa5, canvas.transform);
                break;
            case 6:
                holder = Instantiate(mapa6, canvas.transform);
                break;
            case 7:
                holder = Instantiate(mapa7, canvas.transform);
                break;
            default:
                holder = Instantiate(mapa1, canvas.transform);
                Debug.Log("Se activo caso default");
                break;
        }
        holder.transform.SetAsFirstSibling();
        currentLevelName = holder.name;
        playerSprite.transform.position = initialPlayerPos.transform.position;
    }

    public void CompleteLevel()
    {
        levelCompleted = true;
    }

    void ActivateNextNarrative()
    {
        // Apagamos x si hay alguno prendido
        foreach(GameObject panel in narrativePanels)
        {
            panel.SetActive(false);
        }
        // Prendemos el que corresponda.
        // Si existe..
        if(narrativeIndex <= narrativePanels.Count - 1)
        {
            narrativePanels[narrativeIndex].SetActive(true);
            narrativeIndex++;
        }
    }
}
