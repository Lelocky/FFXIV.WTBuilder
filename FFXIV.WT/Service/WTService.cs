using FFXIV.WT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FFXIV.WT.Service
{
    public class WTService
    {
        private Dictionary<Guid, Duty> Duties;
        private Dictionary<Guid, Party> Parties;
        public WTService()
        {
            PopulateDuties();
            Parties = new Dictionary<Guid, Party>();
        }

        public Guid AddParty()
        {
            Guid partyKey = Guid.NewGuid();
            Parties.Add(partyKey, new Party { SelectedDuties = new Dictionary<Guid, int>(), RoomKey = partyKey });
            return partyKey;
        }

        public bool PartyExists(Guid key)
        {
            return Parties.ContainsKey(key);
        }

        public bool AddDutyToParty(Guid partyKey, Guid dutyKey)
        {
            if (!Parties.ContainsKey(partyKey))
            {
                return false;
            }

            if (!Duties.ContainsKey(dutyKey))
            {
                return false;
            }

            var party = Parties[partyKey];

            if (!party.SelectedDuties.ContainsKey(dutyKey))
            {
                party.SelectedDuties.Add(dutyKey, 1);
            }
            else
            {
                party.SelectedDuties[dutyKey]++;
            }

            return true;
        }

        public bool RemoveDutyFromParty(Guid partyKey, Guid dutyKey)
        {
            if (!Parties.ContainsKey(partyKey))
            {
                return false;
            }

            if (!Duties.ContainsKey(dutyKey))
            {
                return false;
            }

            var party = Parties[partyKey];

            if (party.SelectedDuties.ContainsKey(dutyKey))
            {
                party.SelectedDuties.Remove(dutyKey);
            }

            return true;
        }

        private void PopulateDuties()
        {
            Duties = new Dictionary<Guid, Duty>();

            //Dungeons
            AddDuty("Dungeons (Lv. 1-49)", 1 );
            AddDuty("Dungeons (Lv. 50)", 1);
            AddDuty("Dungeons (Lv. 51-59", 1);
            AddDuty("Dungeons (Lv. 60)", 1);
            AddDuty("Dungeons (Lv. 61-69)", 1);
            AddDuty("Dungeons (Lv. 70)", 1);
            AddDuty("Dungeons (Lv. 71-79)", 1);
            AddDuty("Dungeons (Lv. 80)", 1);

            //Raids
            AddDuty("Labyrinth of the Ancients", 2);
            AddDuty("Syrcus Tower", 2);
            AddDuty("The World of Darkness", 2);
            AddDuty("The Void Ark", 2);
            AddDuty("The Weeping City of Mhach", 2);
            AddDuty("Dun Scaith", 2);
            AddDuty("The Royal City of Rabanastre", 2);
            AddDuty("The Ridorana Lighthouse", 2);
            AddDuty("The Orbonne Monastery", 2);

            AddDuty("The Binding Coil of Bahamut Turn 1", 2);
            AddDuty("The Binding Coil of Bahamut Turn 2", 2);
            AddDuty("The Binding Coil of Bahamut Turn 3", 2);
            AddDuty("The Binding Coil of Bahamut Turn 4", 2);
            AddDuty("The Binding Coil of Bahamut Turn 5", 2);

            AddDuty("The Second Coil of Bahamut Turn 1", 2);
            AddDuty("The Second Coil of Bahamut Turn 2", 2);
            AddDuty("The Second Coil of Bahamut Turn 3", 2);
            AddDuty("The Second Coil of Bahamut Turn 4", 2);

            AddDuty("The Final Coil of Bahamut Turn 1", 2);
            AddDuty("The Final Coil of Bahamut Turn 2", 2);
            AddDuty("The Final Coil of Bahamut Turn 3", 2);
            AddDuty("The Final Coil of Bahamut Turn 4", 2);

            AddDuty("Alexander - The Fist of the Father", 2);
            AddDuty("Alexander - The Cuff of the Father", 2);
            AddDuty("Alexander - The Arm of the Father", 2);
            AddDuty("Alexander - Burden of the Father", 2);

            AddDuty("Alexander - The Fist of the Son", 2);
            AddDuty("Alexander - The Cuff of the Son", 2);
            AddDuty("Alexander - The Arm of the Son", 2);
            AddDuty("Alexander - Burden of the Son", 2);

            AddDuty("Alexander - The Eyes of the Creator", 2);
            AddDuty("Alexander - Breath of the Creator", 2);
            AddDuty("Alexander - Heart or Soul of the Creator", 2);
            AddDuty("Alexander - Soul of the Creator", 2);

            AddDuty("Omega: Deltascape V1.0", 2);
            AddDuty("Omega: Deltascape V2.0", 2);
            AddDuty("Omega: Deltascape V3.0", 2);
            AddDuty("Omega: Deltascape V4.0", 2);

            AddDuty("Omega: Sigmascape V1.0", 2);
            AddDuty("Omega: Sigmascape V2.0", 2);
            AddDuty("Omega: Sigmascape V3.0", 2);
            AddDuty("Omega: Sigmascape V4.0", 2);

            AddDuty("Omega: Alphascape V1.0", 2);
            AddDuty("Omega: Alphascape V2.0", 2);
            AddDuty("Omega: Alphascape V3.0", 2);
            AddDuty("Omega: Alphascape V4.0", 2);

            AddDuty("Eden's Gate: Resurrection", 2);
            AddDuty("Eden's Gate: Inundation", 2);
            AddDuty("Eden's Gate: Descent", 2);
            AddDuty("Eden's Gate: Sepulture", 2);

            //Trials
            AddDuty("The Minstrel's Ballad: Ultima's Bane", 3);
            AddDuty("The Howling Eye (Extreme)", 3);
            AddDuty("The Navel (Extreme)", 3);
            AddDuty("The Bowl of Embers (Extreme)", 3);
            AddDuty("Thornmarch (Extreme)", 3);
            AddDuty("The Whorleater (Extreme)", 3);
            AddDuty("The Striking Tree (Extreme)", 3);
            AddDuty("Akh Afah Amphitheatre (Extreme)", 3);
            AddDuty("The Limitless Blue (Extreme)", 3);
            AddDuty("Thok ast Thok (Extreme)", 3);
            AddDuty("The Minstrel's Ballad: Thordan's Reign", 3);
            AddDuty("The Minstrel's Ballad: Nidhogg's Rage", 3);
            AddDuty("Containment Bay S1T7 (Extreme)", 3);
            AddDuty("Containment Bay P1T6 (Extreme)", 3);
            AddDuty("Containment Bay Z1T9 (Extreme)", 3);
            AddDuty("Emanation (Extreme)", 3);
            AddDuty("The Pool of Tribute (Extreme)", 3);
            AddDuty("The Minstrel's Ballad: Shinryu's Domain", 3);
            AddDuty("The Jade Stoa (Extreme)", 3);
            AddDuty("The Minstrel's Ballad: Tsukuyomi's Pain", 3);
            AddDuty("The Great Hunt (Extreme)", 3);
            AddDuty("Hells' Kier (Extreme)", 3);
            AddDuty("The Wreath of Snakes (Extreme)", 3);

            //Other
            AddDuty("Aquapolis/Uznair/Lyhe Ghiah", 4);
            AddDuty("Palace of the Dead/Heaven-on-High", 4);
            AddDuty("PvP: Frontlines/Danshig Naadam, Astragalos/Hidden Gorge", 4);
            AddDuty("The Feast", 4);
        }

        public void AddDuty(string name, int category)
        {
            Guid key = Guid.NewGuid();
            Duties.Add(key, new Duty { Key = key, DutyName = name, Category = category });
        }

        public Party GetParty(Guid partyKey)
        {
            if (Parties.ContainsKey(partyKey))
            {
                return Parties[partyKey];
            }

            return null;
        }

        public Dictionary<Guid, Duty> GetDuties()
        {
            return Duties;
        }
    }
}
