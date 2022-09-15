using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Animator dollAnimator;
    private CameraController cameraController;
    private MilaController milaController;
    private IEnumerator riddle;
    private IEnumerator reaction;
    private IEnumerator stopInteraction;
    private IEnumerator startTeleport;

    [Header("Animator Bools")]

    [Header("Interacting")]
    private bool dollActive;
    private bool isInteracting;
    private bool hasInteracted;
    private bool answerChosen;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        dollAnimator = GetComponent<Animator>();
        //Mila
        milaController = GameObject.Find("Mila").GetComponent<MilaController>();

        //Camera
        cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();

        //Interaction
        dollActive = false;
        hasInteracted = false;
        answerChosen = false; 
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
        
        if(dollActive && isInteracting)
        {
            Interaction();
        }

        //Choose solution
        if(Input.GetKey(KeyCode.Alpha1) && dollActive)
        {
            DollRiddle(1);
        }
        else if(Input.GetKey(KeyCode.Alpha2) && dollActive)
        {
            DollRiddle(2);
        }
    }

    //Interaction
    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && !isInteracting)
        {
            UIScript.instance.ShowInteractTip(true);
            dollActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
            UIScript.instance.ShowInteractTip(false); 
    }

    private void Interaction()
    {
        UIScript.instance.ShowDollInteraction(true);
        hasInteracted = true;
        isInteracting = true;
        CameraController.instance.CameraSwitch();
        dollAnimator.SetBool("talking", true);

        riddle = TellRiddle(5.0f);
        StartCoroutine(riddle);
    }

    private IEnumerator TellRiddle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UIScript.instance.ShowDollInteraction(false);
        UIScript.instance.ShowDollRiddle(true);
    }

    private void DollRiddle(int answer)
    {
        UIScript.instance.ShowDollRiddle(false);
        dollActive = false;
        //Two choices - One variable
        if(answer == 1 && !answerChosen)
        {
            answerChosen = true;
            dollAnimator.SetTrigger("rightAnswer");        
            dollAnimator.SetBool("talking", false);
            UIScript.instance.AnswerDollCorrect(true);
            reaction = DollReaction(5.0f, 1);
            StartCoroutine(reaction);

            
        }
        else if(answer == 2 && !answerChosen)
        {
            answerChosen = true;
            dollAnimator.SetTrigger("wrongAnswer");
            dollAnimator.SetBool("talking", false);
            UIScript.instance.AnswerDollWrong(true);
            reaction = DollReaction(5.0f, 2);
            StartCoroutine(reaction);
        }
    }

    private IEnumerator DollReaction(float waitTime, int answer)
    {
        yield return new WaitForSeconds(waitTime);
        if(answer == 1)
        {
            UIScript.instance.AnswerDollCorrect(false);
            UIScript.instance.DollReactCorrect(true);

            startTeleport = StartTeleport(5.0f);
            StartCoroutine(startTeleport);

            stopInteraction = StopInteraction(5.0f);
            StartCoroutine(stopInteraction);
        }
        else if(answer == 2)
        {
            UIScript.instance.AnswerDollWrong(false);
            UIScript.instance.DollReactWrong(true);
            HealthManager.instance.TakeDamage(4.0f);

            stopInteraction = StopInteraction(5.0f);
            StartCoroutine(stopInteraction);
        }
        
    }
    private IEnumerator StopInteraction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CameraController.instance.CameraSwitch();
        UIScript.instance.ShowDollInteraction(false);
        UIScript.instance.ShowDollRiddle(false);
        UIScript.instance.DollReactCorrect(false);
        UIScript.instance.DollReactWrong(false);
        isInteracting = false;
        hasInteracted = false;
        answerChosen = false;
    }

    private IEnumerator StartTeleport(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        MilaController.instance.TelePort();

    }
}
