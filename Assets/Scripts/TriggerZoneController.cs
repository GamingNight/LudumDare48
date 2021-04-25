using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneController : MonoBehaviour
{

    private List<ProjectileMove> projectileList;
    private ProjectileMove.Direction lastDir = ProjectileMove.Direction.RIGHT;

    private float previousRotation;

    // Start is called before the first frame update
    void Start() {
        projectileList = new List<ProjectileMove>();
        previousRotation = 0;
    }

    // Update is called once per frame
    void Update() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal > 0) {
            if (vertical > 0) {
                transform.Rotate(0f, 0f, -previousRotation + 45, Space.Self);
                previousRotation = 45;
            } else if (vertical < 0) {
                transform.Rotate(0f, 0f, -previousRotation - 45, Space.Self);
                previousRotation = -45;
            } else {
                transform.Rotate(0f, 0f, -previousRotation, Space.Self);
                previousRotation = 0;
            }
        } else if (horizontal < 0) {
            if (vertical > 0) {
                transform.Rotate(0f, 0f, -previousRotation + 135, Space.Self);
                previousRotation = 135;
            } else if (vertical < 0) {
                transform.Rotate(0f, 0f, -previousRotation - 135, Space.Self);
                previousRotation = -135;
            } else {
                transform.Rotate(0f, 0f, -previousRotation + 180, Space.Self);
                previousRotation = 180;
            }
        } else {
            if (vertical > 0) {
                transform.Rotate(0f, 0f, -previousRotation + 90, Space.Self);
                previousRotation = 90;
            } else if (vertical < 0) {
                transform.Rotate(0f, 0f, -previousRotation - 90, Space.Self);
                previousRotation = -90;
            }
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
        foreach (ProjectileMove projectile in projectileList) {
            projectile.HitProjectile(lastDir);
        }
        //}
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            projectileList.Add(collision.gameObject.GetComponent<ProjectileMove>());
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            projectileList.Remove(collision.gameObject.GetComponent<ProjectileMove>());
        }
    }
}
