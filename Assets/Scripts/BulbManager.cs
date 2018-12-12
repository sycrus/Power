/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using UnityEngine;

/// <summary>
/// Controls the activation of the Lightbulb object
/// To be attached to the Lightbulb object or image target
/// </summary>
public class BulbManager : MonoBehaviour {

    public GameObject bulb;
    public Light bulbLight;
    public Material lightsOff;
    public Material lightsOn;

    LineRenderer lineRenderer;
    Renderer bulbColor;

    float duration = 1.0F;

    void Start()
    {
        //get all renderers
        bulbColor = bulb.GetComponent<Renderer>();

        //set default material to LightsOff
        bulbColor.material = lightsOff;
     
        bulbLight.gameObject.SetActive(false);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "turbine")
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lightsOff;
        }
        if (other.gameObject.tag == "solarpanel")
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lightsOff;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "turbine")
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.widthMultiplier = 0.07f;

            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, other.transform.position);

            if (FindObjectOfType<TurbineSpinManager>().hasElectricity)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
        if (other.gameObject.tag == "solarpanel")
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.widthMultiplier = 0.07f;

            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, other.transform.position);

            if (FindObjectOfType<SolarManager>().hasElectricity)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "turbine")//|| other.gameObject.tag == "solarpanel")
        {
            Destroy(lineRenderer);
            TurnOff();
        }
    }

    /// <summary>
    // Activates the color and the light source
    /// </summary>
    private void TurnOn()
    {
        lineRenderer.material.Lerp(lightsOff, lightsOn, duration);

        //light up bulb
        bulbColor.material.Lerp(lightsOff, lightsOn, duration);
     
        //turn on light
        bulbLight.gameObject.SetActive(true);

    }
    /// <summary>
    // Deactivates the color and the light source
    /// </summary>
    private void TurnOff()
    {
        lineRenderer.material.Lerp(lightsOn, lightsOff, duration);

        //turn off windows
        bulbColor.material.Lerp(lightsOn, lightsOff, duration);
      
        //stop smoke
        bulbLight.gameObject.SetActive(false);

    }

}
