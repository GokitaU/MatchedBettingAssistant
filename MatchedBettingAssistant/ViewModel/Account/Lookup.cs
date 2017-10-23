using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class Lookup<T> where T: ILookup
    {
        private readonly T lookup;

        public Lookup(T lookup)
        {
            this.lookup = lookup;
        }

        public int Id => this.lookup.Id;

        public string Name => this.lookup.Name;

        internal T Model => this.lookup;
    }
}