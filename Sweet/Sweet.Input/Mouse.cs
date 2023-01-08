﻿using DxLibDLL;

namespace Sweet.Input;

public static class Mouse
{
    private static sbyte[] value = new sbyte[5];

    /// <summary>
    /// 更新する
    /// </summary>
    public static void Update()
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (DX.GetMouseInput() == (int)GetMouseKey(i))
            {
                if (!IsPushing(GetMouseKey(i)))
                    value[i] = 1;
                else
                    value[i] = 2;
            }
            else
            {
                if (IsPushing(GetMouseKey(i)))
                    value[i] = -1;
                else
                    value[i] = 0;
            }
        }
    }

    /// <summary>
    /// 押している間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    /// <returns>押された: True</returns>
    public static bool IsPushing(MouseKey keyCode)
        => value[GetMouseKeyIndex(keyCode)] > 0;

    /// <summary>
    /// 推した瞬間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    /// <returns>押された: True</returns>
    public static bool IsPushed(MouseKey keyCode)
        => value[GetMouseKeyIndex(keyCode)] == 1;

    /// <summary>
    /// 離した瞬間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    /// <returns>離された: True</returns>
    public static bool IsSeparate(MouseKey keyCode)
        => value[GetMouseKeyIndex(keyCode)] == -1;

    private static MouseKey GetMouseKey(int index) => index switch
    {
        0 => MouseKey.Left,
        1 => MouseKey.Right,
        2 => MouseKey.Middle,
        3 => MouseKey.Input_4,
        4 => MouseKey.Input_5,
        _ => MouseKey.Left
    };

    private static int GetMouseKeyIndex(MouseKey mouseKey) => mouseKey switch
    {
        MouseKey.Left => 0,
        MouseKey.Right => 1,
        MouseKey.Middle => 2,
        MouseKey.Input_4 => 3,
        MouseKey.Input_5 => 4,
        _ => 0
    };
}

public enum MouseKey
{
    Left = DX.MOUSE_INPUT_LEFT,
    Right = DX.MOUSE_INPUT_RIGHT,
    Middle = DX.MOUSE_INPUT_MIDDLE,
    Input_4 = DX.MOUSE_INPUT_4,
    Input_5 = DX.MOUSE_INPUT_5,
}