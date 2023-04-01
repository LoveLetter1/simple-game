
using UnityEngine;
using UnityEngine.UI;

public class PropPanel : PanelBase
{
    public Image[] redHearts;
    public Image[] blueHearts;

    protected override void Awake()
    {
        base.Awake();
                
        redHearts = transform.GetChild(0).GetComponentsInChildren<Image>();
        blueHearts = transform.GetChild(1).GetComponentsInChildren<Image>();
    }


    /// <summary>
/// 设置UI红血蓝血数
/// </summary>
/// <param name="str">value:Hp or Mp</param>
/// <param name="num"></param>
    public void SetHeartUINum(string str,int num)
    {
        var hearts = str == "Hp" ? redHearts : str == "Mp" ? blueHearts : null;
        if (hearts == null)
            return;

        for (int i = 0; i < hearts.Length; i++)
        {
            Color color = hearts[i].color;
            hearts[i].color = i < num
                ? new Color(color.r, color.g, color.b, 1)
                : new Color(color.r, color.g, color.b, 0);
        }


    }
}
