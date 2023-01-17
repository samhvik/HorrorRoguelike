using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateMachineDisplay : MonoBehaviour
{
    public TMP_Text displayText;
    public PlayerStateMachine stateMachine; 
    // Start is called before the first frame update
    void Start()
    {
        //displayText = GetComponent<TextMeshProUGUI>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = stateMachine.currentState + "";
    }
}
