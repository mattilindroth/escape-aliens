namespace escape_aliens.Engine.Input
{
    public class Input
    {
        private KeyboardBindings _keyboardBindings ;
        //private MouseBindings _mouseBindings;

        public Input () {
            _keyboardBindings = new KeyboardBindings();
        }

        public KeyboardBindings KeyboardBindings {get {return _keyboardBindings;}}

    }
}