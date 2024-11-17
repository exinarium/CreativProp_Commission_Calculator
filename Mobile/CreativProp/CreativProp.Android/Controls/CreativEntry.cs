using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Widget;
using CreativProp.Controls;
using CustomRenderer.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CreativEntry), typeof(CreativEntryRenderer))]
namespace CustomRenderer.iOS
{
    public class CreativEntryRenderer : EntryRenderer
    {
        public CreativEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
                //this.Control.KeyListener = DigitsKeyListener.GetInstance(string.Format("1234567890{0}",
                //System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            }
        }
    }
}