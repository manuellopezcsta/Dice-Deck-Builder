using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OverWorldManager : MonoBehaviour
{
    /// <summary>
    /// Maneja el screen principal antes de los combates.
    /// </summary>

    // Nivel actual va de 1 a 4
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

    // Para cambiar de lv.
    public bool levelCompleted = false;

    public static OverWorldManager instance = null;
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
        // Reseteamos el numero de boton.
        currentButton = 0;

        GameObject holder;
        switch (levelNumber)
        {
            case 1:
                holder = Instantiate(mapa1, canvas.transform);
                //Debug.Log(holder.tag);
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
            default:
                holder = Instantiate(mapa1, canvas.transform);
                Debug.Log("Se activo caso default");
                break;
        }
        holder.transform.SetAsFirstSibling();
        playerSprite.transform.position = initialPlayerPos.transform.position;
    }

    public void CompleteLevel()
    {
        levelCompleted = true;
    }
}
