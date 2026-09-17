using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
namespace DUODEC_BANK;
public class MainWindow:Form
{
    readonly WebView2 web;
    readonly Label loading;
    const string URL="https://duodecbank.vercel.app/indexApp.html";
    public MainWindow()
    {
        Text="DUODEC BANK"; WindowState=FormWindowState.Maximized; MinimumSize=new Size(900,600);
        loading=new Label{Dock=DockStyle.Fill,Text="Carregando...",TextAlign=ContentAlignment.MiddleCenter,Font=new Font("Segoe UI",16),BackColor=ColorTranslator.FromHtml("#F2F3EE")};
        web=new WebView2{Dock=DockStyle.Fill,Visible=false};
        Controls.Add(web);Controls.Add(loading);Shown+=async(_,_)=>await Init();
    }
    async System.Threading.Tasks.Task Init()
    {
        try{
            await web.EnsureCoreWebView2Async();
            web.CoreWebView2.Settings.IsScriptEnabled=true;
            web.CoreWebView2.Settings.IsWebMessageEnabled=true;
            web.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled=true;
            web.CoreWebView2.Settings.IsStatusBarEnabled=false;
            web.CoreWebView2.Settings.IsZoomControlEnabled=false;
            web.CoreWebView2.PermissionRequested+=Permission;
            web.CoreWebView2.NewWindowRequested+=(s,e)=>{e.Handled=true;web.CoreWebView2.Navigate(e.Uri);};
            web.NavigationCompleted+=(s,e)=>{loading.Visible=false;web.Visible=true;};
            web.Source=new Uri(URL);
        }catch(Exception ex){loading.Text="Não foi possível abrir o DUODEC BANK.\n\n"+ex.Message;}
    }
    void Permission(object? s,CoreWebView2PermissionRequestedEventArgs e)
    {
        if(e.PermissionKind==CoreWebView2PermissionKind.Camera||e.PermissionKind==CoreWebView2PermissionKind.Microphone)
            e.State=CoreWebView2PermissionState.Allow;
    }
}