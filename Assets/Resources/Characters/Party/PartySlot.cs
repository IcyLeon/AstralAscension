using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySlot
{
    public PartyMember partyMember { get; private set; }

    public delegate void OnPartySlotChanged(PartySlot PartySlot, PartyMember PartyMember);
    public event OnPartySlotChanged OnPartyAdd;
    public event OnPartySlotChanged OnPartyRemove;

    public void AddMember(CharacterDataStat CharacterDataStat)
    {
        if (HasMember() || CharacterDataStat == null)
            return;

        partyMember = new PartyMember(CharacterDataStat);
        OnPartyAdd?.Invoke(this, partyMember);
    }

    public void RemoveMember()
    {
        if (!HasMember())
            return;

        OnPartyRemove?.Invoke(this, partyMember);
        partyMember = null;
    }

    public bool HasMember()
    {
        return partyMember != null;
    }
}
