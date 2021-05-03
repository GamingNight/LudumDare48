using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GlobalGameDataSO globalGameData;
    public Animator normalIndicator;
    public Animator deeperIndicator;
    public Animator deeperAndDeeperIndicator;

    private int lastIndex = -1;

    private void Awake() {
        globalGameData.InitLayer();
    }

    void Start() {
        lastIndex = -1;
    }

    void Update() {
        if (globalGameData.GetIncLayerButtonDown()) {
            globalGameData.IncLayer();
        }
        if (globalGameData.GetDecLayerButtonDown()) {
            globalGameData.DecLayer();
        }
        int currentIndex = globalGameData.GetCurrentLayerIndex();
        if (lastIndex != currentIndex) {
            lastIndex = currentIndex;
            switch (lastIndex) {
                case 0:
                    normalIndicator.SetBool("ON", true);
                    deeperIndicator.SetBool("ON", false);
                    deeperAndDeeperIndicator.SetBool("ON", false);
                    break;
                case 1:
                    normalIndicator.SetBool("ON", false);
                    deeperIndicator.SetBool("ON", true);
                    deeperAndDeeperIndicator.SetBool("ON", false);
                    break;
                case 2:
                    normalIndicator.SetBool("ON", false);
                    deeperIndicator.SetBool("ON", false);
                    deeperAndDeeperIndicator.SetBool("ON", true);
                    break;
                default:
                    break;
            }
        }
    }
}
