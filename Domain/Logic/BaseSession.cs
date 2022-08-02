namespace Domain.Logic
{
    public abstract class BaseSession<TPlayer>
    {
        protected ICollection<TPlayer> _players { get; }
        internal virtual bool IsGameOver()
        {
            if (_players.Count == 1)
            {
                return true;
            }

            return false;
        }
        internal abstract void Start();
        internal abstract void Restart();
    }
}
