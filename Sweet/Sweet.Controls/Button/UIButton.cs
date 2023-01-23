using DxLibDLL;

namespace Sweet.Controls;

public class UIButton : UIButtonBase
{
    /// <summary>
    /// テキスト
    /// </summary>
    public string Text
    {
        get => _text.Text;
        set => _text.Text = value;
    }

    /// <summary>
    /// 初期化する
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">高さ</param>
    /// <param name="fontName">フォント名</param>
    /// <param name="fontSize">フォントサイズ</param>
    /// <param name="fontThick">フォントの太さ</param>
    public UIButton(int width, int height, string fontName, int fontSize, int fontThick)
        : base(width, height, fontName, fontSize, fontThick)
    {
        Text = "Button";
    }

    public override void Update()
    {
        base.Update();
        _text.FontHandle = FontHandle;
        _text.ParentWidth = Width;
        _text.ParentHeight = Height;
        _text.UpdateText();

        IsTickAnimation = IsPushing();
    }

    protected override void DrawViewArea()
    {
        base.DrawViewArea();
        DrawFade();
        _text.DrawText();
    }

    /// <summary>
    /// フェードの描画
    /// </summary>
    private void DrawFade()
    {
        uint clickColor = DX.GetColor(ClickColor.R, ClickColor.G, ClickColor.B);
        double fade = Math.Sin(AnimeValue * Math.PI / 180) * FadeAlpha;

        DX.SetDrawBlendMode(DX.DX_BLENDMODE_PMA_ALPHA, (int)fade);
        if (Radius <= 0)
        {
            DX.DrawFillBox(0, 0, Width, Height, clickColor);
        }
        else
        {
            DX.DrawRoundRectAA(0, 0, Width, Height, Radius, Radius, 100, clickColor, DX.TRUE);
        }
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_PMA_ALPHA, 255);
    }
}