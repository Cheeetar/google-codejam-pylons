namespace Battlestarcraft_Algorithmica
{
    internal class Starship
    {
        public string Name { get; }
        private Sector _location;

        public Starship(string name)
        {
            Name = name;
        }

        public bool SuccessfulUntraceableJump(Sector proposedJump)
        {
            if (_location == null
                || Navigation.IsUntraceableJump(_location, proposedJump))
            {
                _location = proposedJump;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EmergencyJump(Sector jump)
        {
            _location = jump;
        }
    }
}
