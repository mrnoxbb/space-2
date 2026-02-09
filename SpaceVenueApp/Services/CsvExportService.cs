using System.Collections.Generic;
using System.Globalization;
using SpaceVenueApp.Models;

namespace SpaceVenueApp.Services;

public static class CsvExportService
{
    public static IEnumerable<string> ExportSessions(IEnumerable<Session> sessions)
    {
        yield return "Station,Customer,Start,End,Cost";
        foreach (var session in sessions)
        {
            yield return string.Join(',',
                Escape(session.StationName),
                Escape(session.CustomerName ?? string.Empty),
                Escape(session.StartTime.ToString("u", CultureInfo.InvariantCulture)),
                Escape(session.EndTime?.ToString("u", CultureInfo.InvariantCulture) ?? string.Empty),
                session.Cost?.ToString(CultureInfo.InvariantCulture) ?? string.Empty);
        }
    }

    public static IEnumerable<string> ExportTransactions(IEnumerable<CashTransaction> transactions)
    {
        yield return "Type,Amount,Notes,CreatedAt";
        foreach (var transaction in transactions)
        {
            yield return string.Join(',',
                Escape(transaction.Type),
                transaction.Amount.ToString(CultureInfo.InvariantCulture),
                Escape(transaction.Notes),
                Escape(transaction.CreatedAt.ToString("u", CultureInfo.InvariantCulture)));
        }
    }

    public static IEnumerable<string> ExportItems(IEnumerable<Item> items)
    {
        yield return "Item,Price,UnitsSold,Revenue";
        foreach (var item in items)
        {
            yield return string.Join(',',
                Escape(item.Name),
                item.Price.ToString(CultureInfo.InvariantCulture),
                item.UnitsSold.ToString(CultureInfo.InvariantCulture),
                item.Revenue.ToString(CultureInfo.InvariantCulture));
        }
    }

    private static string Escape(string input)
    {
        if (!input.Contains(',') && !input.Contains('"') && !input.Contains('\n'))
        {
            return input;
        }

        return $"\"{input.Replace("\"", "\"\"")}\"";
    }
}
