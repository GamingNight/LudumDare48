using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour
{
    public int sceneIndexTarget = -1;
    public SpriteRenderer[] levelDigitSpriteRenderers;
    public SpriteRenderer textSpriteRenderer;
    private LevelManagerOld levelManager;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            int index = player.GetPreviousLayerIndex();

            if (index == 0) {
                Transform t = transform;
                int count = 0;
                while (levelManager == null) {
                    t = t.parent;
                    levelManager = t.GetComponent<LevelManagerOld>();
                    count = count + 1;
                    if (count > 20) {
                        Debug.Log("infinit loop (deeper and deeper)");
                        return;
                    }

                }
                levelManager.ActiveNextLevel(sceneIndexTarget);

            }

        }
    }

    public void SetLevelDigitSprite(int index, Sprite sprite) {
        levelDigitSpriteRenderers[index].sprite = sprite;
    }

    public void SetTextSprite(Sprite sprite) {
        textSpriteRenderer.sprite = sprite;
    }
}
