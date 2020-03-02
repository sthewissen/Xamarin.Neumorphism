using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamaneumorphism.Controls;
using Xamaneumorphism.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NeuButton), typeof(NeuButtonRenderer))]
namespace Xamaneumorphism.iOS.Renderers
{
    public class NeuButtonRenderer : ButtonRenderer
    {
        private CAShapeLayer _shadowLayer;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Control != null)
            {
                Control.Layer.MasksToBounds = false;
                Control.Layer.ShadowColor = Color.FromHex("#dfe4ee").ToCGColor();
                Control.Layer.CornerRadius = e.NewElement.CornerRadius;
                Control.Layer.ShadowOffset = new CGSize(4, 4);
                Control.Layer.ShadowOpacity = 1f;
                Control.Layer.ShadowRadius = 8;

                Control.TouchUpInside += OnButtonTouchUpInside;
                Control.TouchDown += OnButtonTouchDown;

                _shadowLayer = new CAShapeLayer
                {
                    Frame = Control.Bounds,
                    BackgroundColor = Color.FromHex("#f1f3f6").ToCGColor(),
                    ShadowColor = Color.White.ToCGColor(),
                    CornerRadius = e.NewElement.CornerRadius,
                    ShadowOffset = new CGSize(-4.0, -4.0),
                    ShadowOpacity = 1,
                    ShadowRadius = 4
                };

                Control.Layer.InsertSublayerBelow(_shadowLayer, Control.ImageView?.Layer);
            }
        }

        private void OnButtonTouchDown(object sender, EventArgs e)
        {
            Control.Layer.ShadowOffset = new CGSize(-4, height: -4);
            Control.Layer.Sublayers[0].ShadowOffset = new CGSize(4, 4);
            Control.ContentEdgeInsets = new UIEdgeInsets(4, 4, 0, 0);
        }

        private void OnButtonTouchUpInside(object sender, EventArgs e)
        {
            Control.Layer.ShadowOffset = new CGSize(4, height: 4);
            Control.Layer.Sublayers[0].ShadowOffset = new CGSize(-4, -4);
            Control.ContentEdgeInsets = new UIEdgeInsets(0, 0, 4, 4);
        }

        public override void Draw(CGRect rect)
        {
            _shadowLayer.Frame = Bounds;
            base.Draw(rect);
        }
    }
}
