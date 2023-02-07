﻿using DxLibDLL;
using System.Diagnostics;
using Sweet.Input;
using Sweet.Elements;
using Sweet.Controls;
using System.Drawing;
using System.Net.NetworkInformation;

namespace TestApp;

internal class App
{
    private double ms = 1.0 / 60.0;
    private Stopwatch stopwatch = new();

    private UIButton? Btn1;
    private VStackPanel stack = new(300, 400);

    private Scroller s = new(100, 500);


    public void Run()
    {
        Init();
        Loop();
        End();
    }

    private void Init()
    {
        DX.SetOutApplicationLogValidFlag(DX.FALSE);
        // DX3D9EXにするとなんも表示されなくなる...　D3DX9EXはサポート対象外にしますか...
        //DX.SetUseDirect3DVersion(DX.DX_DIRECT3D_9EX);
        DX.SetGraphMode(1920, 1080, 32);
        DX.SetWindowSize(1920, 1080);
        DX.SetBackgroundColor(255, 0, 0);
        DX.ChangeWindowMode(DX.TRUE);
        DX.SetWaitVSyncFlag(DX.FALSE);
        DX.DxLib_Init();
        DX.SetDrawScreen(DX.DX_SCREEN_BACK);
        DX.CreateMaskScreen();

        Btn1 = new(200, 200, "Segoe UI", 20, 0);
        Btn1.Style.BackAlpha = 255;
        Btn1.Style.ForeColor = Color.Orange;
        Btn1.Style.ClickColor = Color.Pink;
        Btn1.Text = string.Empty;
        Btn1.OnPushed += Test;

        stack.Children.Add(Btn1);
    }

    private void Loop()
    {
        while (true)
        {
            stopwatch.Restart();

            int ml = DX.ProcessMessage();

            if (ml == -1)
                break;

            DX.ClearDrawScreen();

            Keyboard.Update();
            Mouse.Update();
            Touch.Update();
            Joypad.Update();

            loop();

            DX.ScreenFlip();

            if (stopwatch.Elapsed.TotalSeconds < ms)
            {
                double sleepMs = (ms - stopwatch.Elapsed.TotalSeconds) * 1000.0;

                // Thread.Sleepで止めたらCPU使用率上がらない!!!!
                Thread.Sleep((int)sleepMs);

                // WaitTimerで止めるとCPU使用率が上がる...
                //DX.WaitTimer((int)sleepMs);
            }
        }
    }

    private void End()
    {
        DX.DxLib_End();
    }

    private void loop()
    {
        DX.GetWindowSize(out int w, out int h);

        DX.DrawString(0, 0 + (int)s.Value, $"{Touch.X} : {Touch.Y}", 0xffffff);
        DX.DrawString(20, 100, $"{Touch.X} : {Touch.Y}", 0xffffff);
    }

    private void Call(UIControl cnt)
    {
        DX.GetWindowSize(out int w, out int h);
        cnt.ParentWidth = w;
        cnt.ParentHeight = h;
        cnt.Update();
        cnt.DrawView();
    }

    private void Test()
    {
        Console.WriteLine("Akasoko");
    }
}