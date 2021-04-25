using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public LayerData layer;

    private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {

            LayerData projectileLayer = collision.gameObject.GetComponent<ProjectileMove>().layerData;
            if (projectileLayer.layerIndex == layer.layerIndex) {

                Transform t = transform;
                int count = 0;
                while (levelManager == null) {
                    t = t.parent;
                    levelManager = t.GetComponent<LevelManager>();
                    count = count + 1;
                    if (count > 20) {
                        Debug.Log("infinit loop (deeper and deeper)");
                        return;
                    }

                }
                GetComponent<Animator>().SetBool("Dead", true);

            }
        }

    }

    private void ResetLevel() {
        levelManager.resetLevel();
        GetComponent<Animator>().SetBool("Dead", false);
    }
}
