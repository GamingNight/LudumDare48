using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStraightMove : MonoBehaviour
{
    public LayerDataSO layerData;
    public ProjectileDefaultDataSO defaultData;
    public ProjectileData customData;
    public Vector2 direction = new Vector2(0, 1);
    public GlobalGameDataSO globalGameData;

    private Rigidbody2D rgbd;
    private SpriteRenderer spriteRenderer;
    private Vector2 initVelocity;
    private int previousLayerIndex;
    private bool firstUpdate;
    private ParticleSystem.EmissionModule particleEmission;
    private ParticleSystem.MainModule particleMain;

    private Vector2 hitDirection;
    private float hitTranslationLength;
    private bool hitLock;

    void Start() {

        rgbd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (customData.initForceFactor == 0)
            customData.initForceFactor = defaultData.initForceFactor;
        rgbd.AddForce(direction * customData.initForceFactor);
        previousLayerIndex = -1;
        firstUpdate = true;
        particleEmission = GetComponent<ParticleSystem>().emission;
        particleMain = GetComponent<ParticleSystem>().main;

        hitDirection = Vector2.zero;
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
        if (hitDirection != Vector2.zero) {
            hitLock = true;
            //Pause the regular movement while hit
            rgbd.velocity = Vector2.zero;

            float translationValue = defaultData.hitTranslationSpeedPerLayer[globalGameData.GetCurrentLayerIndex()] * Time.deltaTime;
            rgbd.transform.Translate(hitDirection * translationValue, Space.World);

            hitTranslationLength += translationValue;

            //end of hit movement
            if (hitTranslationLength >= defaultData.hitTranslationLenght) {
                hitDirection = Vector2.zero;
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

    public void HitProjectile(Vector2 direction) {

        if (hitLock)
            return;
        hitDirection = direction;
        hitTranslationLength = 0;
    }

    public void ExplodeProjectile() {
        GetComponent<Animator>().SetBool("Death", true);
    }

    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.tag == "Projectile") {
            if (collision.gameObject.GetComponent<ProjectileStraightMove>().layerData.layerIndex == layerData.layerIndex) {
                collision.gameObject.GetComponent<ProjectileStraightMove>().ExplodeProjectile();
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
