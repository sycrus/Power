/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using UnityEngine;

/// <summary>
/// Activates the Factory object, activating the lights on windows or smoke in chimneys.
/// To be attached to the Turbine object or the image target
/// </summary>
public class FactoryController : MonoBehaviour
{
    GameObject window1;
    GameObject window2;
    GameObject window3;
    Renderer windowColor1;
    Renderer windowColor2;
    Renderer windowColor3;

    LineRenderer lineRenderer;

    public Material lightsOff;
    public Material lightsOn;
    public ParticleSystem s1;
    public ParticleSystem s2;

    float duration = 100.0F;

    void Start()
    {
        //get all windows
        window1 = this.transform.Find("Window1").gameObject;
        window2 = this.transform.Find("Window2").gameObject;
        window3 = this.transform.Find("Window3").gameObject;

        //get all renderers
        windowColor1 = window1.GetComponent<Renderer>();
        windowColor2 = window2.GetComponent<Renderer>();
        windowColor3 = window3.GetComponent<Renderer>();

        //set default material to LightsOff
        windowColor1.material = lightsOff;
        windowColor2.material = lightsOff;
        windowColor3.material = lightsOff;

        s1.gameObject.SetActive(false);
        s2.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "turbine")
        {
            //Debug.Log("Turbine entered Factory");

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
        if (other.gameObject.tag == "turbine" || other.gameObject.tag == "solarpanel")
        {
            Destroy(lineRenderer);
            TurnOff();
        }
    }

    /// <summary>
    /// Turns on the windows and activates smokestacks
    /// </summary>
    private void TurnOn()
    {
        lineRenderer.material.Lerp(lightsOff, lightsOn, duration);

        //light up windows
        windowColor1.material.Lerp(lightsOff, lightsOn, duration);
        windowColor2.material.Lerp(lightsOff, lightsOn, duration);
        windowColor3.material.Lerp(lightsOff, lightsOn, duration);

        s1.gameObject.SetActive(true);
        s2.gameObject.SetActive(true);
    }

    /// <summary>
    /// Turns off the windows and activates smokestacks
    /// </summary>
    private void TurnOff() {
        lineRenderer.material.Lerp(lightsOn, lightsOff, duration);

        //turn off windows
        windowColor1.material.Lerp(lightsOn, lightsOff, duration);
        windowColor2.material.Lerp(lightsOn, lightsOff, duration);
        windowColor3.material.Lerp(lightsOn, lightsOff, duration);

        //stop smoke
        s1.gameObject.SetActive(false);
        s2.gameObject.SetActive(false);
    }


}
