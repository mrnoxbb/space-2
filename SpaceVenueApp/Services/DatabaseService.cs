using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Data.Sqlite;
using SpaceVenueApp.Models;

namespace SpaceVenueApp.Services;

public class DatabaseService
{
    private readonly string _dbPath;

    public DatabaseService(string? dbPath = null)
    {
        var basePath = dbPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpaceVenueApp");
        Directory.CreateDirectory(basePath);
        _dbPath = Path.Combine(basePath, "space-venue.db");
    }

    public void Initialize()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
CREATE TABLE IF NOT EXISTS items (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    price REAL NOT NULL
);
CREATE TABLE IF NOT EXISTS item_sales (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    item_id INTEGER NOT NULL,
    quantity INTEGER NOT NULL,
    total REAL NOT NULL,
    created_at TEXT NOT NULL,
    FOREIGN KEY(item_id) REFERENCES items(id)
);
CREATE TABLE IF NOT EXISTS cash_transactions (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    type TEXT NOT NULL,
    amount REAL NOT NULL,
    notes TEXT,
    created_at TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS sessions (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    station_name TEXT NOT NULL,
    customer_name TEXT,
    start_time TEXT NOT NULL,
    end_time TEXT,
    cost REAL
);
";
        command.ExecuteNonQuery();
    }

    public List<Item> GetItems()
    {
        var items = new List<Item>();
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, name, price FROM items ORDER BY name";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            items.Add(new Item
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            });
        }

        return items;
    }

    public Dictionary<int, ItemSalesSummary> GetItemSalesSummary(DateTime date)
    {
        var summary = new Dictionary<int, ItemSalesSummary>();
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
SELECT item_id, SUM(quantity) AS units, SUM(total) AS revenue
FROM item_sales
WHERE date(created_at) = date($date)
GROUP BY item_id
";
        command.Parameters.AddWithValue("$date", date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            summary[reader.GetInt32(0)] = new ItemSalesSummary
            {
                UnitsSold = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                Revenue = reader.IsDBNull(2) ? 0m : reader.GetDecimal(2)
            };
        }

        return summary;
    }

    public int AddItem(Item item)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO items (name, price) VALUES ($name, $price); SELECT last_insert_rowid();";
        command.Parameters.AddWithValue("$name", item.Name);
        command.Parameters.AddWithValue("$price", item.Price);
        var id = (long)command.ExecuteScalar()!;
        return (int)id;
    }

    public void UpdateItem(Item item)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE items SET name = $name, price = $price WHERE id = $id";
        command.Parameters.AddWithValue("$name", item.Name);
        command.Parameters.AddWithValue("$price", item.Price);
        command.Parameters.AddWithValue("$id", item.Id);
        command.ExecuteNonQuery();
    }

    public void DeleteItem(int id)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM items WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();
    }

    public void AddItemSale(int itemId, int quantity, decimal total)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO item_sales (item_id, quantity, total, created_at) VALUES ($itemId, $quantity, $total, $createdAt)";
        command.Parameters.AddWithValue("$itemId", itemId);
        command.Parameters.AddWithValue("$quantity", quantity);
        command.Parameters.AddWithValue("$total", total);
        command.Parameters.AddWithValue("$createdAt", DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture));
        command.ExecuteNonQuery();
    }

    public List<CashTransaction> GetCashTransactions(DateTime date)
    {
        var transactions = new List<CashTransaction>();
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
SELECT id, type, amount, notes, created_at
FROM cash_transactions
WHERE date(created_at) = date($date)
ORDER BY created_at DESC";
        command.Parameters.AddWithValue("$date", date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            transactions.Add(new CashTransaction
            {
                Id = reader.GetInt32(0),
                Type = reader.GetString(1),
                Amount = reader.GetDecimal(2),
                Notes = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                CreatedAt = DateTime.Parse(reader.GetString(4), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            });
        }

        return transactions;
    }

    public void AddCashTransaction(CashTransaction transaction)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO cash_transactions (type, amount, notes, created_at) VALUES ($type, $amount, $notes, $createdAt)";
        command.Parameters.AddWithValue("$type", transaction.Type);
        command.Parameters.AddWithValue("$amount", transaction.Amount);
        command.Parameters.AddWithValue("$notes", transaction.Notes ?? string.Empty);
        command.Parameters.AddWithValue("$createdAt", transaction.CreatedAt.ToString("O", CultureInfo.InvariantCulture));
        command.ExecuteNonQuery();
    }

    public int AddSession(Session session)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
INSERT INTO sessions (station_name, customer_name, start_time)
VALUES ($station, $customer, $startTime);
SELECT last_insert_rowid();";
        command.Parameters.AddWithValue("$station", session.StationName);
        command.Parameters.AddWithValue("$customer", session.CustomerName ?? string.Empty);
        command.Parameters.AddWithValue("$startTime", session.StartTime.ToString("O", CultureInfo.InvariantCulture));
        var id = (long)command.ExecuteScalar()!;
        return (int)id;
    }

    public void CloseSession(int id, DateTime endTime, decimal cost)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
UPDATE sessions
SET end_time = $endTime, cost = $cost
WHERE id = $id";
        command.Parameters.AddWithValue("$endTime", endTime.ToString("O", CultureInfo.InvariantCulture));
        command.Parameters.AddWithValue("$cost", cost);
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();
    }

    public List<Session> GetSessions(DateTime date)
    {
        var sessions = new List<Session>();
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
SELECT id, station_name, customer_name, start_time, end_time, cost
FROM sessions
WHERE date(start_time) = date($date)
ORDER BY start_time DESC";
        command.Parameters.AddWithValue("$date", date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            sessions.Add(new Session
            {
                Id = reader.GetInt32(0),
                StationName = reader.GetString(1),
                CustomerName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                StartTime = DateTime.Parse(reader.GetString(3), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                EndTime = reader.IsDBNull(4)
                    ? null
                    : DateTime.Parse(reader.GetString(4), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                Cost = reader.IsDBNull(5) ? null : reader.GetDecimal(5)
            });
        }

        return sessions;
    }

    private SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection($"Data Source={_dbPath}");
        connection.Open();
        return connection;
    }
}
