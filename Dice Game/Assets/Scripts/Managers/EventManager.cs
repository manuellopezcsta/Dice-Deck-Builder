using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    [SerializeField] public Sprite[] eventSprites;
    public List<Event> eventList = new List<Event>();

    // Es el numero de elementos en el enum de abajo.
    private int eventNumber = 4;

    // Lista con los eventos.
    public List<Event> allPossibleEvents = new List<Event>(); 
    public enum Eventos
    {
        HEAL,
        TAKE_DMG,
        GAIN_ARMOUR,
        GAIN_MR
    }

    public static EventManager instance = null;
    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        PopulateGame(100);
    }

    void AddEvent(Eventos name)
    {
        string[] aaa = { "texto", "boton1", "boton2" };
        Event evento = new Event(eventSprites[0], "default", eventSprites[0], aaa);

        switch (name)
        {
            case Eventos.HEAL:
                //evento = new Event(eventSprites[0], "heal", 5);
                evento = allPossibleEvents[0];
                break;
            case Eventos.TAKE_DMG:
                //evento = new Event(eventSprites[3], "take dmg", 5);
                evento = allPossibleEvents[1];
                break;
            case Eventos.GAIN_ARMOUR:
                //evento = new Event(eventSprites[2], "gain armour", 5);
                evento = allPossibleEvents[2];
                break;
            case Eventos.GAIN_MR:
                //evento = new Event(eventSprites[1], "gain mr", 5);
                evento = allPossibleEvents[3];
                break;

        }
        eventList.Add(evento);
        //Debug.Log("Se agrego evento " + evento.name);
    }

    void PopulateGame(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            AddEvent((Eventos)Random.Range(0, eventNumber));
            //Debug.Log("Creado Evento " + eventList[i].name);
        }
    }

    public void ResolveEvent(Event name)
    {
        Debug.Log("Resolviendo evento: ." + name.name);
        // Empezamos el evento
        GameManager.instance.StartEvent();
    }
}
