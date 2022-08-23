using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public const float ItemFadeDuration = 0.35f;//遮蔽物透明渐变时间
    public const float FadeAlpha = 0.45f;//遮蔽物目标透明度

    public const float CanvasFadeDuration = 2.0f;

    //时间相关
    public const float secondThershold = 0.05f;

    //    每分、时、天、月、年
    //有多少秒、分、时、天、季
    public const int secondHold = 1;
    public const int minuteHold = 60;
    public const int hourHold = 16;
    public const int dayHold = 1;
    public const int seasonHold = 3;
}