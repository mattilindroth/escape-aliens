using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine
{
    public class SpriteComponent: Component, IRenderable
    {
        private int _ZValue;
        private bool _visible;

        private List<Texture> _animationChain;
        private int _currentFrameIndex;
        private bool _flipTextures;
        private Transformation2D _transformation;

        public SpriteComponent(Transformation2D transformation, int ZValue = 0) {
            _visible = true;
            _ZValue = ZValue;
            _flipTextures = false;
            _transformation = transformation;
            _animationChain = new List<Texture>();
        }

        public bool FlipTextures {
            get {return _flipTextures;}
            set {_flipTextures = value;}
        }

        public int CountOfAnimationFrames {get {return _animationChain.Count;}}

        public void AddAnimationFrame(Texture frame) {
            _animationChain.Add(frame);
        }

        public void AdvanceFrame() {
            if(_currentFrameIndex == _animationChain.Count -1)
                _currentFrameIndex = 0;
            else
                _currentFrameIndex += 1;
        }

        Transformation2D IRenderable.Transformation {get {return _transformation;}}
        
        int IRenderable.ZValue {
            get {return _ZValue;}
        }

        bool IRenderable.DoRender {
            get {return _visible;}
        }

        void IRenderable.Render(Renderer renderer) {
            renderer.DrawTexture(_animationChain[_currentFrameIndex], _transformation, _flipTextures);
        }
    }
}