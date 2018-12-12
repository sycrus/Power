/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// Keeps track of the Active status of the Solarpanel object
/// To be attached to Solarpanel object or image target
/// </summary>
public class SolarManager : MonoBehaviour {
    
    public GameObject solarPanel;
    Material solarColor;
    public bool hasElectricity = false;
    public Material solarOn;
    public Material solarOff;

    private void Update()
    {
        StartCoroutine(ColorPulse());
    }
    private void Start()
    {
        hasElectricity = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "sun")
        {
            hasElectricity = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sun")
        {
            hasElectricity = false;
        }
    }

    /// <summary>
    /// Coroutine that alternates between solarOff color and solarOn color
    /// </summary>
    /// <returns>The pulse.</returns>
    IEnumerator ColorPulse()
    {
        while (hasElectricity)
        {
            solarPanel.GetComponent<Renderer>().material.color = Color.Lerp(solarOff.color, solarOn.color, Mathf.PingPong(Time.time, 1));
            yield return null;
        }

        solarPanel.GetComponent<Renderer>().material.color = solarOff.color;

    }

}
