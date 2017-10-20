using System;
using UIKit;

namespace Lab05
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            var TranslatedNumber = string.Empty;

            TranslateButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                PhoneNumberText.ResignFirstResponder();

                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);

                if (string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    CallButton.SetTitle("Llamar", UIControlState.Normal);
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.SetTitle($"Llamar al {TranslatedNumber}", UIControlState.Normal);
                    CallButton.Enabled = true;
                }
            };

            CallButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                PhoneNumberText.ResignFirstResponder();

                var URL = new Foundation.NSUrl($"tel:{TranslatedNumber}");
                if (!UIApplication.SharedApplication.OpenUrl(URL))
                {
                    var Alert = UIAlertController.Create("No soportado",
                        "El esquema 'tel:' no es soportado en este dispositivo",
                        UIAlertControllerStyle.Alert);
                    Alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    PresentViewController(Alert, true, null);
                }
            };
        }

        /// <summary>
        /// Called when the system is running low on memory.
        /// </summary>
        /// <remarks>
        /// <para>This method is automatically called when the system is running low on memory. Application developers who override this method in order to release resources must call <c>base.DidReceiveMemoryWarning()</c>, as shown in the following code, taken from the “Media Notes” sample:</para>
        /// <example>
        ///   <code lang="C#"><![CDATA[
        /// public override void DidReceiveMemoryWarning ()
        /// {
        /// photoMap.Clear ();
        /// View = null;
        /// photoImageView = null;
        /// toolbar = null;
        /// syncIsNeeded = true;
        /// base.DidReceiveMemoryWarning();
        /// }
        /// ]]></code>
        /// </example>
        /// </remarks>
        /// <related type="sample" href="http://samples.xamarin.com/iOS/Samples/ByGuid?guid=FAE04EC0-301F-11D3-BF4B-00C04F79EFBC">Media Notes</related>
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        /// <summary>
        /// Verifies the button touch up inside.
        /// </summary>
        /// <param name="sender">The sender.</param>
        partial void VerifyButton_TouchUpInside(UIButton sender)
        {
            Validate();
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        async void Validate()
        {
            var Client = new SALLab05.ServiceClient();
            var result = await Client.ValidateAsync("email@email.com", "password", this);

            var Alert = UIAlertController.Create("Resultado ",
                $"{result.Status}\n{result.FullName}\n{result.Token}",
                UIAlertControllerStyle.Alert);

            Alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

            PresentViewController(Alert, true, null);
        }
    }
}