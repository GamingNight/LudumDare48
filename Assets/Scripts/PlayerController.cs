using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed = 10f;
    public LayerData layerData;
    public GlobalGameData globalGameData;
    public DummyController dummyController0;
    public DummyController dummyController1;
    public GameObject playerSprite;

    private int previousLayerIndex;
    private SpriteRenderer spriteRenderer;
    ParticleSystem.MainModule particleMainModule;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
        particleMainModule = GetComponent<ParticleSystem>().main;
        previousLayerIndex = -1;
    }

    // Update is called once per frame
    void Update() {
        int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
        if (currentLayerIndex != previousLayerIndex) {
            //Update this player layer
            GetComponent<PlayerDeath>().layer = globalGameData.allLayers[currentLayerIndex];

            if (previousLayerIndex < currentLayerIndex) {
                if (currentLayerIndex == 1) {
                    SetPlayerColor(new Color(0, 1, 1));
                    dummyController0.Pop(transform.position);
                } else if (currentLayerIndex == 2) {
                    SetPlayerColor(new Color(1, 1, 0));
                    dummyController1.Pop(transform.position);
                }

            } else {

                if (currentLayerIndex == 0) {
                    SetPlayerColor(new Color(1, 1, 1));
                    transform.position = dummyController0.transform.position;
                    dummyController0.Depop();
                } else if (currentLayerIndex == 1) {
                    SetPlayerColor(new Color(0, 1, 1));
                    transform.position = dummyController1.transform.position;
                    dummyController1.Depop();
                }
            }
            previousLayerIndex = currentLayerIndex;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Speed);
        }
    }

    private void SetPlayerColor(Color c) {
        spriteRenderer.color = c;
        particleMainModule.startColor = c;
    }
    public int GetPreviousLayerIndex() {
        return previousLayerIndex;
    }

}
