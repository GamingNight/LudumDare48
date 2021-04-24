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

    private SpriteRenderer spriteRenderer;
    private int previousLayerIndex;
    private Vector3 dummyController0_pos = new Vector3(-1, -1, -1);
    private Vector3 dummyController1_pos = new Vector3(-1, -1, -1);

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                    dummyController0_pos = transform.position;
                    dummyController0.pop(dummyController0_pos);
                } else if (currentLayerIndex == 2) {
                    dummyController1_pos = transform.position;
                    dummyController1.pop(dummyController1_pos);
                }


                // create new dummy player
                // save pos for the respown

            } else {
                // delete child dummy player
                // translate saved pos
                if (currentLayerIndex == 0) {
                    transform.position = dummyController0_pos;
                    dummyController0.depop();
                } else if (currentLayerIndex == 1) {
                    transform.position = dummyController1_pos;
                    dummyController1.depop();
                }
            }
            previousLayerIndex = currentLayerIndex;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * Speed);
            spriteRenderer.flipY = true;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Speed);
            spriteRenderer.flipY = false;
        }
    }

}
