using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public LayerDataSO layer;
    public GlobalGameDataSO globalGameData;
    public GameObject shield;
    public GameObject shieldCircle;
    public LevelManager levelManager;


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {

            ProjectileMove projectileMove = collision.gameObject.GetComponent<ProjectileMove>();

            LayerDataSO projectileLayer = projectileMove.layerData;
            if (projectileLayer.layerIndex == layer.layerIndex) {
                globalGameData.LockInputs();
                GetComponent<Animator>().SetBool("Dead", true);
                if (shield != null) {
                    shield.SetActive(false);
                    shieldCircle.SetActive(false);
                }
            }
        }

    }

    private void ResetLevel() {
        levelManager.ResetCurrentLevel();
        globalGameData.UnlockInputs();
        GetComponent<Animator>().SetBool("Dead", false);
    }
}
