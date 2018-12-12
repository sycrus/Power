/* 
 * Created by Joe Chung
 * Copyright 2018 
 * joechung.me
 */

using UnityEngine;

/// <summary>
/// Activates the Turbine object, activating the spin of the Blades.
/// To be attached to the Turbine object or the image target
/// </summary>
public class TurbineSpinManager : MonoBehaviour {

    public GameObject blades;
    public bool hasElectricity = false;

    private void Start()
    {
        hasElectricity = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "wind") 
        {
            hasElectricity = true;
            blades.transform.Rotate(Vector3.forward*Time.deltaTime*1000);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wind")
        {
            hasElectricity = false;
            Debug.Log("hasElectricity = false");
        }
    }
}
