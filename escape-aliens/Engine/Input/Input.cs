namespace escape_aliens.Engine.Input
{
    public class Input
    {
        private KeyboardBindings _keyboardBindings ;
        private MouseBindings _mouseBindings;

        public Input () {
            _keyboardBindings = new KeyboardBindings();
            _mouseBindings = new MouseBindings();
        }

        public KeyboardBindings KeyboardBindings {get {return _keyboardBindings;}}
        public MouseBindings MouseBindings {get {return _mouseBindings;}}
    }
}