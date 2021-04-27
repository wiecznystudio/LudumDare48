using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StageController : MonoBehaviour {

    #region Singleton

    private static StageController instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int stageDataId;
    [SerializeField]
    private StageData[] stages;

    [SerializeField]
    private RectTransform[] ui = new RectTransform[2];
    [SerializeField]
    private Transform kartka;

    private bool switchedToPlayerInput = false;
    private bool endGame = false;

    private void Start() {
        Change(stageDataId, stages[stageDataId].restartPoint);
        UiSetActive(false, 1);
        UiSetActive(true, 0);
    }

    public static void Change(int id, Transform destination) {
        instance.stages[instance.stageDataId].gameObject.SetActive(false);
        instance.stageDataId = id;
        instance.stages[instance.stageDataId].gameObject.SetActive(true);
        instance.player.Restart();
        instance.player.transform.position = destination.position;
        instance.cameraController.minVal = instance.GetStageData().cameraClamp[0];
        instance.cameraController.maxVal = instance.GetStageData().cameraClamp[1];
        instance.cameraController.ClampPos();
    }

    public static void Restart() {
        StageData data = instance.GetStageData();
        instance.player.transform.position = data.restartPoint.transform.position;
        instance.player.Restart();
        AudioManager.instance.PlaySfx("Death");

        for(int i = 0; i < data.flowers.Length; i++) {
            data.flowers[i].OnRestart();
        }
        for(int i = 0; i < data.actions.Length; i++) {
            data.actions[i].OnRestart();
        }

    }

    public static void EndGame() {
        instance.EndGameUI();
    }
    private void EndGameUI() {
        endGame = true;
        switchedToPlayerInput = false;
        UiSetActive(true, 1);
        input.SwitchCurrentActionMap("UI");
    }

    private StageData GetStageData() {
        return stages[stageDataId];
    }

    public void OnExitGame(InputAction.CallbackContext context) {
        Application.Quit();
    }
    public void OnAnyKey(InputAction.CallbackContext context) {
        if(switchedToPlayerInput == true)
            return;

        if(endGame) {
            Application.Quit();
            return;
        }

        switchedToPlayerInput = true;
        UiSetActive(false, 0);
        input.SwitchCurrentActionMap("Player");
    }

    private void UiSetActive(bool active, int id) {
        kartka.gameObject.SetActive(active);
        ui[id].gameObject.SetActive(active);
    }



}
