using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneController : MonoBehaviour
{
    public GlobalGameDataSO globalGameData;

    private List<ProjectileMove> projectileList;

    private float previousRotation;

    // Start is called before the first frame update
    void Start() {
        projectileList = new List<ProjectileMove>();
        previousRotation = 0;
    }

    // Update is called once per frame
    void Update() {

        float horizontal = globalGameData.GetPlayerHorizontalMove();
        float vertical = globalGameData.GetPlayerVerticalMove();
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
        List<ProjectileMove> _projectileList = new List<ProjectileMove>(projectileList);
        foreach (ProjectileMove projectile in _projectileList) {
            if (projectile == null) // happen when the projectile has been destroyed by a collision with another projectile while still in the trigger zone
                projectileList.Remove(projectile);
            else {
                ProjectileMove.Direction projectileDirection = ProjectileMove.Direction.NONE;
                Vector3 projectilePos = projectile.transform.position;
                float distX = projectilePos.x - transform.position.x;
                float distY = projectilePos.y - transform.position.y;
                if (distX > 0 && Mathf.Abs(distX) >= Mathf.Abs(distY)) {
                    projectileDirection = ProjectileMove.Direction.RIGHT;
                } else if (distX < 0 && Mathf.Abs(distX) >= Mathf.Abs(distY)) {
                    projectileDirection = ProjectileMove.Direction.LEFT;
                } else if (distY > 0 && Mathf.Abs(distY) >= Mathf.Abs(distX)) {
                    projectileDirection = ProjectileMove.Direction.UP;
                } else if (distY < 0 && Mathf.Abs(distY) >= Mathf.Abs(distX)) {
                    projectileDirection = ProjectileMove.Direction.DOWN;
                }
                projectile.HitProjectile(projectileDirection);
            }
        }
        //}
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {
            ProjectileMove projectileMove = collision.gameObject.GetComponent<ProjectileMove>();
            int layerIndex = globalGameData.GetCurrentLayerIndex();
            if (layerIndex == projectileMove.layerData.layerIndex + 1)
                projectileList.Add(projectileMove);

        }
    }

    void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {
            ProjectileMove projectileMove = collision.gameObject.GetComponent<ProjectileMove>();
            int layerIndex = globalGameData.GetCurrentLayerIndex();
            if (layerIndex == projectileMove.layerData.layerIndex + 1)
                projectileList.Remove(projectileMove);
        }
    }
}
