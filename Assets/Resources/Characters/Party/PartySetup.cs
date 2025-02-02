using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyEvents : EventArgs
{
    public PartySetup PartySetup;
    public PartySlot PartyMemberSlot;
}

public class PartySetup
{
    private const int MAX_MEMBERS = 4;
    private PartySlot[] partyMemberSlots;
    public PartySlot selectedPartyMemberSlot { get; private set; }
    public event Action OnSelectPartyMemberChanged;
    public event EventHandler<PartyEvents> OnPartyAdd;
    public event EventHandler<PartyEvents> OnPartyRemove;

    public PartySetup()
    {
        partyMemberSlots = new PartySlot[MAX_MEMBERS];

        for (int i = 0; i < partyMemberSlots.Length; i++)
        {
            PartySlot partySlot = new PartySlot();
            partySlot.OnPartyAdd += PartySlot_OnPartyAdd;
            partySlot.OnPartyRemove += PartySlot_OnPartyRemove;
            partyMemberSlots[i] = partySlot;
        }

        SelectPartyMemberSlot(0);
    }

    private void PartySlot_OnPartyRemove(PartySlot PartySlot, PartyMember PartyMember)
    {
        OnPartyRemove?.Invoke(this, new PartyEvents
        {
            PartySetup = this,
            PartyMemberSlot = PartySlot,
        });
    }

    private void PartySlot_OnPartyAdd(PartySlot PartySlot, PartyMember PartyMember)
    {
        OnPartyAdd?.Invoke(this, new PartyEvents
        {
            PartySetup = this,
            PartyMemberSlot = PartySlot,
        });
    }

    public void SelectPartyMemberSlot(int PartyLocation)
    {
        selectedPartyMemberSlot = GetPartySlot(PartyLocation);
        OnSelectPartyMemberChanged?.Invoke();
    }

    public int GetSelectedPartyMemberSlotIndex()
    {
        for (int i = 0; i < partyMemberSlots.Length; i++)
        {
            PartySlot partySlot = partyMemberSlots[i];

            if (partySlot == selectedPartyMemberSlot)
                return i;
        }

        return -1;
    }

    public List<PartySlot> GetKnownPartySlots()
    {
        List<PartySlot> PartyLayout = new(partyMemberSlots);

        for (int i = PartyLayout.Count - 1; i >= 0; i--)
        {
            if (!PartyLayout[i].HasMember())
            {
                PartyLayout.RemoveAt(i);
            }
        }

        return PartyLayout;
    }

    public PartySlot GetPartySlot(int PartyLocation)
    {
        int index = PartyLocation;

        index = Mathf.Clamp(index, 0, partyMemberSlots.Length);

        return partyMemberSlots[index];
    }

    public PartySlot GetKnownPartySlot(int PartyLocation)
    {
        List<PartySlot> PartyLayout = new(GetKnownPartySlots());

        if (PartyLayout.Count == 0)
            return null;

        int index = PartyLocation;

        index = Mathf.Clamp(index, 0, PartyLayout.Count);

        return PartyLayout[index];
    }


    public void RemoveMember(CharactersSO charactersSO)
    {
        int slot = Find(charactersSO);

        if (slot == -1)
            return;

        RemoveMember(slot);
    }

    public void RemoveMember(int PartyLocation)
    {
        partyMemberSlots[PartyLocation].RemoveMember();
    }

    public void AddMember(CharacterDataStat CharacterDataStat)
    {
        AddMember(CharacterDataStat, GetEmptySlot());
    }

    public void AddMember(CharacterDataStat CharacterDataStat, int PartyLocation)
    {
        int existCharacterInSlot = Find(CharacterDataStat.damageableEntitySO);

        if (existCharacterInSlot != -1)
        {
            return;
        }

        PartySlot partySlot = GetPartySlot(PartyLocation);
        partySlot.AddMember(CharacterDataStat);
    }

    private int Find(CharactersSO charactersSO)
    {
        int slot = -1;

        if (charactersSO == null)
            return slot;

        for(int i = 0; i < partyMemberSlots.Length; i++)
        {
            PartyMember PartyMember = GetPartySlot(i).partyMember;
            if (PartyMember != null && PartyMember.characterDataStat.damageableEntitySO == charactersSO)
            {
                slot = i;
                return slot;
            }

        }

        return slot;
    }

    private int GetEmptySlot()
    {
        for (int i = 0; i < partyMemberSlots.Length; i++)
        {
            if (!partyMemberSlots[i].HasMember())
            {
                return i;
            }
        }

        return -1;
    }

    public void OnDestroy()
    {
        for (int i = 0; i < partyMemberSlots.Length; i++)
        {
            PartySlot partySlot = partyMemberSlots[i];
            partySlot.OnPartyAdd -= PartySlot_OnPartyAdd;
            partySlot.OnPartyRemove -= PartySlot_OnPartyRemove;
        }
    }
}
