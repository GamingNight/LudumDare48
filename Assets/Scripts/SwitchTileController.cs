using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTileController : MonoBehaviour
{
    public SwitchTileData tileData;
    public SpriteRenderer[] levelDigitSpriteRenderers;
    public SpriteRenderer textSpriteRenderer;
    public AbstractSwitchTileActionSO tileActionSO;
    public GlobalGameDataSO globalGameData;
    public LevelManager levelManager; //TODO Transformer ça en ScriptableObject (attention gros chantier)

    private SpriteRenderer spriteRenderer;
    private SwitchTileData previousData;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        previousData = tileData.Copy();
        Init();
    }

    private void Init() {
        for (int i = 0; i < tileData.levelDigitSprites.Length; i++) {
            levelDigitSpriteRenderers[i].sprite = null;
        }
        switch (tileData.actionType) {
            case AbstractSwitchTileActionSO.ActionType.QUIT:
                tileActionSO = ScriptableObject.CreateInstance<QuitActionSO>();
                break;
            case AbstractSwitchTileActionSO.ActionType.LEVEL_SELECTOR:
                tileActionSO = ScriptableObject.CreateInstance<GoToLevelActionSO>();
                ((GoToLevelActionSO)tileActionSO).LevelManager = levelManager;
                ((GoToLevelActionSO)tileActionSO).LevelIndex = tileData.targetLevelIndex;
                break;
            default:
                break;
        }
    }

    private void Update() {

        CheckForLevelEditing();
        transform.position = tileData.position;
        for (int i = 0; i < tileData.levelDigitSprites.Length; i++) {
            levelDigitSpriteRenderers[i].sprite = tileData.levelDigitSprites[i];
        }
        textSpriteRenderer.sprite = tileData.textSprite;
        spriteRenderer.color = tileData.color;
    }

    private void CheckForLevelEditing() {
        //If the tile has been updated in the level scriptable object, reinit the spawner
        if (!tileData.Equals(previousData)) {
            previousData = tileData.Copy();
            Init();
        } else { //if the tile has been updated in the scene, update the level scriptable object
            if ((Vector2)transform.position != tileData.position) {
                tileData.position = transform.position;
                previousData = tileData.Copy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player" && globalGameData.GetCurrentLayerIndex() == 0) {
            tileActionSO.Trigger();
        }
    }
}
