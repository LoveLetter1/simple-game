using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class RhythmManager : UnitySingleton<RhythmManager>
{
    public int noteCount;
    public GameObject notePrefab;
    public Note[] notes;
    public float noteTimeScale;
    [ShowInInspector]
    public Queue<Note> noteQueue;
    public int noteIdx = 0;
    //拍击顺序（临时）
    [ShowInInspector]
    public string keyCodes;
    public bool inMusicMode;
    
    [SerializeField]
    private RectTransform startLine;
    [SerializeField]
    private RectTransform endLine;

    public float rhythmTime = 1f;
    public float rhythmTimeCounter;
    private void Awake()
    {
        initialization();
    }

    void initialization()
    {
        notes = new Note[noteCount];
        noteQueue = new Queue<Note>();
        keyCodes = "";
        Transform notesTrans = GameObject.Find("Notes_U").transform;
        for (int idx = 0; idx < noteCount; idx++)
        {
            var position = startLine.position;
            var noteObj = Instantiate(notePrefab, position, default, notesTrans);
            noteObj.name = "Note_U";
            var note = noteObj.GetComponent<Note>();
            note.Init(position);
            notes[idx] = note;
        }
    }

    private void Update()
    {
        if (inMusicMode)
        {
            rhythmTimeCounter -= Time.deltaTime;
            if (rhythmTimeCounter <= 0f)
            {
                AudioManager.Instance.PlayNoteCrossMusic();
                StartNextNote(startLine.position,endLine.position);
                rhythmTimeCounter = rhythmTime / GameManager.Instance.multipleTime;
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public bool InMusicMode()
    {
        if (!inMusicMode)
        {
            inMusicMode = true;
            GameManager.Instance.OnSetTimeScaleEv(2f);

            rhythmTimeCounter = rhythmTime / GameManager.Instance.multipleTime;

            noteQueue.Enqueue(notes[noteIdx]);
            notes[noteIdx].SetActive(startLine.position,endLine.position);
            return true;
        }

        return false;

    }

    public bool OutMusicMode(string str)
    {
        inMusicMode = false;
        GameManager.Instance.OnResetTimeScaleEv();
        AudioManager.Instance.OutMusicModeReset();
            
        for (int idx = 0; idx < notes.Length; idx++)
        {
            notes[idx].Reset();
        }
        noteQueue.Clear();
        switch (str)
        {
            case "Normal":
                Skill skill = null;
                GameManager.Instance.FindSkill(keyCodes, out skill);
                if(skill && GameManager.Instance.playerController.mp >= skill.mpUsage)//TODO:测试集合
                {
                    var playerController = GameManager.Instance.playerController;
                    var playerTrans = playerController.transform;
                    skill.ActiveSkill(playerController.gameObject);
                    playerController.UseMp(skill.mpUsage);
                    keyCodes = "";
                    UIManager.Instance.GetPanelBase("TapLevelPanel").GetComponent<TapLevelPanel>().HideTapLevelText();
                    UIManager.Instance.GetPanelBase("ComboPanel").GetComponent<ComboPanel>().BreakCombo();
                    return true;
                }
                else
                {
                    Debug.Log("顺序不匹配或mp不够,无法触发技能，退出奏乐模式，无惩罚");
                    keyCodes = "";

                    UIManager.Instance.GetPanelBase("TapLevelPanel").GetComponent<TapLevelPanel>().HideTapLevelText();
                    UIManager.Instance.GetPanelBase("ComboPanel").GetComponent<ComboPanel>().BreakCombo();
                    return false;
                }
                break;
            case "Miss":
                
                break;
        }
        return false;
    }
    private bool CheckLists(List<KeyCode> list1,List<KeyCode> list2)
    {
        if (list1.Count != list2.Count || list1.Count <= 0 || list2.Count <= 0)
            return false;
        for (int idx = 0; idx < list1.Count; idx++)
        {
            if (list1[idx] != list2[idx])
                return false;
        }
        return true;
    }
    public void StartNextNote(Vector2 start,Vector2 end)
    {
        noteIdx = (noteIdx + 1) % notes.Length;
        noteQueue.Enqueue(notes[noteIdx]);
        notes[noteIdx].SetActive(start,end);
    }
    public void NoteDeQueue()
    {
        
        noteQueue.Dequeue();
    }
    public void BreakCombo()
    {
        keyCodes = "";

        UIManager.Instance.GetPanelBase("ComboPanel").GetComponent<ComboPanel>().BreakCombo();


    }
    public void Tap(KeyCode keyCode)
    {
        if (noteQueue.Count == 0) return;
        TapLevel tapLevel = noteQueue.Peek().BeTapped();
        UIManager.Instance.GetPanelBase("TapLevelPanel").GetComponent<TapLevelPanel>().ShowTapLevelText(tapLevel);

        switch (tapLevel)
        {
            case TapLevel.Miss:
                Debug.Log("Miss音符，跳出节奏模式，获得惩罚");
                keyCodes = "";
                OutMusicMode("Normal");
                UIManager.Instance.GetPanelBase("ComboPanel").GetComponent<ComboPanel>().BreakCombo();
                
                break;
            case TapLevel.Good:

                Debug.Log(keyCode.ToString());
                keyCodes += keyCode.ToString();
                UIManager.Instance.GetPanelBase("ComboPanel").GetComponent<ComboPanel>().RefreshCombo();

                break;
            case TapLevel.Perfect:
                Debug.Log(keyCode.ToString());
                keyCodes += keyCode.ToString();
                UIManager.Instance.GetPanelBase("ComboPanel").GetComponent<ComboPanel>().RefreshCombo();

                break;
            default:
                Debug.LogError("传入的TapLevel有问题");
                break;
        }

        if (keyCodes.Length == 4)
        {
            OutMusicMode("Normal");
        }
    }
}
