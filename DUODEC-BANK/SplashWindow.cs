using System;
using System.Drawing;
using System.Windows.Forms;
namespace DUODEC_BANK;
public class SplashWindow : Form
{
    PictureBox logo;
    Label text;
    Timer timer;
    int step;
    public SplashWindow()
    {
        BackColor=ColorTranslator.FromHtml("#F2F3EE");
        FormBorderStyle=FormBorderStyle.None;
        StartPosition=FormStartPosition.CenterScreen;
        ClientSize=new Size(420,420);
        logo=new PictureBox{Bounds=new Rectangle(110,90,200,200),SizeMode=PictureBoxSizeMode.Zoom,BackColor=Color.Transparent};
        var p=System.IO.Path.Combine(AppContext.BaseDirectory,"Assets","logo.png");
        if(System.IO.File.Exists(p)) logo.Image=Image.FromFile(p);
        text=new Label{Bounds=new Rectangle(20,300,380,50),TextAlign=ContentAlignment.MiddleCenter,Font=new Font("Segoe UI",16),ForeColor=Color.FromArgb(35,35,35)};
        Controls.Add(logo); Controls.Add(text);
        timer=new Timer{Interval=45}; timer.Tick+=Animate; Shown+=(_,_)=>timer.Start();
    }
    void Animate(object? s,EventArgs e)
    {
        step++;
        if(step<=18){int n=120+step*4;logo.Bounds=new Rectangle(210-n/2,190-n/2,n,n);return;}
        if(step<=55){string t="O banco que resolve.";text.Text=t[..Math.Min(step-18,t.Length)];return;}
        if(step<=72){text.Visible=false;return;}
        timer.Stop();Hide();
        var main=new MainWindow();main.FormClosed+=(_,_)=>Close();main.Show();
    }
}