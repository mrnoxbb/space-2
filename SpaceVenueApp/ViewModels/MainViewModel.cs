using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using SpaceVenueApp.Models;

namespace SpaceVenueApp.ViewModels;

public class MainViewModel : ObservableObject
{
    private readonly DispatcherTimer _timer;
    private bool _isRtlEnabled;
    private string _dailyReportSummary = string.Empty;
    private FlowDirection _uiFlowDirection = FlowDirection.LeftToRight;

    public MainViewModel()
    {
        Stations = new ObservableCollection<StationViewModel>
        {
            new("Billiards Table 1", "Pool Table", 120m),
            new("Billiards Table 2", "Pool Table", 120m),
            new("Billiards Table 3", "Pool Table", 120m),
            new("PlayStation 1", "Console", 90m),
            new("PlayStation 2", "Console", 90m)
        };

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

        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (_, _) => TickStations();
        _timer.Start();
    }

    public ObservableCollection<StationViewModel> Stations { get; }
    public ObservableCollection<Item> Items { get; }
    public CashSummary CashSummary { get; }
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

    private void TickStations()
    {
        foreach (var station in Stations)
        {
            station.Tick(TimeSpan.FromSeconds(1));
        }

        UpdateDailyReport();
    }

    private void UpdateDailyReport()
    {
        var totalStationRevenue = Stations.Sum(station => station.CurrentCharge);
        var totalItemRevenue = Items.Sum(item => item.Price * item.UnitsSold);
        var totalRevenue = totalStationRevenue + totalItemRevenue;

        var builder = new StringBuilder();
        builder.AppendLine($"Stations revenue: {totalStationRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine($"Item sales: {totalItemRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine($"Estimated total: {totalRevenue.ToString("C", CultureInfo.CurrentCulture)}");
        builder.AppendLine("Close the day with a cash count and reconcile against expected balance.");

        DailyReportSummary = builder.ToString().Trim();
    }

    private void ExportReport()
    {
        MessageBox.Show("Export queued. Reports will be saved as PDF and CSV in the reports folder.",
            "Export",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}
