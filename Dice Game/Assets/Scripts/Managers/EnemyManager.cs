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

    public List<Enemy> enemiesList = new List<Enemy>();
    public List<Enemy> bossList = new List<Enemy>();
    [SerializeField] public Sprite[] enemySprites; //= new Sprite[7];

    // Es el numero de elementos en el enum de abajo.
    private int enemyNumber = 4;
    public enum EnemyName
    {
        ENEMIGO1,
        ENEMIGO2,
        ENEMIGO3,
        ENEMIGO4
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
    void AddEnemy(EnemyName name)
    {
        Enemy enemy = new Enemy(enemySprites[0], "default", 50, 50);

        switch (name)
        {
            case EnemyName.ENEMIGO1:
                enemy = new Enemy(enemySprites[1], "Hadita", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO2:
                enemy = new Enemy(enemySprites[2], "Hadita con Vestido", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO3:
                enemy = new Enemy(enemySprites[3], "Fantasma Japones", 10, 0, 0);
                break;
            case EnemyName.ENEMIGO4:
                enemy = new Enemy(enemySprites[4], "Hadita Azul", 10, 0, 0);
                break;
        }
        enemiesList.Add(enemy);
        //Debug.Log("Se agrego enemigo " + enemy.name);
    }

    // Agrega X enemigos al juego.
    void PopulateGame(int quantity)
    {
        for(int i = 0; i < quantity; i++)
        {
            AddEnemy((EnemyName)Random.Range(0, enemyNumber)); 
            //Debug.Log("Creado Enemigo "+ enemiesList[i].name);
        }
    }
    void PopulateBosses()
    {
        // Genera los bosses para la peleas
        Enemy boss1 = new Enemy(enemySprites[5], "Jefe1", 10, 0, 0); // Cerbero
        Enemy boss2 = new Enemy(enemySprites[5], "Jefe2", 10, 0, 0); // Jefe 1
        Enemy boss3 = new Enemy(enemySprites[5], "Jefe3", 10, 0, 0); // Jefe 2
        // Lo agregamos a la lista.
        bossList.Add(boss1);
        bossList.Add(boss2);
        bossList.Add(boss3);
    }
}
