using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    public LayerData layerData;
    public ProjectileDefaultData defaultData;
    public Direction direction = Direction.UP;
    public GlobalGameData globalGameData;
    private Rigidbody2D rgbd;
    private SpriteRenderer spriteRenderer;
    private Vector2 initVelocity;
    private int previousLayerIndex;
    private bool firstUpdate;

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
        rgbd.AddForce(initForce * defaultData.initSpeed);
        previousLayerIndex = -1;
        firstUpdate = true;
    }

    void Update() {

        if (firstUpdate) {
            initVelocity = rgbd.velocity;
            firstUpdate = false;
        }

        if (globalGameData.GetCurrentLayerIndex() != previousLayerIndex) {
            int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
            int projectileLayerIndex = layerData.layerIndex;
            float speedFactor = globalGameData.allLayers[currentLayerIndex].layerSpeedFactors[projectileLayerIndex];
            if (speedFactor != -1) {
                spriteRenderer.enabled = true;
                rgbd.velocity = initVelocity * speedFactor;
            } else {
                spriteRenderer.enabled = false;
                rgbd.velocity = initVelocity * 0;
            }
            previousLayerIndex = currentLayerIndex;
        }
    }
}
