using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public LayerData layer;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Projectile") {

            LayerData projectileLayer = collision.gameObject.GetComponent<ProjectileMove>().layerData;
            if(projectileLayer.layerIndex == layer.layerIndex) {
            }
        }
    }
}
