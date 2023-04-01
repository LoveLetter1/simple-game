using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum TapLevel
{
    Miss,Good,Perfect
}
public class Note : MonoBehaviour
{
    public float remainTime;
  
    public float duration;

    public float perfectTime;
    
    public float perfectTimeOffset;
    
    public float greatTimeOffset;

    public float multipleTime;

    [SerializeField]
    private float moveSpeed = 1f;

    private RectTransform rectTrans;

    public Vector2 startV2;
    public Vector2 endV2;
    

    public bool isActivated;
    public bool isShow;
    public bool hasTapped;

    private void Start()
    {

    }

    private void Update()
    {
        if (isActivated)
        {
            remainTime -= (Time.deltaTime * multipleTime);
            
            if ((remainTime * multipleTime) < (perfectTime - greatTimeOffset) * multipleTime && isShow && !hasTapped)
            {
                HideNote();
                UIManager.Instance.GetPanelBase("TapLevelPanel").GetComponent<TapLevelPanel>().ShowTapLevelText(TapLevel.Miss);

                RhythmManager.Instance.NoteDeQueue();
                
                //TODO: combo断连
                RhythmManager.Instance.OutMusicMode("Miss");
                RhythmManager.Instance.BreakCombo();
            }
            if(remainTime <= 0.001f )
            {
                Reset();
            }

        }
    }
    private void FixedUpdate()
    {
        if (isActivated)
        {
            rectTrans.position += new Vector3((- startV2.x + endV2.x ) * Time.deltaTime * multipleTime  / 2 , 0,0);
        }
    }

    void initialization()
    {
        rectTrans = GetComponent<RectTransform>();


    }
    



    public void Reset()
    {
        transform.position = startV2;
        remainTime = duration;
        isActivated = false;
        hasTapped = false;
        HideNote();
    }

    public void SetActive(Vector2 start,Vector2 end)
    {
        
        startV2 = start;
        endV2 = end;
        Reset();
        isActivated = true;
        this.multipleTime = GameManager.Instance.multipleTime;
        ShowNote();
    }
    public TapLevel BeTapped()
    {
        hasTapped = true;
        HideNote();
        RhythmManager.Instance.NoteDeQueue();
        TapLevel tapLevel = TapLevel.Miss;
        if (remainTime * multipleTime > (perfectTime + greatTimeOffset) * multipleTime || remainTime * multipleTime < (perfectTime - greatTimeOffset) * multipleTime)
            tapLevel = TapLevel.Miss;
        else if (remainTime * multipleTime <= (perfectTime + perfectTimeOffset) * multipleTime && remainTime * multipleTime >= (perfectTime - perfectTimeOffset) * multipleTime)
            tapLevel = TapLevel.Perfect;
        else
            tapLevel = TapLevel.Good;

        return tapLevel;
        
    }
    private void ShowNote()
    {
        isShow = true;
        GetComponent<CanvasGroup>().alpha = 1;
    }
    private void HideNote()
    {
        isShow = false;
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Init(Vector2 startV2)
    {
        this.startV2 = startV2;

        initialization();
        //Reset();
        
    }
}
