using LenaTalent_ConsoleBackend.Dtos;

namespace LenaTalent_ConsoleBackend.Services
{
    public class EventService
    {
        readonly List<Event> events; 
        readonly List<LocationDuration> durations;
        
        public EventService()
        {
            events = GetEvents();
            durations = GetLocationDurationMatrix();
        }

        private List<Event> GetEvents()
        {
            return new List<Event>
            {
                new Event(1, TimeSpan.Parse("10:00"), TimeSpan.Parse("12:00"), "A", 50),
                new Event(2, TimeSpan.Parse("10:00"), TimeSpan.Parse("11:00"), "B", 30),
                new Event(3, TimeSpan.Parse("11:30"), TimeSpan.Parse("12:30"), "A", 40),
                new Event(4, TimeSpan.Parse("14:30"), TimeSpan.Parse("16:00"), "C", 70),
                new Event(5, TimeSpan.Parse("14:25"), TimeSpan.Parse("15:30"), "B", 60),
                new Event(6, TimeSpan.Parse("13:00"), TimeSpan.Parse("14:00"), "D", 80)
            };
        }

        private List<LocationDuration> GetLocationDurationMatrix()
        {
            return new List<LocationDuration>
            {
                new LocationDuration("A", "B", 15),
                new LocationDuration("A", "C", 20),
                new LocationDuration("A", "D", 10),
                new LocationDuration("B", "C", 5),
                new LocationDuration("B", "D", 25),
                new LocationDuration("C", "D", 25)
            };
        }

        public EventCombination GetBestEventCombinationToAttend()
        {
            var sortedEvents = events
                .OrderBy(e => e.Start_Time)
                .ThenByDescending(e => e.Priority)
                .ToList();

            var bestCombination = new EventCombination();

            for (int i = 0; i < sortedEvents.Count; i++)
            {
                var currentEvent = sortedEvents[i];
                var currentCombination = new EventCombination { TotalPoint = currentEvent.Priority };
                currentCombination.EventIdList.Add(currentEvent.Id);

                for (int j = i + 1; j < sortedEvents.Count; j++)
                {
                    var nextEvent = sortedEvents[j];
                    if (CanAttendEvent(currentCombination, nextEvent, durations))
                    {
                        currentCombination.TotalPoint += nextEvent.Priority;
                        currentCombination.EventIdList.Add(nextEvent.Id);
                    }
                }

                if (currentCombination.TotalPoint > bestCombination.TotalPoint)
                {
                    bestCombination = currentCombination;
                }
            }

            return bestCombination;
        }

        private bool CanAttendEvent(EventCombination currentCombination, Event nextEvent, List<LocationDuration> durations)
        {
            var lastEventId = currentCombination.EventIdList.LastOrDefault();
            var lastEvent = lastEventId != 0 ? events.First(e => e.Id == lastEventId) : null;

            if (lastEvent != null && lastEvent.End_Time > nextEvent.Start_Time)
                return false;

            if (lastEvent != null)
            {
                var lastEventLocation = lastEvent.Location;

                if (lastEventLocation != nextEvent.Location)
                {
                    var duration = durations.FirstOrDefault(d => d.From == lastEventLocation && d.To == nextEvent.Location);
                    if (duration != null)
                    {
                        var travelTime = TimeSpan.FromMinutes(duration.DurationMinutes);
                        if (lastEvent.End_Time.Add(travelTime) > nextEvent.Start_Time)
                            return false;
                    }
                }
            }

            return true;
        }
    }
}
