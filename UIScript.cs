using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;
    public GameObject InteractPopUp;
    public GameObject PandaDialogue;
    public GameObject MilaAnswersPandaCorrect;
    public GameObject MilaAnswersPandaWrong;
    public GameObject PandaRespondsToCorrect;
    public GameObject PandaRespondsToWrong;
    public GameObject DollDialogueStart;
    public GameObject DollDialogueRiddle;
    public GameObject MilaAnswersDollCorrect;
    public GameObject MilaAnswersDollWrong;
    public GameObject DollRespondsToCorrect;
    public GameObject DollRespondsToWrong;
    public GameObject ControlPanelStart;
    public GameObject RedButton;
    public GameObject YellowButton;
    public GameObject BlueButton;

    void Start()
    {
        instance = this;
    }

    public void ShowInteractTip(bool status)
    {
        InteractPopUp.gameObject.SetActive(status);
    }

//PANDA----------------------------------------------------------
    public void ShowPandaInteraction(bool status)
    {
        PandaDialogue.gameObject.SetActive(status);
    }
    public void AnswerPandaCorrect(bool status)
    {
        MilaAnswersPandaCorrect.gameObject.SetActive(status);
    }
    public void AnswerPandaWrong(bool status)
    {
        MilaAnswersPandaWrong.gameObject.SetActive(status);
    }
    public void PandaReactCorrect(bool status)
    {
        PandaRespondsToCorrect.gameObject.SetActive(status);
    }
    public void PandaReactWrong(bool status)
    {
        PandaRespondsToWrong.gameObject.SetActive(status);
    }

//DOLL----------------------------------------------------------
    public void ShowDollInteraction(bool status)
    {
        DollDialogueStart.gameObject.SetActive(status);
    }
    public void ShowDollRiddle(bool status)
    {
        DollDialogueRiddle.gameObject.SetActive(status);
    }
    public void AnswerDollCorrect(bool status)
    {
        MilaAnswersDollCorrect.gameObject.SetActive(status);
    }
    public void AnswerDollWrong(bool status)
    {
        MilaAnswersDollWrong.gameObject.SetActive(status);
    }
    public void DollReactCorrect(bool status)
    {
        DollRespondsToCorrect.gameObject.SetActive(status);
    }
    public void DollReactWrong(bool status)
    {
        DollRespondsToWrong.gameObject.SetActive(status);
    }

//CONTROL PANEL-----------------------------------------------
    public void ShowPanelInteraction(bool status)
    {
        ControlPanelStart.gameObject.SetActive(status);
    }
    public void ShowRedButtonReaction(bool status)
    {
        RedButton.gameObject.SetActive(status);
    }
    public void ShowYellowButtonReaction(bool status)
    {
        YellowButton.gameObject.SetActive(status);
    }
    public void ShowBlueButtonReaction(bool status)
    {
        BlueButton.gameObject.SetActive(status);
    }
}
