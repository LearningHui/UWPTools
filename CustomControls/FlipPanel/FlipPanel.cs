using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace CustomControls
{
    public partial class FlipPanel : Control
    {
        public FlipPanel()
        {
            this.DefaultStyleKey = typeof(FlipPanel);
        }        

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ToggleButton flipButton = base.GetTemplateChild("FlipButton") as ToggleButton;
            if (flipButton != null) flipButton.Click += flipButton_Click;

            // Allow for two flip buttons if needed (one for each side of the panel).
            // This is an optional design, as the control consumer may use template
            // that places the flip button outside of the panel sides, like the 
            // default template does.
            ToggleButton flipButtonAlternate = base.GetTemplateChild("FlipButtonAlternate") as ToggleButton;
            if (flipButtonAlternate != null)
                flipButtonAlternate.Click += flipButton_Click;

            this.ChangeVisualState(false);
        }

        private void flipButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsFlipped = !this.IsFlipped;
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (!this.IsFlipped)
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Flipped", useTransitions);
            }

            // Disable flipped side to prevent tabbing to invisible buttons.            
            UIElement front = FrontContent as UIElement;
            if (front != null)
            {
                if (IsFlipped)
                {
                    front.Visibility = Visibility.Collapsed;
                }
                else
                {
                    front.Visibility = Visibility.Visible;
                }
            }
            UIElement back = BackContent as UIElement;
            if (back != null)
            {
                if (IsFlipped)
                {
                    back.Visibility = Visibility.Visible;
                }
                else
                {
                    back.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
