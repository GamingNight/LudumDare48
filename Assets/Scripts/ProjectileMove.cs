using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

    public LayerDataSO layerData;
    public ProjectileDefaultDataSO defaultData;
    public ProjectileData customData;
    public Direction direction = Direction.UP;
    public GlobalGameDataSO globalGameData;
    private Rigidbody2D rgbd;
    private SpriteRenderer spriteRenderer;
    private Vector2 initVelocity;
    private int previousLayerIndex;
    private bool firstUpdate;
    private ParticleSystem.EmissionModule particleEmission;
    private ParticleSystem.MainModule particleMain;

    private Direction hitDirection;
    private float hitTranslationLength;
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
        if (customData.initForceFactor == 0)
            customData.initForceFactor = defaultData.initForceFactor;
        rgbd.AddForce(initForce * customData.initForceFactor);
        previousLayerIndex = -1;
        firstUpdate = true;
        particleEmission = GetComponent<ParticleSystem>().emission;
        particleMain = GetComponent<ParticleSystem>().main;

        hitDirection = Direction.NONE;
        hitTranslationLength = 0;
        hitLock = false;

        ComputeVelocity();
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
            ComputeVelocity();

            previousLayerIndex = globalGameData.GetCurrentLayerIndex();
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

            float translationValue = defaultData.hitTranslationSpeedPerLayer[globalGameData.GetCurrentLayerIndex()] * Time.deltaTime;
            rgbd.transform.Translate(directionVector * translationValue, Space.World);

            hitTranslationLength += translationValue;

            //end of hit movement
            if (hitTranslationLength >= defaultData.hitTranslationLenght) {
                hitDirection = Direction.NONE;
                ComputeVelocity();
                hitLock = false;
            }
        }
    }

    private void ComputeVelocity() {
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
    }

    public void HitProjectile(Direction direction) {

        if (hitLock)
            return;
        hitDirection = direction;
        hitTranslationLength = 0;
    }

    public void ExplodeProjectile() {
        GetComponent<Animator>().SetBool("Death", true);
    }

    public Direction GetHitDirection() {

        return hitDirection;
    }

    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {
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
            Destroy(gameObject, 2f);
        }
    }
}
