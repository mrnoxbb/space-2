using System;
using System.Collections.ObjectModel;
using System.Globalization;
 codex/create-desktop-app-for-billiards-venue-pnjysf
using System.IO;

 HEAD
using System.IO;

 main
 main
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using SpaceVenueApp.Models;
 codex/create-desktop-app-for-billiards-venue-pnjysf
using SpaceVenueApp.Services;

 HEAD
using SpaceVenueApp.Services;

 main
 main

namespace SpaceVenueApp.ViewModels;

public class MainViewModel : ObservableObject
{
    private readonly DispatcherTimer _timer;
 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
    private readonly DatabaseService _database;
    private bool _isRtlEnabled;
    private string _dailyReportSummary = string.Empty;
    private string _monthlyReportSummary = string.Empty;
    private FlowDirection _uiFlowDirection = FlowDirection.LeftToRight;
    private string _newItemName = string.Empty;
    private decimal _newItemPrice;
    private Item? _selectedItem;
    private int _saleQuantity = 1;
    private decimal _transactionAmount;
    private string _transactionNotes = string.Empty;

    public MainViewModel()
    {
        _database = new DatabaseService();
        _database.Initialize();

 codex/create-desktop-app-for-billiards-venue-pnjysf

    private bool _isRtlEnabled;
    private string _dailyReportSummary = string.Empty;
    private FlowDirection _uiFlowDirection = FlowDirection.LeftToRight;

    public MainViewModel()
    {
 main
 main
        Stations = new ObservableCollection<StationViewModel>
        {
            new("Billiards Table 1", "Pool Table", 120m),
            new("Billiards Table 2", "Pool Table", 120m),
            new("Billiards Table 3", "Pool Table", 120m),
            new("PlayStation 1", "Console", 90m),
            new("PlayStation 2", "Console", 90m)
        };

 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
        foreach (var station in Stations)
        {
            station.SessionStarted += OnSessionStarted;
            station.SessionStopped += OnSessionStopped;
        }

        Items = new ObservableCollection<Item>();
        CashTransactions = new ObservableCollection<CashTransaction>();
        Sessions = new ObservableCollection<Session>();

        CashSummary = new CashSummary();

        AddItemCommand = new RelayCommand(AddItem, () => !string.IsNullOrWhiteSpace(NewItemName) && NewItemPrice > 0);
        UpdateItemCommand = new RelayCommand(UpdateItem, () => SelectedItem != null);
        DeleteItemCommand = new RelayCommand(DeleteItem, () => SelectedItem != null);
        RecordSaleCommand = new RelayCommand(RecordSale, () => SelectedItem != null && SaleQuantity > 0);

        DepositCommand = new RelayCommand(() => AddTransaction("Deposit"), () => TransactionAmount > 0);
        WithdrawCommand = new RelayCommand(() => AddTransaction("Withdrawal"), () => TransactionAmount > 0);

        ExportCommand = new RelayCommand(ExportReport);

        LoadData();
        UpdateDailyReport();
        UpdateMonthlyReport();
 codex/create-desktop-app-for-billiards-venue-pnjysf

        Items = new ObservableCollection<Item>
        {
            new() { Name = "Karkadeh", Price = 25m, UnitsSold = 14 },
            new() { Name = "Cola", Price = 20m, UnitsSold = 10 },
            new() { Name = "Chips", Price = 15m, UnitsSold = 7 },
            new() { Name = "Energy Drink", Price = 35m, UnitsSold = 4 }
        };

        CashSummary = new CashSummary
        {
            OpeningBalance = 1500m,
            TotalDeposits = 2350m,
            TotalWithdrawals = 400m
        };

        ExportCommand = new RelayCommand(ExportReport);
        UpdateDailyReport();
 main
 main

        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (_, _) => TickStations();
        _timer.Start();
    }

    public ObservableCollection<StationViewModel> Stations { get; }
    public ObservableCollection<Item> Items { get; }
 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
    public ObservableCollection<CashTransaction> CashTransactions { get; }
    public ObservableCollection<Session> Sessions { get; }
    public CashSummary CashSummary { get; }

    public RelayCommand AddItemCommand { get; }
    public RelayCommand UpdateItemCommand { get; }
    public RelayCommand DeleteItemCommand { get; }
    public RelayCommand RecordSaleCommand { get; }
    public RelayCommand DepositCommand { get; }
    public RelayCommand WithdrawCommand { get; }
 codex/create-desktop-app-for-billiards-venue-pnjysf


    public CashSummary CashSummary { get; }
 main
 main
    public RelayCommand ExportCommand { get; }

    public bool IsRtlEnabled
    {
        get => _isRtlEnabled;
        set
        {
            if (SetProperty(ref _isRtlEnabled, value))
            {
                UiFlowDirection = _isRtlEnabled ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            }
        }
    }

    public FlowDirection UiFlowDirection
    {
        get => _uiFlowDirection;
        private set => SetProperty(ref _uiFlowDirection, value);
    }

    public string DailyReportSummary
    {
        get => _dailyReportSummary;
        private set => SetProperty(ref _dailyReportSummary, value);
    }

 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
    public string MonthlyReportSummary
    {
        get => _monthlyReportSummary;
        private set => SetProperty(ref _monthlyReportSummary, value);
    }

    public string NewItemName
    {
        get => _newItemName;
        set
        {
            if (SetProperty(ref _newItemName, value))
            {
                AddItemCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public decimal NewItemPrice
    {
        get => _newItemPrice;
        set
        {
            if (SetProperty(ref _newItemPrice, value))
            {
                AddItemCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public Item? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (SetProperty(ref _selectedItem, value))
            {
                UpdateItemCommand.RaiseCanExecuteChanged();
                DeleteItemCommand.RaiseCanExecuteChanged();
                RecordSaleCommand.RaiseCanExecuteChanged();
                if (value != null)
                {
                    NewItemName = value.Name;
                    NewItemPrice = value.Price;
                }
            }
        }
    }

    public int SaleQuantity
    {
        get => _saleQuantity;
        set
        {
            if (SetProperty(ref _saleQuantity, value))
            {
                RecordSaleCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public decimal TransactionAmount
    {
        get => _transactionAmount;
        set
        {
            if (SetProperty(ref _transactionAmount, value))
            {
                DepositCommand.RaiseCanExecuteChanged();
                WithdrawCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string TransactionNotes
    {
        get => _transactionNotes;
        set => SetProperty(ref _transactionNotes, value);
    }

    private void LoadData()
    {
        Items.Clear();
        var items = _database.GetItems();
        var salesSummary = _database.GetItemSalesSummary(DateTime.Today);
        foreach (var item in items)
        {
            if (salesSummary.TryGetValue(item.Id, out var summary))
            {
                item.UnitsSold = summary.UnitsSold;
                item.Revenue = summary.Revenue;
            }
            Items.Add(item);
        }

        CashTransactions.Clear();
        foreach (var transaction in _database.GetCashTransactions(DateTime.Today))
        {
            CashTransactions.Add(transaction);
        }

        Sessions.Clear();
        foreach (var session in _database.GetSessions(DateTime.Today))
        {
            Sessions.Add(session);
        }

        RecalculateCashSummary();
    }

 codex/create-desktop-app-for-billiards-venue-pnjysf


 main
 main
    private void TickStations()
    {
        foreach (var station in Stations)
        {
            station.Tick(TimeSpan.FromSeconds(1));
        }

        UpdateDailyReport();
 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
        UpdateMonthlyReport();
    }

    private void AddItem()
    {
        var item = new Item { Name = NewItemName.Trim(), Price = NewItemPrice };
        item.Id = _database.AddItem(item);
        Items.Add(item);
        NewItemName = string.Empty;
        NewItemPrice = 0m;
    }

    private void UpdateItem()
    {
        if (SelectedItem == null)
        {
            return;
        }

        SelectedItem.Name = NewItemName.Trim();
        SelectedItem.Price = NewItemPrice;
        _database.UpdateItem(SelectedItem);
        RaisePropertyChanged(nameof(Items));
    }

    private void DeleteItem()
    {
        if (SelectedItem == null)
        {
            return;
        }

        _database.DeleteItem(SelectedItem.Id);
        Items.Remove(SelectedItem);
        SelectedItem = null;
    }

    private void RecordSale()
    {
        if (SelectedItem == null)
        {
            return;
        }

        var total = SelectedItem.Price * SaleQuantity;
        _database.AddItemSale(SelectedItem.Id, SaleQuantity, total);
        SelectedItem.UnitsSold += SaleQuantity;
        SelectedItem.Revenue += total;
        SaleQuantity = 1;
        UpdateDailyReport();
        UpdateMonthlyReport();
    }

    private void AddTransaction(string type)
    {
        var transaction = new CashTransaction
        {
            Type = type,
            Amount = TransactionAmount,
            Notes = TransactionNotes,
            CreatedAt = DateTime.UtcNow
        };
        _database.AddCashTransaction(transaction);
        CashTransactions.Insert(0, transaction);
        TransactionAmount = 0m;
        TransactionNotes = string.Empty;
        RecalculateCashSummary();
    }

    private void RecalculateCashSummary()
    {
        var deposits = CashTransactions.Where(t => t.Type == "Deposit").Sum(t => t.Amount);
        var withdrawals = CashTransactions.Where(t => t.Type == "Withdrawal").Sum(t => t.Amount);
        CashSummary.OpeningBalance = 1500m;
        CashSummary.TotalDeposits = deposits;
        CashSummary.TotalWithdrawals = withdrawals;
        RaisePropertyChanged(nameof(CashSummary));
codex/create-desktop-app-for-billiards-venue-pnjysf


 main
 main
    }

    private void UpdateDailyReport()
    {
        var totalStationRevenue = Stations.Sum(station => station.CurrentCharge);
 codex/create-desktop-app-for-billiards-venue-pnjysf
        var totalItemRevenue = Items.Sum(item => item.Revenue);

 HEAD
        var totalItemRevenue = Items.Sum(item => item.Revenue);

        var totalItemRevenue = Items.Sum(item => item.Price * item.UnitsSold);
 main
 main
        var totalRevenue = totalStationRevenue + totalItemRevenue;

        var builder = new StringBuilder();
        builder.AppendLine($"Stations revenue: {totalStationRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine($"Item sales: {totalItemRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine($"Estimated total: {totalRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine("Close the day with a cash count and reconcile against expected balance.");

        DailyReportSummary = builder.ToString().Trim();
    }

 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main
    private void UpdateMonthlyReport()
    {
        var monthlyStationRevenue = Stations.Sum(station => station.CurrentCharge) * 30;
        var monthlyItemRevenue = Items.Sum(item => item.Revenue) * 30;
        var totalMonthly = monthlyStationRevenue + monthlyItemRevenue;

        var builder = new StringBuilder();
        builder.AppendLine($"Projected stations revenue: {monthlyStationRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine($"Projected item sales: {monthlyItemRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine($"Projected total: {totalMonthly.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine("Use the CSV export for detailed monthly breakdowns.");

        MonthlyReportSummary = builder.ToString().Trim();
    }

    private void ExportReport()
    {
        var exportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpaceVenueApp", "Reports");
        Directory.CreateDirectory(exportPath);

        var sessionsFile = Path.Combine(exportPath, $"sessions-{DateTime.Today:yyyyMMdd}.csv");
        var transactionsFile = Path.Combine(exportPath, $"cash-{DateTime.Today:yyyyMMdd}.csv");
        var itemsFile = Path.Combine(exportPath, $"items-{DateTime.Today:yyyyMMdd}.csv");

        File.WriteAllLines(sessionsFile, CsvExportService.ExportSessions(Sessions));
        File.WriteAllLines(transactionsFile, CsvExportService.ExportTransactions(CashTransactions));
        File.WriteAllLines(itemsFile, CsvExportService.ExportItems(Items));

        MessageBox.Show($"Export complete. Reports saved to {exportPath}.",
 codex/create-desktop-app-for-billiards-venue-pnjysf


    private void ExportReport()
    {
        MessageBox.Show("Export queued. Reports will be saved as PDF and CSV in the reports folder.",
 main
 main
            "Export",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
 codex/create-desktop-app-for-billiards-venue-pnjysf

 HEAD
 main

    private void OnSessionStarted(object? sender, SessionEventArgs e)
    {
        var session = new Session
        {
            StationName = e.StationName,
            CustomerName = string.IsNullOrWhiteSpace(e.CustomerName) ? "Walk-in" : e.CustomerName,
            StartTime = DateTime.UtcNow
        };
        var sessionId = _database.AddSession(session);
        if (sender is StationViewModel station)
        {
            station.AttachSession(sessionId);
        }

        session.Id = sessionId;
        Sessions.Insert(0, session);
    }

    private void OnSessionStopped(object? sender, SessionEventArgs e)
    {
        if (e.SessionId is null)
        {
            return;
        }

        var cost = e.Cost ?? 0m;
        _database.CloseSession(e.SessionId.Value, DateTime.UtcNow, cost);
        var session = Sessions.FirstOrDefault(entry => entry.Id == e.SessionId.Value);
        if (session != null)
        {
            session.EndTime = DateTime.UtcNow;
            session.Cost = cost;
        }

        UpdateDailyReport();
        UpdateMonthlyReport();
    }
 codex/create-desktop-app-for-billiards-venue-pnjysf


 main
 main
}
