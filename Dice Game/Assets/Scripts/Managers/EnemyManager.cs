using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// Maneja a los enemigos.
    /// </summary>

    public List<Enemy> enemiesListLv1 = new List<Enemy>();
    public List<Enemy> enemiesListLv2 = new List<Enemy>();
    public List<Enemy> enemiesListLv3 = new List<Enemy>();
    public List<Enemy> bossList = new List<Enemy>();
    [SerializeField] public Sprite[] enemySprites; //= new Sprite[7];
    [SerializeField] public Sprite[] miniSprites;


    //Se saco uno de los 4 enemigos


    // Es el numero de elementos en el enum de abajo.
    //private int enemyNumber = 4;
    public enum EnemyName
    {
        ENEMIGO1, // Robots
        ENEMIGO2,
        ENEMIGO3,
        ENEMIGO4, // Togaman
        ENEMIGO5,
        ENEMIGO6,
        ENEMIGO7, // Soldados
        ENEMIGO8,
        ENEMIGO9,
    }
    // Start is called before the first frame update
    void Start()
    {
        PopulateGame(100);
        PopulateBosses();
    }

    public static EnemyManager instance = null;
    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }
    void AddEnemy(EnemyName name, List<Enemy> outputList)
    {
        Enemy enemy = new Enemy(enemySprites[0], miniSprites[0], "default", 50, 50);

        switch (name)
        { // ARREGLAR SPRITES DE LOS ENEMIGOS

            case EnemyName.ENEMIGO1:
                //enemy = new Enemy(enemySprites[1], "Hadita", 30, 10, 5);
                enemy = new Enemy(enemySprites[0], miniSprites[0], "Robot 1", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO2:
                //enemy = new Enemy(enemySprites[2], "Hadita con Vestido", 30, 5, 10);
                enemy = new Enemy(enemySprites[1], miniSprites[0], "Robot 2", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO3:
                //enemy = new Enemy(enemySprites[3], "Fantasma Japones", 45, 0, 0);
                enemy = new Enemy(enemySprites[2], miniSprites[0], "Robot 3", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO4:
                //enemy = new Enemy(enemySprites[4], "Hadita Azul", 15, 15, 15);
                enemy = new Enemy(enemySprites[3], miniSprites[0], "Togaman 1", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO5:
                //enemy = new Enemy(enemySprites[4], "Hadita Azul", 15, 15, 15);
                enemy = new Enemy(enemySprites[4], miniSprites[0], "Togaman 2", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO6:
                //enemy = new Enemy(enemySprites[4], "Hadita Azul", 15, 15, 15);
                enemy = new Enemy(enemySprites[5], miniSprites[0], "Togaman 3", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO7:
                //enemy = new Enemy(enemySprites[4], "Hadita Azul", 15, 15, 15);
                enemy = new Enemy(enemySprites[6], miniSprites[0], "Soldado 1", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO8:
                //enemy = new Enemy(enemySprites[4], "Hadita Azul", 15, 15, 15);
                enemy = new Enemy(enemySprites[7], miniSprites[0], "Soldado 2", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO9:
                //enemy = new Enemy(enemySprites[4], "Hadita Azul", 15, 15, 15);
                enemy = new Enemy(enemySprites[8], miniSprites[0], "Soldado 3", 10, 0, 0);
                break;
        }
        outputList.Add(enemy);
        //Debug.Log("Se agrego enemigo " + enemy.name);
    }

    // Agrega X enemigos al juego.
    void PopulateGame(int quantity)
    {
        // For lv 1
        for (int i = 0; i < quantity; i++) // Los del lv 1
        {
            AddEnemy((EnemyName)Random.Range(0, 3), enemiesListLv1);
            Debug.Log("Creado Enemigo " + enemiesListLv1[i].name);
        }
        // For lv 2
        for (int i = 0; i < quantity; i++) // Los del lv 2
        {
            AddEnemy((EnemyName)Random.Range(3, 6), enemiesListLv2);
            //Debug.Log("Creado Enemigo "+ enemiesListLv2[i].name);
        }
        // For lv 3
        for (int i = 0; i < quantity; i++) // Los del lv 3
        {
            AddEnemy((EnemyName)Random.Range(6, 9), enemiesListLv3);
            //Debug.Log("Creado Enemigo "+ enemiesListLv3[i].name);
        }
    }
    void PopulateBosses()
    {
        // Genera los bosses para la peleas
        Enemy boss1 = new Enemy(enemySprites[9], miniSprites[1], "Jefe1", 60, 10, 10); // Cerbero
        Enemy boss2 = new Enemy(enemySprites[10], miniSprites[1], "Jefe2", 40, 15, 20); // Jefe 1
        Enemy boss3 = new Enemy(enemySprites[11], miniSprites[1], "Jefe3", 150, 0, 0); // Jefe 2
        // Lo agregamos a la lista.
        bossList.Add(boss1);
        bossList.Add(boss2);
        bossList.Add(boss3);
    }
}
