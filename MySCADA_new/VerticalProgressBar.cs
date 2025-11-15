using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

public class VerticalProgressBar : ProgressBar
{
    public VerticalProgressBar()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Rectangle rect = this.ClientRectangle;
        Graphics g = e.Graphics;

        if (ProgressBarRenderer.IsSupported)
            ProgressBarRenderer.DrawVerticalBar(g, rect);

        rect.Inflate(-3, -3);

        int percentage = (int)(((double)Value / Maximum) * rect.Height);
        Rectangle fill = new Rectangle(rect.X, rect.Bottom - percentage, rect.Width, percentage);

        using (Brush brush = new SolidBrush(System.Drawing.Color.LimeGreen))
        {
            g.FillRectangle(brush, fill);
        }
    }
}