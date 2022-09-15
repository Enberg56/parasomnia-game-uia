using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger4 : MonoBehaviour
{
    private BlockingEyeScript blockingEyeScript;
    private bool triggerActive;
    private bool triggerPushed;
    private IEnumerator resetTrigger;
    private bool isInteracting;
    
    void Start()
    {
        triggerActive = false;
        triggerPushed = false;

        //BlockingEye
        blockingEyeScript = GameObject.Find("BlockingEye").GetComponent<BlockingEyeScript>();
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
            BlockingEyeScript.instance.DestroyPlatform();
            triggerPushed = true;
            resetTrigger = ResetTrigger(20.0f);
            StartCoroutine(resetTrigger);

            triggerActive = false;
    }

    public IEnumerator ResetTrigger(float waitTime)
    {
            yield return new WaitForSeconds(waitTime);
            BlockingEyeScript.instance.RespawnPlatfrom();
            
            triggerPushed = false;
    }
}
