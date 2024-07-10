using FFXIV.WT.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FFXIV.WT.Service
{
    public class WTService
    {
        private Dictionary<Guid, Duty> Duties;
        private Dictionary<Guid, Party> Parties;
        public event Action OnChange;
        private readonly ILogger<WTService> _logger;
        
        public WTService(ILogger<WTService> logger)
        {
            PopulateDuties();
            Parties = new Dictionary<Guid, Party>();
            _logger = logger;
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

            NotifyStateChanged();
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

        private async Task PopulateDuties()
        {
            Duties = new Dictionary<Guid, Duty>();

            var remoteDutyList = await GetRemoteDutyList();

            foreach (var a in remoteDutyList)
            {
                Duties.Add(a.Key, new Duty { Key = a.Key, Category = a.Category, DutyName = a.Name });
            }
        }

        private async Task<List<RemoteDuty>> GetRemoteDutyList()
        {
            try
            {
                string downloadUrl = "https://raw.githubusercontent.com/Lelocky/FFXIV.WTBuilder/main/data/duties.json";

                using var client = new HttpClient();
                using var s = await client.GetStreamAsync(downloadUrl);
                using var reader = new StreamReader(s);

                var dutyJson = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<List<RemoteDuty>>(dutyJson);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Exception while getting remote duties");
                return new List<RemoteDuty>();
            }

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

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class RemoteDuty
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
    }
}
