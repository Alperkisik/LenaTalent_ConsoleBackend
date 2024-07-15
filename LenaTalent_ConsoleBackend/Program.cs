using LenaTalent_ConsoleBackend.Services;

var eventService = new EventService();

var bestEventCombinations = eventService.GetBestEventCombinationToAttend();

Console.WriteLine($"Katılınabilecek Maksimum Etkinlik Sayısı: {bestEventCombinations.EventIdList.Count}");
Console.WriteLine($"Katılınabilecek Etkinliklerin ID'leri: {string.Join(", ", bestEventCombinations.EventIdList)}");
Console.WriteLine($"Toplam Değer: {bestEventCombinations.TotalPoint}");

Console.ReadLine();