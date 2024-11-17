using CreativProp.Controls;
using MyApp.iOS.Renderer;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CreativEntry), typeof(CreativEntryRenderer))]
namespace MyApp.iOS.Renderer
{
    public class CreativEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
        }
    }
}