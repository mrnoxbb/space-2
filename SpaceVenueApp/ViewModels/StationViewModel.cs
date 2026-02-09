using System;
using System.Windows.Input;
using System.Windows.Media;

namespace SpaceVenueApp.ViewModels;

public class StationViewModel : ObservableObject
{
    private bool _isActive;
    private bool _isPaused;
    private TimeSpan _elapsed;
    private decimal _currentCharge;

    public StationViewModel(string name, string type, decimal ratePerHour)
    {
        Name = name;
        Type = type;
        RatePerHour = ratePerHour;
        StartCommand = new RelayCommand(StartSession, () => !IsActive || IsPaused);
        PauseCommand = new RelayCommand(PauseSession, () => IsActive && !IsPaused);
        StopCommand = new RelayCommand(StopSession, () => IsActive);
    }

    public string Name { get; }
    public string Type { get; }
    public decimal RatePerHour { get; }

    public ICommand StartCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand StopCommand { get; }

    public bool IsActive
    {
        get => _isActive;
        private set
        {
            if (SetProperty(ref _isActive, value))
            {
                RaisePropertyChanged(nameof(StatusBrush));
                RaiseCommandStates();
            }
        }
    }

    public bool IsPaused
    {
        get => _isPaused;
        private set
        {
            if (SetProperty(ref _isPaused, value))
            {
                RaiseCommandStates();
            }
        }
    }

    public TimeSpan Elapsed
    {
        get => _elapsed;
        private set
        {
            if (SetProperty(ref _elapsed, value))
            {
                RaisePropertyChanged(nameof(ElapsedDisplay));
            }
        }
    }

    public string ElapsedDisplay => $"{Elapsed:hh\\:mm\\:ss}";

    public decimal CurrentCharge
    {
        get => _currentCharge;
        private set => SetProperty(ref _currentCharge, value);
    }

    public Brush StatusBrush => IsActive ? Brushes.LimeGreen : Brushes.IndianRed;

    public void Tick(TimeSpan delta)
    {
        if (!IsActive || IsPaused)
        {
            return;
        }

        Elapsed += delta;
        CurrentCharge = (decimal)Elapsed.TotalHours * RatePerHour;
    }

    private void StartSession()
    {
        IsActive = true;
        IsPaused = false;
    }

    private void PauseSession()
    {
        IsPaused = true;
    }

    private void StopSession()
    {
        IsActive = false;
        IsPaused = false;
        Elapsed = TimeSpan.Zero;
        CurrentCharge = 0m;
    }

    private void RaiseCommandStates()
    {
        (StartCommand as RelayCommand)?.RaiseCanExecuteChanged();
        (PauseCommand as RelayCommand)?.RaiseCanExecuteChanged();
        (StopCommand as RelayCommand)?.RaiseCanExecuteChanged();
    }
}
