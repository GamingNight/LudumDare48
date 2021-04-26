using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public LayerData layer;
    public GlobalGameData globalGameData;

    private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {

        	ProjectileMove projectileMove = collision.gameObject.GetComponent<ProjectileMove>();
        	int ProjectileKillerlayerIndex = projectileMove.GetKillerLayer();

        	// Old style
/*            LayerData projectileLayer = ProjectileMove.layerData;
            if (projectileLayer.layerIndex == layer.layerIndex) {*/

            if (ProjectileKillerlayerIndex >= layer.layerIndex) {


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
                globalGameData.LockInputs();
                GetComponent<Animator>().SetBool("Dead", true);

            }
        }

    }

    private void ResetLevel() {
        levelManager.resetLevel();
        //globalGameData.UnlockInputs();
        GetComponent<Animator>().SetBool("Dead", false);
    }
}
