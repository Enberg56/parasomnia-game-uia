using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelScript : MonoBehaviour
{
    private IEnumerator reaction;
    private IEnumerator stopInteraction;
    private CameraController cameraController;
    private TrapdoorScript trapdoorScript;

    [Header("Interacting")]
    private bool panelActive;
    private bool isInteracting;
    private bool hasInteracted;
    private bool buttonChosen;

    void Start()
    {
        //Camera
        cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();

        //Trapdoor
        trapdoorScript = GameObject.Find("Trapdoor").GetComponent<TrapdoorScript>();

        //Interaction
        panelActive = false;
        hasInteracted = false;
        buttonChosen = false; 
    }

    void Update()
    {
        //Interaction
        if(Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
        
        if(panelActive && isInteracting)
        {
            Interaction();
        }

        //Choose solution
        if(Input.GetKey(KeyCode.Alpha1) && panelActive)
        {
            PanelRiddle(1);
        }
        else if(Input.GetKey(KeyCode.Alpha2) && panelActive)
        {
            PanelRiddle(2);
        }
        else if(Input.GetKey(KeyCode.Alpha3) && panelActive)
        {
            PanelRiddle(3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && !isInteracting)
        {
            UIScript.instance.ShowInteractTip(true);
            panelActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
            UIScript.instance.ShowInteractTip(false); 
    }
    private void Interaction()
    {
        UIScript.instance.ShowPanelInteraction(true);
        hasInteracted = true;
        isInteracting = true;
        CameraController.instance.CameraSwitch();
    }

    private void PanelRiddle(int answer)
    {
        UIScript.instance.ShowPanelInteraction(false);
        panelActive = false;
        //Three choices - One variable
        if(answer == 1 && !buttonChosen)
        {
            buttonChosen = true;
            UIScript.instance.ShowRedButtonReaction(true);
            reaction = PanelReaction(2.0f, 1);
            StartCoroutine(reaction);

        }
        else if(answer == 2 && !buttonChosen)
        {
            buttonChosen = true;
            //Run function(instance) in game manager(decrease "life")
            HealthManager.instance.TakeDamage(2.0f);
            UIScript.instance.ShowYellowButtonReaction(true);
            reaction = PanelReaction(2.0f, 2);
            StartCoroutine(reaction);
        }
        else if(answer == 3 && !buttonChosen)
        {
            buttonChosen = true;
            StartCoroutine(TrapdoorScript.instance.OpenDoor());
            UIScript.instance.ShowBlueButtonReaction(true);
            reaction = PanelReaction(2.0f, 3);
            StartCoroutine(reaction);
        }
    }

    private IEnumerator PanelReaction(float waitTime, int answer)
    {
        yield return new WaitForSeconds(waitTime);
        if (answer == 1)
        {
            stopInteraction = StopInteraction(3.0f);
            StartCoroutine(stopInteraction);
        }
        else if (answer == 2)
        {
            stopInteraction = StopInteraction(3.0f);
            StartCoroutine(stopInteraction);
        }
        else if(answer == 3)
        {
            stopInteraction = StopInteraction(3.0f);
            StartCoroutine(stopInteraction);
        }
    }

    private IEnumerator StopInteraction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UIScript.instance.ShowPanelInteraction(false);
        UIScript.instance.ShowRedButtonReaction(false);
        UIScript.instance.ShowYellowButtonReaction(false);
        UIScript.instance.ShowBlueButtonReaction(false);

        CameraController.instance.CameraSwitch();
        isInteracting = false;
        hasInteracted = false;
        buttonChosen = false;
    }
}
