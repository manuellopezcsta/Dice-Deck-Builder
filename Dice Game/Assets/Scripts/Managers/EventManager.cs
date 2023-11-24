using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    public Sprite eventMini;
    public Sprite eventDefaultPic;
    public List<Event> eventList = new List<Event>();   // La lista donde quedan los eventos generados.
    public List<Event> storyEventList = new List<Event>(); // Lista solo para los eventos de historia que no se repiten.
    // PONER ICONO DE INTERROGACION A LOS DE HISTORIA.
    

    // Es el numero de elementos en el enum de abajo.
    private int eventNumber = 14;

    // Lista con los eventos.
    public List<Event> allPossibleEvents = new List<Event>(); 
    public enum Eventos
    { // Solo los de trampas y buff , los de historia son hechos a mano en la otra list.
        BUFF1,
        BUFF2,
        BUFF3,
        BUFF4,
        BUFF5,
        BUFF6,
        BUFF7,
        TRAP1,
        TRAP2,
        TRAP3,
        TRAP4,
        TRAP5,
        TRAP6,
        TRAP7,
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
        Event evento = new Event(eventMini, "default", eventDefaultPic, aaa);

        switch (name)
        {
            case Eventos.BUFF1:
                evento = allPossibleEvents[0];
                break;
            case Eventos.BUFF2:
                evento = allPossibleEvents[1];
                break;
            case Eventos.BUFF3:
                evento = allPossibleEvents[2];
                break;
            case Eventos.BUFF4:
                evento = allPossibleEvents[3];
                break;
            case Eventos.BUFF5:
                evento = allPossibleEvents[4];
                break;
            case Eventos.BUFF6:
                evento = allPossibleEvents[5];
                break;
            case Eventos.BUFF7:
                evento = allPossibleEvents[6];
                break;
            case Eventos.TRAP1:
                evento = allPossibleEvents[7];
                break;
            case Eventos.TRAP2:
                evento = allPossibleEvents[8];
                break;
            case Eventos.TRAP3:
                evento = allPossibleEvents[9];
                break;
            case Eventos.TRAP4:
                evento = allPossibleEvents[10];
                break;
            case Eventos.TRAP5:
                evento = allPossibleEvents[11];
                break;
            case Eventos.TRAP6:
                evento = allPossibleEvents[12];
                break;
            case Eventos.TRAP7:
                evento = allPossibleEvents[13];
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
