using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CustomControls
{
    public partial class FlipPanel
    {
        #region FrontContentProperty
        public object FrontContent
        {
            get { return (object)GetValue(FrontContentProperty); }
            set { SetValue(FrontContentProperty, value); }
        }
        public static readonly DependencyProperty FrontContentProperty =
            DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel), null);

        #endregion

        #region BackContentProperty

        public object BackContent
        {
            get { return (object)GetValue(BackContentProperty); }
            set { SetValue(BackContentProperty, value); }
        }
        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), null);

        #endregion

        #region CornerRadiusProperty
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel), null);

        #endregion

        #region IsFlippedProperty
        public bool IsFlipped
        {
            get { return (bool)GetValue(IsFlippedProperty); }
            set { SetValue(IsFlippedProperty, value); ChangeVisualState(true); }
        }
        public static readonly DependencyProperty IsFlippedProperty =
            DependencyProperty.Register("IsFlipped", typeof(bool), typeof(FlipPanel), null);

        #endregion
    }
}
