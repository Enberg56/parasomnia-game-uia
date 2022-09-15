using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaController : MonoBehaviour
{

    private Animator PandaAnimator;
    private Rigidbody rigidBody;
    private CameraController cameraController;
    
    private IEnumerator reaction;
    private IEnumerator stopInteraction;

    [Header("Interacting")]
    private bool pandaActive;
    private bool isInteracting;
    private bool hasInteracted;
    private bool answerChosen;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        PandaAnimator = GetComponent<Animator>();
        NormalFreeze();

        //Camera
        cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();

        //Interaction
        pandaActive = false;
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
        
        if(pandaActive && isInteracting)
        {
            Interaction();
        }

        //Choose solution
        if(Input.GetKey(KeyCode.Alpha1) && pandaActive)
        {
            PickAHand(1);
        }
        else if(Input.GetKey(KeyCode.Alpha2) && pandaActive)
        {
            PickAHand(2);
        }

    }

    void NormalFreeze()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    //Interaction
    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && !isInteracting)
        {
            UIScript.instance.ShowInteractTip(true);
            pandaActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
            UIScript.instance.ShowInteractTip(false); 
    }

    private void Interaction()
    {
        UIScript.instance.ShowPandaInteraction(true);
        hasInteracted = true;
        isInteracting = true;
        CameraController.instance.CameraSwitch();  
    }

    private void PickAHand(int answer)
    {
        pandaActive = false;
        UIScript.instance.ShowPandaInteraction(false);
        if(answer == 1 && !answerChosen)
        {
            answerChosen = true;
            UIScript.instance.AnswerPandaCorrect(true);
            
            reaction = PandaReaction(3.0f, 1);
            StartCoroutine(reaction);

        }
        else if(answer == 2 && !answerChosen)
        {
            answerChosen = true;
            UIScript.instance.AnswerPandaWrong(true);
            
            reaction = PandaReaction(3.0f, 2);
            StartCoroutine(reaction);

        } 
    }

    private IEnumerator PandaReaction(float waitTime, int answer)
    {
        yield return new WaitForSeconds(waitTime);
        if(answer == 1)
        {
            UIScript.instance.AnswerPandaCorrect(false);
            UIScript.instance.PandaReactCorrect(true);
            
            HealthManager.instance.GainHP(2.0f);
            PandaAnimator.SetTrigger("rightAnswer");  

            stopInteraction = StopInteraction(5.0f);
            StartCoroutine(stopInteraction);
        }
        else if(answer == 2)
        {
            UIScript.instance.AnswerPandaWrong(false);
            UIScript.instance.PandaReactWrong(true);
            
            HealthManager.instance.TakeDamage(2.0f);
            PandaAnimator.SetTrigger("wrongAnswer");    

            stopInteraction = StopInteraction(5.0f);
            StartCoroutine(stopInteraction);
        }
        
    }
    private IEnumerator StopInteraction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UIScript.instance.ShowPandaInteraction(false);
        UIScript.instance.PandaReactCorrect(false);
        UIScript.instance.PandaReactWrong(false);
        CameraController.instance.CameraSwitch();
        isInteracting = false;
        hasInteracted = false;
        answerChosen = false;
    }


}
