using Unity.Mathematics;
using UnityEngine;

namespace Abandio.Debugger
{
    //TODO: 绘制方向需要修改
    public class DebugDrawer : MonoBehaviour
    {
        public static void DrawCircle(Vector2 center,float radius,float smooth,float filpX, float r = 1f, float g = 0f, float b = 0f)
        {
            Color color = new Color(r, g, b);
            float duration = 2.5f;
            Vector2 startPointOffset =  new Vector2(radius, 0);
            Vector2 startPointUp = center + startPointOffset;
            Vector2 startPointDown = center - startPointOffset;
            for (float theta = smooth; theta < Mathf.PI; theta += smooth)
            {
                
                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);
                Vector2 endPointOffset = new Vector2(x, y);
                Vector2 endPointUp = center + endPointOffset;
                Vector2 endPointDown = center - endPointOffset;
                Debug.DrawLine(startPointUp ,endPointUp,color,duration);
                Debug.DrawLine(startPointDown,endPointDown,color,duration);
                startPointUp = endPointUp;
                startPointDown = endPointDown;
            }
        }

        public static void DrawBox(Vector2 center, Vector2 size, float filpX, float r = 1f, float g = 0f, float b = 0f)
        {
            Color color = new Color(r, g, b);
            Vector2 deltaSize = size;
            float duration = 2.5f;
            Debug.DrawLine(center + deltaSize * new Vector2(-1,-1),center + deltaSize * new Vector2(1,-1),color,duration);
            Debug.DrawLine(center + deltaSize * new Vector2(-1,-1),center + deltaSize * new Vector2(-1,1),color,duration);
            Debug.DrawLine(center + deltaSize * new Vector2(1,1),center + deltaSize * new Vector2(-1,1),color,duration);
            Debug.DrawLine(center + deltaSize * new Vector2(1,1),center + deltaSize * new Vector2(1,-1),color,duration);
        }
        
    }
}
