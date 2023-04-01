using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerController playerController;
    public Queue<KeyCode> inputQueue;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (RhythmManager.Instance.InMusicMode())
            {
                
            }
        }
        if (RhythmManager.Instance.inMusicMode)
        {
            Tap(KeyCode.J);
            Tap(KeyCode.K);
            Tap(KeyCode.L);
        }
        else
        {
            

        }

    }

    private void Tap(KeyCode keyCode) 
    {
        if (Input.GetKeyDown(keyCode))
            RhythmManager.Instance.Tap(keyCode);
    } 
    private void Attack(KeyCode keyCode)
    {
        
    }

    
}
