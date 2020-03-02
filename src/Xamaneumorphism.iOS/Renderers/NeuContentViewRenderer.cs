using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamaneumorphism.Controls;
using Xamaneumorphism.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NeuContentView), typeof(NeuContentViewRenderer))]
namespace Xamaneumorphism.iOS.Renderers
{
    public class NeuContentViewRenderer : ViewRenderer<NeuContentView, UIView>
    {
        private UIView _uiView;
        private CGSize _previousSize;
        private CAShapeLayer _shadowLayer;

        protected override void OnElementChanged(ElementChangedEventArgs<NeuContentView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _uiView = new UIView();

                foreach (var item in NativeView.Subviews)
                {
                    _uiView.AddSubview(item);
                }

                _uiView.Layer.MasksToBounds = false;
                _uiView.Layer.ShadowColor = Color.FromHex("#dfe4ee").ToCGColor();
                _uiView.Layer.ShadowOffset = new CGSize(4, 4);
                _uiView.Layer.ShadowOpacity = 1f;
                _uiView.Layer.ShadowRadius = 8;

                _shadowLayer = new CAShapeLayer
                {
                    BackgroundColor = Color.FromHex("#f1f3f6").ToCGColor(),
                    ShadowColor = Color.White.ToCGColor(),
                    ShadowOffset = new CGSize(-4.0, -4.0),
                    ShadowOpacity = 1,
                    ShadowRadius = 4
                };

                _uiView.Layer.InsertSublayer(_shadowLayer, 0);

                this.SetNativeControl(_uiView);
            }
        }

        public override void LayoutSubviews()
        {
            if (_previousSize != Bounds.Size)
                SetNeedsDisplay();

            base.LayoutSubviews();
        }

        public override void Draw(CGRect rect)
        {
            _uiView.Frame = Bounds;
            _shadowLayer.Frame = Bounds;

            base.Draw(rect);

            _previousSize = Bounds.Size;
        }
    }
}
