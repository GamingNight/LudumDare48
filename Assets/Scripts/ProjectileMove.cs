using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    public LayerData layerData;
    public ProjectileDefaultData projectileData;
    public Direction direction = Direction.UP;
    public GlobalGameData globalGameData;
    private Rigidbody2D rgbd;
    private SpriteRenderer spriteRenderer;
    private Vector2 initVelocity;
    private int previousLayerIndex;
    private bool firstUpdate;
    private ParticleSystem.EmissionModule particleEmission;
    private ParticleSystem.MainModule particleMain;

    private Direction hitDirection;
    private float hitTranslationLength;
    private Vector2 velocityBeforeHit;
    private bool hitLock;

    // Start is called before the first frame update
    void Start() {

        rgbd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 initForce;
        switch (direction) {
            case Direction.UP:
                initForce = new Vector2(0, 1);
                break;
            case Direction.DOWN:
                initForce = new Vector2(0, -1);
                break;
            case Direction.LEFT:
                initForce = new Vector2(-1, 0);
                break;
            case Direction.RIGHT:
                initForce = new Vector2(1, 0);
                break;
            default:
                initForce = new Vector2(0, 0);
                break;
        }
        rgbd.AddForce(initForce * projectileData.initForceFactor);
        previousLayerIndex = -1;
        firstUpdate = true;
        particleEmission = GetComponent<ParticleSystem>().emission;
        particleMain = GetComponent<ParticleSystem>().main;

        hitDirection = Direction.NONE;
        hitTranslationLength = 0;
        hitLock = false;
    }

    void FixedUpdate() {

        if (firstUpdate) {
            initVelocity = rgbd.velocity;
            if (initVelocity == Vector2.zero)
                return;
            firstUpdate = false;
        }

        //Regular move updated when current layer has changed
        if (globalGameData.GetCurrentLayerIndex() != previousLayerIndex) {
            int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
            int projectileLayerIndex = layerData.layerIndex;
            float speedFactor = globalGameData.allLayers[currentLayerIndex].layerSpeedFactors[projectileLayerIndex];
            if (speedFactor != -1) {
                spriteRenderer.enabled = true;
                particleEmission.enabled = true;
                rgbd.velocity = initVelocity * speedFactor;
                //Update also particle speed
                particleMain.simulationSpeed = particleMain.simulationSpeed * speedFactor;
            } else {
                spriteRenderer.enabled = false;
                //rgbd.velocity = initVelocity * 0;
                particleEmission.enabled = false;
            }


            previousLayerIndex = currentLayerIndex;
        }

        //Move when hit
        if (hitDirection != Direction.NONE) {
            hitLock = true;
            //Pause the regular movement while hit
            rgbd.velocity = Vector2.zero;

            Vector2 directionVector = new Vector2(0, 0);
            switch (hitDirection) {
                case Direction.UP:
                    directionVector.y = 1;
                    break;
                case Direction.DOWN:
                    directionVector.y = -1;
                    break;
                case Direction.LEFT:
                    directionVector.x = -1;
                    break;
                case Direction.RIGHT:
                    directionVector.x = 1;
                    break;
                default:
                    break;
            }

            float translationValue = projectileData.hitTranslationSpeedPerLayer[globalGameData.GetCurrentLayerIndex()] * Time.deltaTime;
            rgbd.transform.Translate(directionVector * translationValue, Space.World);

            hitTranslationLength += translationValue;

            //end of hit movement
            if (hitTranslationLength >= projectileData.hitTranslationLenght) {
                hitDirection = Direction.NONE;
                rgbd.velocity = velocityBeforeHit;
                hitLock = false;
            }
        }
    }

    public void HitProjectile(Direction direction) {

        if (hitLock)
            return;
        hitDirection = direction;
        hitTranslationLength = 0;
        velocityBeforeHit = rgbd.velocity;
    }

    public void ExplodeProjectile() {
        GetComponent<Animator>().SetBool("Death", true);
    }

    public Direction GetHitDirection() {

        return hitDirection;
    }

    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {
            //Vector2 oProjectilePosition = collision.gameObject.transform.position;
            //Direction oProjectileHitDirection = collision.gameObject.GetComponent<ProjectileMove>().GetHitDirection();
            //if (oProjectileHitDirection != Direction.NONE) {
            //    Direction relativeDirectionToOther = Direction.NONE;
            //    float distY = transform.position.y - oProjectilePosition.y;
            //    float distX = transform.position.x - oProjectilePosition.x;
            //    if (distY > 0 && Mathf.Abs(distY) >= Mathf.Abs(distX))
            //        relativeDirectionToOther = Direction.UP;
            //    else if (distY < 0 && Mathf.Abs(distY) >= Mathf.Abs(distX))
            //        relativeDirectionToOther = Direction.DOWN;
            //    else if (distX > 0 && Mathf.Abs(distX) >= Mathf.Abs(distY))
            //        relativeDirectionToOther = Direction.RIGHT;
            //    else if (distX < 0 && Mathf.Abs(distX) >= Mathf.Abs(distY))
            //        relativeDirectionToOther = Direction.LEFT;

            //    if (oProjectileHitDirection == relativeDirectionToOther)
            //        HitProjectile(oProjectileHitDirection);
            //}
            if (collision.gameObject.GetComponent<ProjectileMove>().layerData.layerIndex == layerData.layerIndex) {
                collision.gameObject.GetComponent<ProjectileMove>().ExplodeProjectile();
                ExplodeProjectile();
            }
        }
    }

    public void DestroyProjectile() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
