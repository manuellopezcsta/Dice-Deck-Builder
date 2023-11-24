using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class AnimacionTransparenciaTexto : MonoBehaviour
{
    public float duracionFade = 1.5f; // Duraci�n de cada desvanecimiento/aparici�n
    public float tiempoEspera = 0.5f; // Tiempo de espera entre desvanecimiento y aparici�n
    public TextMeshProUGUI texto;

    void OnEnable()
    {
        // Obtener el componente TextMeshProUGUI del objeto en el Canvas
        texto = GetComponent<TextMeshProUGUI>();

        // Comenzar la animaci�n de transparencia
        StartCoroutine(AnimacionTransparencia());
    }

    IEnumerator AnimacionTransparencia()
    {
        while (true) // Ciclo infinito para que la animaci�n se repita continuamente
        {
            // Desvanecer el texto
            yield return DesvanecerTexto();

            // Esperar un tiempo antes de volver a mostrar el texto
            yield return new WaitForSeconds(tiempoEspera);

            // Mostrar el texto nuevamente
            yield return MostrarTexto();

            // Esperar un tiempo antes del siguiente desvanecimiento
            yield return new WaitForSeconds(tiempoEspera);
        }
    }

    IEnumerator DesvanecerTexto()
    {
        float tiempoInicio = Time.time;
        Color colorInicial = texto.color;
        Color colorFinal = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 0.0f);

        while (Time.time - tiempoInicio < duracionFade)
        {
            float factor = (Time.time - tiempoInicio) / duracionFade;
            texto.color = Color.Lerp(colorInicial, colorFinal, factor);
            yield return null;
        }

        texto.color = colorFinal; // Asegurar que el texto sea completamente transparente al final
    }

    IEnumerator MostrarTexto()
    {
        float tiempoInicio = Time.time;
        Color colorInicial = texto.color;
        Color colorFinal = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 1.0f);

        while (Time.time - tiempoInicio < duracionFade)
        {
            float factor = (Time.time - tiempoInicio) / duracionFade;
            texto.color = Color.Lerp(colorInicial, colorFinal, factor);
            yield return null;
        }

        texto.color = colorFinal; // Asegurar que el texto sea completamente visible al final
    }
}
