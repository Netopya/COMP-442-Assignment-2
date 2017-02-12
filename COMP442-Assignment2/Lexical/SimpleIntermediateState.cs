using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        A non-final node representing a transition
        to a set of possible states
    */
    class SimpleIntermediateState : IState
    {
        private readonly Dictionary<ICharacterMatch, IState> _transitions;
        private IState _defaultState;

        public SimpleIntermediateState(Dictionary<ICharacterMatch, IState> transitions, IState defaultState)
        {
            _transitions = transitions;
            _defaultState = defaultState;
        }

        // If the transition nodes haven't been created yet,
        // we can add them later with addTransition
        public SimpleIntermediateState(IState defaultState)
        {
            _transitions = new Dictionary<ICharacterMatch, IState>();
            _defaultState = defaultState;
        }

        // This constructor allows a reflexive default state
        public SimpleIntermediateState()
        {
            _transitions = new Dictionary<ICharacterMatch, IState>();
            _defaultState = this;
        }

        public void addTransition(ICharacterMatch match, IState state)
        {
            _transitions.Add(match, state);
        }

        // Determine if there is a match for the provided character and
        // return the appropriate state
        public IState getNextState(char character)
        {
            KeyValuePair<ICharacterMatch, IState>? nextStatePair = _transitions.FirstOrDefault(x => x.Key.doesCharacterMatch(character));

            if (nextStatePair.Value.Value != null)
                return nextStatePair.Value.Value;
            else
                return _defaultState; // Return the default state for this node if there is no match
        }

        // This class is non final, so backtracking is
        // irrelevant and no token can be provided
        public bool isFinalState()
        {
            return false;
        }

        public bool backTrack()
        {
            throw new NotImplementedException();
        }

        public IToken token()
        {
            throw new NotImplementedException();
        }
    }
}
