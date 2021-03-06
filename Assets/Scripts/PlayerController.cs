using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed = 10f;
    public GlobalGameDataSO globalGameData;
    public DummyController dummyController0;
    public DummyController dummyController1;
    public GameObject playerSprite;
    public GameObject shield;
    public GameObject shieldCircle;
    public AudioSource moveAudioSource;
    public PositionData initPositionData;

    private int previousLayerIndex;
    private SpriteRenderer spriteRenderer;
    ParticleSystem.MainModule particleMainModule;

    private PositionData previousPositionData;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
        particleMainModule = GetComponent<ParticleSystem>().main;
        previousLayerIndex = -1;

        transform.position = initPositionData.position;
        previousPositionData = initPositionData.Copy();
        ResetPosition();
    }

    // Update is called once per frame
    void Update() {

        CheckForLevelEditing();
        int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
        if (currentLayerIndex != previousLayerIndex) {
            //Update this player layer
            GetComponent<PlayerDeath>().layer = globalGameData.allLayers[currentLayerIndex];
            shield.SetActive(currentLayerIndex != 0);
            shieldCircle.SetActive(currentLayerIndex != 0);

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
        float horizontal = globalGameData.GetPlayerHorizontalMove();
        float vertical = globalGameData.GetPlayerVerticalMove();
        if (horizontal > 0) {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        } else if (horizontal < 0) {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
        if (vertical > 0) {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * Speed);
        } else if (vertical < 0) {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Speed);
        }

        if (horizontal == 0 && vertical == 0) {
            moveAudioSource.Stop();
        } else {
            if (!moveAudioSource.isPlaying)
                moveAudioSource.Play();
        }
    }

    private void SetPlayerColor(Color c) {
        spriteRenderer.color = c;
        particleMainModule.startColor = c;
        shield.GetComponent<SpriteRenderer>().color = c;
        shieldCircle.GetComponent<SpriteRenderer>().color = c;
    }
    public int GetPreviousLayerIndex() {
        return previousLayerIndex;
    }

    public void ResetPosition() {
        transform.position = initPositionData.position;
        dummyController0.Depop();
        dummyController1.Depop();
        dummyController0.transform.position = initPositionData.position;
        dummyController1.transform.position = initPositionData.position;
    }
    private void CheckForLevelEditing() {
        if (previousPositionData.position != initPositionData.position) {
            ResetPosition();
            previousPositionData = initPositionData.Copy();
        }
    }
}
