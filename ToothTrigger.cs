using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothTrigger : MonoBehaviour
{
    private ToothScript toothScript;
    private bool triggerActive;
    private bool triggerPushed;
    private IEnumerator resetTrigger;
    private bool isInteracting;
    
    void Start()
    {
        triggerActive = false;
        triggerPushed = false;

        //BlockingEye
        toothScript = GameObject.Find("Tooth").GetComponent<ToothScript>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
       
        if(triggerActive && isInteracting)
        {
            useTrigger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerPushed && !isInteracting)
        {
            UIScript.instance.ShowInteractTip(true);
            triggerActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
            UIScript.instance.ShowInteractTip(false); 
    }

    private void useTrigger()
    {
            StartCoroutine(ToothScript.instance.OpenTooth());
            triggerPushed = true;
            resetTrigger = ResetTrigger(20.0f);
            StartCoroutine(resetTrigger);

            triggerActive = false;
    }

    public IEnumerator ResetTrigger(float waitTime)
    {
            yield return new WaitForSeconds(waitTime);
            
            triggerPushed = false;
    }
}
